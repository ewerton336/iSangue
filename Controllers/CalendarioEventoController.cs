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
    public class CalendarioEventoController : Controller
    {
        private readonly iSangueContext _context;

        public CalendarioEventoController(iSangueContext context)
        {
            _context = context;
        }

        // GET: CalendarioEvento
        public async Task<IActionResult> Index()
        {
            return View(await _context.CalendarioEvento.ToListAsync());
        }

        // GET: CalendarioEvento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarioEvento = await _context.CalendarioEvento
                .FirstOrDefaultAsync(m => m.id == id);
            if (calendarioEvento == null)
            {
                return NotFound();
            }

            return View(calendarioEvento);
        }

        // GET: CalendarioEvento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalendarioEvento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nomeEvento,dataEvento,quantidadeInteressados,enderecoLocalColeta,numeroEnderecoLocalColeta,entidadeColetora")] CalendarioEvento calendarioEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendarioEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calendarioEvento);
        }

        // GET: CalendarioEvento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarioEvento = await _context.CalendarioEvento.FindAsync(id);
            if (calendarioEvento == null)
            {
                return NotFound();
            }
            return View(calendarioEvento);
        }

        // POST: CalendarioEvento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nomeEvento,dataEvento,quantidadeInteressados,enderecoLocalColeta,numeroEnderecoLocalColeta,entidadeColetora")] CalendarioEvento calendarioEvento)
        {
            if (id != calendarioEvento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendarioEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarioEventoExists(calendarioEvento.id))
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
            return View(calendarioEvento);
        }

        // GET: CalendarioEvento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarioEvento = await _context.CalendarioEvento
                .FirstOrDefaultAsync(m => m.id == id);
            if (calendarioEvento == null)
            {
                return NotFound();
            }

            return View(calendarioEvento);
        }

        // POST: CalendarioEvento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendarioEvento = await _context.CalendarioEvento.FindAsync(id);
            _context.CalendarioEvento.Remove(calendarioEvento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarioEventoExists(int id)
        {
            return _context.CalendarioEvento.Any(e => e.id == id);
        }
    }
}
