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
    public class EntidadeColetoraController : Controller
    {
        private readonly iSangueContext _context;
        private readonly EntidadeColetoraDao entidadeColetoraDao;
        private readonly UsuarioDao usuarioDao;

        public EntidadeColetoraController(iSangueContext context)
        {
            _context = context;
            entidadeColetoraDao = new EntidadeColetoraDao(Helper.DBConnectionSql);
            usuarioDao = new UsuarioDao(Helper.DBConnectionSql);
        }

        // GET: EntidadeColetora
        public async Task<IActionResult> Index()
        {
            return View(await entidadeColetoraDao.GetEntidades());
        }

        // GET: EntidadeColetora/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var entidadeColetora = entidadeColetoraDao.GetEntidadeById(id);
            if (entidadeColetora == null)
            {
                return NotFound();
            }

            return View(entidadeColetora);
        }

        // GET: EntidadeColetora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntidadeColetora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idEntidade,nome,enderecoComercial,telefone,nomeResponsavel,id,email,senha,tipoUsuario")] EntidadeColetora entidadeColetora)
        {
            if (ModelState.IsValid)
            {
                usuarioDao.InserirUsuario(entidadeColetora.email, entidadeColetora.senha, "ENTIDADE_COLETORA");
                int idCriada = usuarioDao.getIdByEmail(entidadeColetora.email);
                entidadeColetoraDao.InserirEntidade(entidadeColetora, idCriada);
                return RedirectToAction(nameof(Index));
            }
            return View(entidadeColetora);
        }

        // GET: EntidadeColetora/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var entidadeColetora = entidadeColetoraDao.GetEntidadeById(id);
            if (entidadeColetora == null)
            {
                return NotFound();
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
                    entidadeColetoraDao.AtualizarEntidade(entidadeColetora);
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
            var entidade = entidadeColetoraDao.GetEntidadeById(id);
            if (entidade == null)
            {
                return NotFound();
            }

            return View(entidade);
        }

        // POST: EntidadeColetora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entidade = entidadeColetoraDao.GetEntidadeById(id);
            usuarioDao.Delete(entidade.id);
            return RedirectToAction(nameof(Index));
        }

        private bool EntidadeColetoraExists(int id)
        {
            return _context.entidadeColetora.Any(e => e.id == id);
        }
    }
}
