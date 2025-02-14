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
using System.Net.Mail;
using System.Net;

namespace iSangue.Controllers
{
    public class CalendarioEventoController : Controller
    {
        private readonly iSangueContext _context;
        private CalendarioEventoDao calendarioEventoDao;
        private EntidadeColetoraDao entidadeColetoraDao;
        private CedenteLocalDao cedenteLocalDao;
        private DoadorDao doadorDao;
        private string usuario;

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

        // GET: CalendarioEvento
        public async Task<IActionResult> Index()
        {
            if (usuarioSession == "ADMINISTRADOR")
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
            else return NotFound();
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
            else
            {
                evento.quantidadeInteressados = await Doador.GetQuantidadeDoadoresEvento(evento.id);
                evento.entidadeColetora = await EntidadeColetora.GetEntidadeById(evento.entidadeColetoraID);
                evento.cedenteLocal = await CedenteLocal.GetCedenteById(evento.cedenteLocalID);
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
        public async Task<IActionResult> InscricaoEvento(int id)
        {
            var tipoUsuario = HttpContext.Session.GetString("TIPO_USUARIO") == null ? "" : HttpContext.Session.GetString("TIPO_USUARIO");
            if (tipoUsuario.Equals("DOADOR"))
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
                else
                {
                    evento.quantidadeInteressados = await Doador.GetQuantidadeDoadoresEvento(evento.id);
                    evento.entidadeColetora = await EntidadeColetora.GetEntidadeById(evento.entidadeColetoraID);
                    evento.cedenteLocal = await CedenteLocal.GetCedenteById(evento.cedenteLocalID);
                    return View(evento);
                }
                
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
            return View("CadastradoEventoSucesso");
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


        // GET: CalendarioEvento/DispararEmails/5
        public async Task<IActionResult> DispararEmails(int id)
        {
           var doadores = await Doador.GetDoadoresEvento(id);

            if (doadores == null)
            {
                return NotFound();
            }
            ViewBag.idEvento = id;
            return View(doadores);
        }

        // POST: CalendarioEvento/DispararEmails/5
        [HttpPost, ActionName("DispararEmails")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EvniarEmailsUsuarios(int id)
        {
            var doadores = await Doador.GetDoadoresEvento(id);

            #region configurações do email

            //cria uma mensagem
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("contato.isangue@outlook.com")
            };

            //adiciona o email dos doadores interessados na lista de emails a enviar
            foreach (var doador in doadores)
            {
                mail.To.Add(doador.email);
            }
            var detalhesEvento = await CalendarioEvento.GetEventoById(id);
            detalhesEvento.cedenteLocal = await CedenteLocal.GetCedenteById(detalhesEvento.cedenteLocalID);
            detalhesEvento.entidadeColetora = await EntidadeColetora.GetEntidadeById(detalhesEvento.entidadeColetoraID);
            mail.Subject = "Doação de Sangue - Evento confirmado com data de Coleta Definida!";
            mail.Body = @$"Olá! Estamos muito felizes em informar que em breve haverá coleta no evento de Doação de sangue que você está inscrito(a). <br>
            Segue os detalhes do evento <br>
            Data: {detalhesEvento.dataEvento};
            Local: {detalhesEvento.cedenteLocal.nome} <br>
            Endereço: {detalhesEvento.cedenteLocal.endereco}
            Entidade Coletora: {detalhesEvento.entidadeColetora.nome} <br>
            Obs: Não se esqueça de comparecer na data informada munido de documentos. <br>
            Obrigado por fazer parte da comunidade iSangue!";
            mail.IsBodyHtml = true;


            //configurações stmp
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);
          
            // inclui as credenciais
            client.UseDefaultCredentials = false;
            NetworkCredential cred = new NetworkCredential("contato.isangue@outlook.com", "isangue123");
            client.EnableSsl = true;
            client.Credentials = cred;

            await client.SendMailAsync(mail);

            #endregion
            return RedirectToAction(nameof(EmailSucesso));
        }


        // GET: CalendarioEvento/DispararEmails/5
        public async Task<IActionResult> EmailSucesso()
        {
            return View();
        }
    }
}
