using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteFatec.Controllers
{
    public class EntidadeColetoraController : Controller
    {
        // GET: EntidadeColetoraController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EntidadeColetoraController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntidadeColetoraController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntidadeColetoraController/Create
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

        // GET: EntidadeColetoraController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntidadeColetoraController/Edit/5
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

        // GET: EntidadeColetoraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntidadeColetoraController/Delete/5
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
