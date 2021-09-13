using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteFatec.Controllers
{
    public class CalendarioEventoController : Controller
    {
        // GET: CalendarioEventoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CalendarioEventoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalendarioEventoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalendarioEventoController/Create
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

        // GET: CalendarioEventoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalendarioEventoController/Edit/5
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

        // GET: CalendarioEventoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalendarioEventoController/Delete/5
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
