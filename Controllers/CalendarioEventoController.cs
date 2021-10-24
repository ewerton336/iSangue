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
    public class CalendarioEventoController : Controller
    {
        private readonly iSangueContext _context;
        private CalendarioEventoDao calendarioEventoDao;

        public CalendarioEventoController(iSangueContext context)
        {
            _context = context;
        }

        CalendarioEventoDao CalendarioEvento
        {
            get
            {
                if (calendarioEventoDao == null)
                {
                    calendarioEventoDao = new CalendarioEventoDao(Helper.DBConnectionSql);
                }
                return calendarioEventoDao;
            }
            set
            {
                calendarioEventoDao = value;
            }
        }


        // GET: CalendarioEvento
        public async Task<IActionResult> Index()
        {
            return View(await CalendarioEvento.GetCalendariosEventos());
        }

        // GET: CalendarioEvento/Details/5
        public async Task<IActionResult> Details(int id)
        {
           if (id == 0)
            {
                return NotFound();
            }

            var evento = await CalendarioEvento.GetEventoById(id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: CalendarioEvento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalendarioEvento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nomeEvento,dataEvento,quantidadeInteressados,entidadeColetoraID,cedenteLocalID")] CalendarioEvento calendarioEvento)
        {
            if (ModelState.IsValid)
            {
                await CalendarioEvento.InserirEvento(calendarioEvento);
                return RedirectToAction(nameof(Index));
            }
            return View(calendarioEvento);
        }

        // GET: CalendarioEvento/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var evento = await CalendarioEvento.GetEventoById(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: CalendarioEvento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nomeEvento,dataEvento,quantidadeInteressados,entidadeColetoraID,cedenteLocalID")] CalendarioEvento calendarioEvento)
        {
            if (id != calendarioEvento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await CalendarioEvento.AtualizarEvento(calendarioEvento);
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
        public async Task<IActionResult> Delete(int id)
        {

            var calendarioEvento = await CalendarioEvento.GetEventoById(id);
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
            var calendarioEvento = await CalendarioEvento.GetEventoById(id);
            await CalendarioEvento.DeletarEvento(calendarioEvento.id);;
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarioEventoExists(int id)
        {
            return _context.CalendarioEvento.Any(e => e.id == id);
        }
    }
}
