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
        public ActionResult Login(String mensaje)
        {
            return View(mensaje);
        }
        public ActionResult Logout()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Login", "Usuario");
        }
        [HttpPost]
        public ActionResult ValidaLogin(Usuario usuario )
        {
            Arbol arbol = new Arbol();
            Usuario valida = arbol.valida_sesion(usuario.nickname, usuario.clave);
            Usuario nuevo_usuario = new Usuario("admin", Usuario.GenerarSha1("admin.12"), "admin", "", new DateTime());
            arbol.insertar(nuevo_usuario);
            arbol.recorre_arbol_in_orden_guardar();
            if (valida != null)
            {
                Session["Usuario"] = usuario.nickname;
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login", "Usuario");
        }
    }
}