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
    public class CedenteLocalController : Controller
    {
        private readonly iSangueContext _context;

        public CedenteLocalController(iSangueContext context)
        {
            _context = context;
        }

        // GET: CedenteLocal
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: CedenteLocal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cedenteLocal = await _context.CedenteLocal
                .FirstOrDefaultAsync(m => m.id == id);
            if (cedenteLocal == null)
            {
                return NotFound();
            }

            return View(cedenteLocal);
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
                _context.Add(cedenteLocal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cedenteLocal);
        }

        // GET: CedenteLocal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cedenteLocal = await _context.CedenteLocal.FindAsync(id);
            if (cedenteLocal == null)
            {
                return NotFound();
            }
            return View(cedenteLocal);
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
                    _context.Update(cedenteLocal);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cedenteLocal = await _context.CedenteLocal
                .FirstOrDefaultAsync(m => m.id == id);
            if (cedenteLocal == null)
            {
                return NotFound();
            }

            return View(cedenteLocal);
        }

        // POST: CedenteLocal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cedenteLocal = await _context.CedenteLocal.FindAsync(id);
            _context.CedenteLocal.Remove(cedenteLocal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CedenteLocalExists(int id)
        {
            return _context.CedenteLocal.Any(e => e.id == id);
        }
    }
}
