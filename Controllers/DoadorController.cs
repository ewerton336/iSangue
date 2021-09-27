using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiteFatec.Controllers;
namespace SiteFatec.DAO;
{
    public class DoadorController : Controller
    {
        // GET: DoadorController
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: DoadorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DoadorController/Create
        public ActionResult Create(Models.Doador doador)
        {
            new UsuarioDao(Helper.DBConnectionSql).InserirUsuario(doador.email, doador.senha);
            new DAO.DoadorDao(Helper.DBConnectionSql).InserirDoador(doador);
            return View();
        }

        // POST: DoadorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoadorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DoadorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DoadorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DoadorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
