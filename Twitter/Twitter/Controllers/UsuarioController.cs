using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;

namespace Twitter.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        // GET: Usuario
        public ActionResult Registro()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ValidaLogin(Usuario usuario )
        {
            Session["test"] = "123";
            usuario.clave = "123";
            return RedirectToAction("Index", "Home");
        }
    }
}