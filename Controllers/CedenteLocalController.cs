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

namespace iSangue.Controllers
{
    public class CedenteLocalController : Controller
    {
        private readonly iSangueContext _context;
        private CedenteLocalDao cedenteLocalDao;
        private UsuarioDao usuarioDao;

        public CedenteLocalController(iSangueContext context)
        {
            _context = context;

        }
        CedenteLocalDao Cedente
        {
            get
            {
                if (cedenteLocalDao == null)
                {
                    cedenteLocalDao = new CedenteLocalDao(Helper.DBConnectionSql);
                }
                return cedenteLocalDao;
            }
            set
            {
                cedenteLocalDao = value;
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


        // GET: CedenteLocal
        public async Task<IActionResult> Index()
        {
            return View(await Cedente.GetCedenteLocals());
        }

        // GET: CedenteLocal/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var cedente = Cedente.GetCedenteById(id);

            if (cedente == null)
            {
                return NotFound();
            }

            return View(cedente);
        }

        // GET: CedenteLocal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CedenteLocal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,telefone,endereco,responsavel,id,email,senha,tipoUsuario")] CedenteLocal cedenteLocal)
        {
            if (ModelState.IsValid)
            {
                Usuario.InserirUsuario(cedenteLocal.email, cedenteLocal.senha, "CEDENTE_LOCAL");
                int idCriada = Usuario.getIdByEmail(cedenteLocal.email);
                Cedente.InserirCedente(cedenteLocal, idCriada);
                return RedirectToAction(nameof(Index));
            }
            return View(cedenteLocal);
        }

        // GET: CedenteLocal/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cedente = Cedente.GetCedenteById(id);
            if (cedente == null)
            {
                return NotFound();
            }
            return View(cedente);
        }


        // POST: CedenteLocal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nome,telefone,endereco,responsavel,id,email,senha,tipoUsuario")] CedenteLocal cedenteLocal)
        {
            if (id != cedenteLocal.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Cedente.AtualizarCedente(cedenteLocal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CedenteLocalExists(cedenteLocal.id))
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
            return View(cedenteLocal);
        }

        // GET: CedenteLocal/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cedente = Cedente.GetCedenteById(id);
            if (cedente == null)
            {
                return NotFound();
            }

            return View(cedente);
        }

        // POST: CedenteLocal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cedente = Cedente.GetCedenteById(id);
            Usuario.Delete(cedente.id);
            return RedirectToAction(nameof(Index));
        }

        private bool CedenteLocalExists(int id)
        {
            return _context.CedenteLocal.Any(e => e.id == id);
        }
    }
}
