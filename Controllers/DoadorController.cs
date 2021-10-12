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

namespace iSangue.Controllers
{
    public class DoadorController : Controller
    {
        private readonly iSangueContext _context;
        private DoadorDao doadorDao;
        private UsuarioDao usuarioDao;
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





        // GET: Doador
        public async Task<IActionResult> Index()
        {
            return View(await Doador.GetDoadores());
        }


        public async Task<IActionResult> LoginError()
        {
            return View();
        }

        public async Task<IActionResult> LoginSucess()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("email,senha")] Doador doador)
        {
            var login = Usuario.LoginUsuario(doador.email, doador.senha);
            if (login != null)
            {
                return RedirectToAction(nameof(LoginSucess));
            }
            else
            {
                return RedirectToAction(nameof(LoginError));
            }

        }





        // GET: Doador/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var doador = Doador.GetDoadorById(id);

            if (doador == null)
            {
                return NotFound();
            }

            return View(doador);
        }

        // GET: Doador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,sobrenome,endereco,numeroResidencia,complemento,cidadeResidencia,estadoResidencia,dataNasc,telefone,cidadeDoacao,tipoSanguineo,id,email,senha")] Doador doador)
        {
            if (ModelState.IsValid)
            {
                Usuario.InserirUsuario(doador.email, doador.senha, "DOADOR");
                int idCriada = Usuario.getIdByEmail(doador.email);
                Doador.InserirDoador(doador, idCriada);
                return RedirectToAction(nameof(Index));
            }
            return View(doador);
        }

        // GET: Doador/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var doador = Doador.GetDoadorById(id);
            if (doador == null)
            {
                return NotFound();
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
                    Doador.AtualizarDoador(doador);
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
                return RedirectToAction(nameof(Index));
            }
            return View(doador);
        }

        // GET: Doador/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var doador = Doador.GetDoadorById(id);
            if (doador == null)
            {
                return NotFound();
            }

            return View(doador);
        }

        // POST: Doador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doador = Doador.GetDoadorById(id);
            Usuario.Delete(doador.id);
            return RedirectToAction(nameof(Index));
        }

        private bool DoadorExists(int id)
        {
            return _context.Doador.Any(e => e.id == id);
        }
    }
}
