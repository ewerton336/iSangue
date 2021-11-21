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
    public class CalendarioEventoController : Controller
    {
        private readonly iSangueContext _context;
        private CalendarioEventoDao calendarioEventoDao;
        private EntidadeColetoraDao entidadeColetoraDao;
        private CedenteLocalDao cedenteLocalDao;
        private DoadorDao doadorDao;

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

        EntidadeColetoraDao EntidadeColetora
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
        CedenteLocalDao CedenteLocal
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


        // GET: CalendarioEvento
        public async Task<IActionResult> Index()
        {

            IEnumerable<CalendarioEvento> eventos = await CalendarioEvento.GetCalendariosEventos();
            foreach (var evento in eventos)
            {
                evento.quantidadeInteressados = await Doador.GetQuantidadeDoadoresEvento(evento.id);
                evento.entidadeColetora = await EntidadeColetora.GetEntidadeById(evento.entidadeColetoraID);
                evento.cedenteLocal = await CedenteLocal.GetCedenteById(evento.cedenteLocalID);
            }
            
            return View(eventos);
        }


        //GET: ListarEventos
        public async Task<IActionResult> ListarEventos()
        {

            IEnumerable<CalendarioEvento> eventos = await CalendarioEvento.GetCalendariosEventos();
            foreach (var evento in eventos)
            {
                evento.quantidadeInteressados = await Doador.GetQuantidadeDoadoresEvento(evento.id);
                evento.entidadeColetora = await EntidadeColetora.GetEntidadeById(evento.entidadeColetoraID);
                evento.cedenteLocal = await CedenteLocal.GetCedenteById(evento.cedenteLocalID);
            }
            return View(eventos);
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


        public async Task<IActionResult> DetalhesEvento(int id)
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
            ViewData["cedentes"] = new CedenteLocalDao(Helper.DBConnectionSql).GetCedenteLocals().Result;
            ViewData["entidades"] = new EntidadeColetoraDao(Helper.DBConnectionSql).GetEntidades().Result;
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

        // GET: CalendarioEvento/InscricaoEvento
        public async Task <IActionResult> InscricaoEvento(int id)
        {
            var tipoUsuario = HttpContext.Session.GetString("TIPO_USUARIO") == null ? "" : HttpContext.Session.GetString("TIPO_USUARIO");
            if (tipoUsuario.Equals("DOADOR"))
            {
                var evento = await CalendarioEvento.GetEventoById(id);

                if (evento == null)
                {
                    return NotFound();
                }

                return View(evento);
            }
            else
            {
                return RedirectToAction("Login", "Usuario");
            }
        }


        // POST: CalendarioEvento/InscricaoEvento
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> InscricaoEvento([Bind("id, cedenteLocalID")] CalendarioEvento evento)
        {
            //o id do cedenteLocal é o id do doador

            await Doador.CadastradrDoadorNoEvento(evento.cedenteLocalID, evento.id);
            return View("CadastradoNoEvento");
        }




        // GET: CalendarioEvento/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["cedentes"] = new CedenteLocalDao(Helper.DBConnectionSql).GetCedenteLocals().Result;
            ViewData["entidades"] = new EntidadeColetoraDao(Helper.DBConnectionSql).GetEntidades().Result;
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
            await CalendarioEvento.DeletarEvento(calendarioEvento.id); ;
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarioEventoExists(int id)
        {
            return _context.CalendarioEvento.Any(e => e.id == id);
        }
    }
}
