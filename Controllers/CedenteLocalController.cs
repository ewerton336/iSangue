using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Controllers
{
    public class CedenteLocalController : Controller
    {
        // GET: CedenteLocalController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CedenteLocalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CedenteLocalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CedenteLocalController/Create
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

        // GET: CedenteLocalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CedenteLocalController/Edit/5
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

        // GET: CedenteLocalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CedenteLocalController/Delete/5
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
