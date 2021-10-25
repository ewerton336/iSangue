using System;
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
    public class EntidadeColetoraController : Controller
    {
        private readonly iSangueContext _context;
        private EntidadeColetoraDao entidadeColetoraDao;
        private UsuarioDao usuarioDao;
        private string usuario;
        private string email;

        public EntidadeColetoraController(iSangueContext context)
        {
            _context = context;
            entidadeColetoraDao = new EntidadeColetoraDao(Helper.DBConnectionSql);
            usuarioDao = new UsuarioDao(Helper.DBConnectionSql);
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

        EntidadeColetoraDao Entidade
        {
            get
            {
                if (entidadeColetoraDao == null)
                {
                    entidadeColetoraDao = new EntidadeColetoraDao(Helper.DBConnectionSql);
                }
                return entidadeColetoraDao;
            }
            set
            {
                entidadeColetoraDao = value;
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




        // GET: EntidadeColetora
        public async Task<IActionResult> Index()
        {
            if (usuarioSession.Equals("ADMINISTRADOR") || usuario.Equals("ENTIDADE_COLETORA"))
            {
                return View(await Entidade.GetEntidades());
            }
            else
            {
                return Redirect("../Error/NotAuthorized");
            }
        }

        // GET: EntidadeColetora/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var entidadeColetora = await Entidade.GetEntidadeById(id);
            if (usuarioSession == "ADMINISTRADOR")
            {
                return View(entidadeColetora);
            }

            if (entidadeColetora == null || entidadeColetora.email != emailSession)
            {
                return Redirect("../Error/NotAuthorized");
            }
            return View(entidadeColetora);
        }

        // GET: EntidadeColetora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntidadeColetora/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEntidade,nome,enderecoComercial,telefone,nomeResponsavel,id,email,senha,tipoUsuario")] EntidadeColetora entidadeColetora)
        {
            if (ModelState.IsValid)
            {
                await usuarioDao.InserirUsuario(entidadeColetora.email, entidadeColetora.senha, "ENTIDADE_COLETORA");
                int idCriada = await usuarioDao.getIdByEmail(entidadeColetora.email);
                await Entidade.InserirEntidade(entidadeColetora, idCriada);
                return RedirectToAction(nameof(Index));
            }
            return View(entidadeColetora);
        }

        // GET: EntidadeColetora/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entidadeColetora = await Entidade.GetEntidadeById(id);
            if (entidadeColetora == null)
            {
                return NotFound();
            }
            if (usuarioSession == "ADMINISTRADOR")
            {
                return View(entidadeColetora);
            }
            if (entidadeColetora.email != emailSession)
            {
                return Redirect("../Error/NotAuthorized");
            }
            return View(entidadeColetora);
        }

        // POST: EntidadeColetora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idEntidade,nome,enderecoComercial,telefone,nomeResponsavel,id,email,senha,tipoUsuario")] EntidadeColetora entidadeColetora)
        {

            if (id != entidadeColetora.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Entidade.AtualizarEntidade(entidadeColetora);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntidadeColetoraExists(entidadeColetora.id))
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
            return View(entidadeColetora);
        }

        // GET: EntidadeColetora/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var entidade = await Entidade.GetEntidadeById(id);
            if (usuarioSession == "ADMINISTRADOR")
            {
                return View(entidade);
            }

            if (entidade == null || entidade.email != emailSession)
            {
                return Redirect("../Error/NotAuthorized");
            }
            return View(entidade);
        }

        // POST: EntidadeColetora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entidade = await Entidade.GetEntidadeById(id);
            await usuarioDao.Delete(entidade.id);
            return RedirectToAction(nameof(Index));
        }

        private bool EntidadeColetoraExists(int id)
        {
            return _context.entidadeColetora.Any(e => e.id == id);
        }
    }
}
