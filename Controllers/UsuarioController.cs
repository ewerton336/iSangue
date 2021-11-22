using iSangue.DAO;
using iSangue.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iSangue.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioDao usuarioDao;
        private DoadorDao doadorDao;

        UsuarioDao Usuario
        {
            get
            {
                if (usuarioDao == null)
                {
                    usuarioDao = new UsuarioDao(Helper.DBConnectionSql);
                }
                return usuarioDao;
            }
            set
            {
                usuarioDao = value;
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
        // GET: UsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return Redirect("../Home/Index"); ;
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
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


        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("email,senha")] Usuario usuario)
        {
            var login = await Usuario.LoginUsuario(usuario.email, usuario.senha);
            if (login != null)
            {
                //return RedirectToAction(nameof(LoginSucess));
                return await LoginSucess(login);
            }
            else
            {
                return RedirectToAction(nameof(LoginError));
            }
        }



        public async Task<IActionResult> LoginError()
        {
            return View();
        }

        public async Task<IActionResult> LoginSucess(Usuario usuario)
        {
            
            switch (usuario.tipoUsuario)
            {
                case "DOADOR":
                    var doador  = await Doador.GetDoadorByUserID(usuario.id);
                    HttpContext.Session.SetString("ID_DOADOR", doador.idDoador.ToString());
                    break;
            }


            ViewBag.nome = "Indefinido";
            
            var nome = await Usuario.GetNomeByUserId(usuario.id, usuario.tipoUsuario);

            HttpContext.Session.SetString("TIPO_USUARIO", usuario.tipoUsuario);
            HttpContext.Session.SetString("NOME_USUARIO", nome);
            HttpContext.Session.SetString("EMAIL_USUARIO", usuario.email);
            HttpContext.Session.SetString("ID_USUARIO", usuario.id.ToString());

            return View("LoginSucess");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("TIPO_USUARIO");
            HttpContext.Session.Remove("NOME_USUARIO");
            HttpContext.Session.Remove("EMAIL_USUARIO");
            HttpContext.Session.Remove("ID_USUARIO");
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> ErrorUsuarioJaExistente()
        {
            return View();
        }


    }
}
