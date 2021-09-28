using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iSangue.Data;
using iSangue.Models;

namespace iSangue.Controllers
{
    public class EntidadecoletoraController : Controller
    {
        private readonly iSangueContext _context;

        public EntidadecoletoraController(iSangueContext context)
        {
            _context = context;
        }

        // GET: Entidadecoletora
        public async Task<IActionResult> Index()
        {
            return View(await _context.Entidadecoletora.ToListAsync());
        }

        // GET: Entidadecoletora/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entidadecoletora = await _context.Entidadecoletora
                .FirstOrDefaultAsync(m => m.id == id);
            if (entidadecoletora == null)
            {
                return NotFound();
            }

            return View(entidadecoletora);
        }

        // GET: Entidadecoletora/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entidadecoletora/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,enderecoComercial,telefone,nomeResponsavel,id,email,senha")] Entidadecoletora entidadecoletora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entidadecoletora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(entidadecoletora);
        }

        // GET: Entidadecoletora/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entidadecoletora = await _context.Entidadecoletora.FindAsync(id);
            if (entidadecoletora == null)
            {
                return NotFound();
            }
            return View(entidadecoletora);
        }

        // POST: Entidadecoletora/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nome,enderecoComercial,telefone,nomeResponsavel,id,email,senha")] Entidadecoletora entidadecoletora)
        {
            if (id != entidadecoletora.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entidadecoletora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntidadecoletoraExists(entidadecoletora.id))
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
            return View(entidadecoletora);
        }

        // GET: Entidadecoletora/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entidadecoletora = await _context.Entidadecoletora
                .FirstOrDefaultAsync(m => m.id == id);
            if (entidadecoletora == null)
            {
                return NotFound();
            }

            return View(entidadecoletora);
        }

        // POST: Entidadecoletora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entidadecoletora = await _context.Entidadecoletora.FindAsync(id);
            _context.Entidadecoletora.Remove(entidadecoletora);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntidadecoletoraExists(int id)
        {
            return _context.Entidadecoletora.Any(e => e.id == id);
        }
    }
}
