﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iSangue.Data;
using iSangue.Models;
using iSangue.DAO;
using Microsoft.AspNetCore.Http;

namespace iSangue.Controllers
{
    public class DoadorController : Controller
    {
        private readonly iSangueContext _context;
        private DoadorDao doadorDao;
        private UsuarioDao usuarioDao;
        private string usuario;
        public string email;


        public DoadorController(iSangueContext context)
        {
            _context = context;
        }
        DoadorDao Doador
        {
            get
            {
                if (doadorDao == null)
                {
                    doadorDao = new DoadorDao(Helper.DBConnectionSql);
                }
                return doadorDao;
            }
            set
            {
                doadorDao = value;
            }
        }

        UsuarioDao Usuario
        {
            get
            {
                if (usuarioDao == null)
                {
                    usuarioDao = new UsuarioDao(Helper.DBConnectionSql);
                }
                return usuarioDao;
            }
            set
            {
                usuarioDao = value;
            }
        }

        private string usuarioSession
        {
            get
            {
                if (usuario == null)
                {
                    usuario = HttpContext.Session.GetString("TIPO_USUARIO") == null ? "" : HttpContext.Session.GetString("TIPO_USUARIO");
                }
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        private string emailSession
        {
            get
            {
                if (email == null)
                {
                    email = HttpContext.Session.GetString("EMAIL_USUARIO") == null ? "" : HttpContext.Session.GetString("EMAIL_USUARIO");
                }
                return email;
            }

            set
            {
                email = value;
            }
        }


        // GET: Doador
        public async Task<IActionResult> IndexAdmin()
        {
            if (usuarioSession.Equals("ADMINISTRADOR"))
            {
                return View(await Doador.GetDoadores());
            }
            else
            {
                return Redirect("../Error/NotAuthorized");
            }
        }

        // GET: Doador/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var doador = await Doador.GetDoadorById(id);

            if (usuarioSession == "ADMINISTRADOR" || doador.email == emailSession)
            {
                return View(doador);
            }

            if (doador == null || doador.email != emailSession)
            {
                return Redirect("../Error/NotAuthorized");
            }

            return View(doador);
        }

        // GET: Doador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,sobrenome,endereco,numeroResidencia,complemento,cidadeResidencia,estadoResidencia,dataNasc,telefone,cidadeDoacao,tipoSanguineo,id,email,senha")] Doador doador)
        {
            if (ModelState.IsValid)
            {
                var usuario =  await Usuario.VerificarUsuarioExistente(doador.email);
                if (usuario != null) return Redirect("../Usuario/ErrorUsuarioJaExistente");
                await Usuario.InserirUsuario(doador.email, doador.senha, "DOADOR");
                int idCriada = await Usuario.getIdByEmail(doador.email);
                await Doador.InserirDoador(doador, idCriada);

                //após cadastro atribuir os dados criados na session
                var doador2 = await Doador.GetDoadorByUserID(idCriada);
                HttpContext.Session.SetString("ID_DOADOR", doador2.idDoador.ToString());
                HttpContext.Session.SetString("TIPO_USUARIO", "DOADOR");
                HttpContext.Session.SetString("NOME_USUARIO", doador.nome);
                HttpContext.Session.SetString("EMAIL_USUARIO", doador.email);
                HttpContext.Session.SetString("ID_USUARIO", idCriada.ToString());

                return Redirect("../Home/Index"); ;

            }

            return View(doador);
        }

        // GET: Doador/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var doador = await Doador.GetDoadorById(id);
            if (doador == null)
            {
                return NotFound();
            }
            if (usuarioSession == "ADMINISTRADOR" || doador.email == emailSession)
            {
                return View(doador);
            }
            if (doador.email != emailSession)
            {
                return Redirect("../Error/NotAuthorized");
            }
            return View(doador);
        }

        // POST: Doador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nome,sobrenome,endereco,numeroResidencia,complemento,cidadeResidencia,estadoResidencia,dataNasc,telefone,cidadeDoacao,tipoSanguineo,id,email,senha")] Doador doador)
        {
            if (id != doador.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Doador.AtualizarDoador(doador);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoadorExists(doador.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(doador);
        }

        // GET: Doador/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var doador = await Doador.GetDoadorById(id);

            if (usuarioSession == "ADMINISTRADOR")
            {
                return View(doador);
            }

            if (doador == null || doador.email != emailSession)
            {
                return Redirect("../Error/NotAuthorized");
            }
            return View(doador);
        }

        // POST: Doador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doador = Doador.GetDoadorById(id).Result;
            await Usuario.Delete(doador.id);
            return RedirectToAction(nameof(IndexAdmin));
        }


        private bool DoadorExists(int id)
        {
            return _context.Doador.Any(e => e.id == id);
        }
    }
}
