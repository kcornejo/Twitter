using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ActionResult ValidaRegistro(Usuario usuario) {
            Arbol arbol = new Arbol();
            if (arbol.obtiene_usuario(usuario.nickname) == null)
            {
                Session["Mensaje_Exito"] = "Usuario creado correctamente";
            }
            else
            {
                Session["Mensaje_Error"] = "Usuario no existente";
            }
            arbol.agregar_usuario(usuario);
            return RedirectToAction("Login", "Usuario");
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
            if (valida != null)
            {
                usuario = arbol.obtiene_usuario(usuario.nickname);
                Session["Usuario"] = usuario.nickname;
                Session["Imagen"] = usuario.ubicacionSinErrorImagen();
                Session["Mensaje_Exito"] = "Acceso correcto";
                return RedirectToAction("Index", "Home");
            }
            Session["Mensaje_Error"] = "Problema de credenciales";
            return RedirectToAction("Login", "Usuario");
        }
        public ActionResult EliminaUsuario() {
            Arbol arbol = new Arbol();
            Usuario usuario = arbol.obtiene_usuario(Session["Usuario"].ToString());
            if (usuario != null)
            {
                arbol.eliminar_nodo(usuario);
                arbol.recorre_arbol_in_orden_guardar();
            }
            Session["Usuario"] = null;
            Session["Mensaje_Exito"] = "Usuario eliminado correctamente";
            return RedirectToAction("Login", "Usuario");
        }
    }
}