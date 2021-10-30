using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;
namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Busqueda(String valor)
        {
            if (valor == null || valor == "")
            {
                return new EmptyResult();
            }
            Arbol arbol = new Arbol();
            List<dynamic> listado_completo = arbol.listar();
            String[,] listado = new String[10, 3];
            int contador = 0;
            foreach (var item in listado_completo)
            {
                if ((item.nickname.IndexOf(valor, 0, StringComparison.CurrentCultureIgnoreCase) != -1 || item.nombreCompleto.IndexOf(valor, 0, StringComparison.CurrentCultureIgnoreCase) != -1) && contador < 10 && item.nickname != Session["Usuario"])
                {
                    listado[contador, 0] = item.nickname;
                    listado[contador, 1] = item.nombreCompleto;
                    listado[contador, 2] = item.ubicacionSinErrorImagen();
                    contador++;
                }
            }
            return View(listado);
        }
        public ActionResult Index()
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            return View(usuario);
        }
        public ActionResult Perfil()
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            return View(usuario);
        }
        public ActionResult PerfilExterno(string username)
        {
            String usuario_actual = Session["Usuario"].ToString();
            if (username == usuario_actual)
            {
                return RedirectToAction("Perfil", "Home");
            }
            Arbol arbol = new Arbol();
            Usuario usuario = arbol.obtiene_usuario(username);
            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult Seguidores(String username)
        {
            if (username == null || username == "")
            {
                username = Session["Usuario"].ToString();
            }
            Arbol arbol = new Arbol();
            Usuario usuario = arbol.obtiene_usuario(username);
            return View(usuario);
        }
        public ActionResult Seguidos(String username)
        {
            if (username == null || username == "")
            {
                username = Session["Usuario"].ToString();
            }
            Arbol arbol = new Arbol();
            Usuario usuario = arbol.obtiene_usuario(username);
            return View(usuario);
        }
        public ActionResult NuevoTuit(String mensaje)
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            Tweet tweet = new Tweet(usuario, mensaje);
            usuario.tweets_muro.insertarTweet(tweet);
            arbol.modifica_usuario(usuario);
            arbol.inserta_xml_tuits();
            return new EmptyResult();
        }
        public ActionResult EliminarTuit(String contenido, String tiempo)
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            NodoDoblementeEnlazado primero = usuario.tweets_muro.primero;
            DateTime fechaTuit = DateTime.ParseExact(tiempo, "d/M/yyyy H:m",
                                       System.Globalization.CultureInfo.InvariantCulture);
            Tweet tweet = null;
            while (primero != null)
            {
                tweet = primero.tweet;
                if (tweet != null && tweet.usuario.nickname == username && tweet.contenido == contenido && tweet.fechaHora.Year == fechaTuit.Year && tweet.fechaHora.Month == fechaTuit.Month && tweet.fechaHora.Day == fechaTuit.Day && tweet.fechaHora.Hour == fechaTuit.Hour && tweet.fechaHora.Minute == fechaTuit.Minute)
                {
                    break;
                }
                primero = primero.siguiente;

            }
            if (tweet != null)
            {
                usuario.tweets_muro.eliminar(tweet);
                arbol.modifica_usuario(usuario);
                arbol.inserta_xml_tuits();
                for (int i = 0; i < 1027; i++)
                {
                    if (usuario.seguidores.Buscar(i) != null)
                    {
                        usuario.seguidores.Buscar(i).tweets_muro.eliminar(tweet);
                        arbol.modifica_usuario(usuario.seguidores.Buscar(i));
                        arbol.inserta_xml_tuits();
                    }
                }
                Session["Mensaje_Exito"] = "Tweet Eliminado";
            }
            else
            {
                Session["Mensaje_Error"] = "Tweet no encontrado";
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ListadoTuit()
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            int tamanio = 0;
            NodoDoblementeEnlazado primero = usuario.tweets_muro.primero;
            while (primero != null)
            {
                tamanio++;
                primero = primero.siguiente;
            }
            String[,] listado = new String[tamanio, 5];
            NodoDoblementeEnlazado ultimo = usuario.tweets_muro.ultimo;
            int contador = 0;
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            List<Tweet> listado_tweet = new List<Tweet>();
            //filling the counts

            while (ultimo != null)
            {
                listado_tweet.Add(ultimo.tweet);
                ultimo = ultimo.anterior;
            }
            listado_tweet = listado_tweet.OrderByDescending(lc => lc.fechaHora).ToList();
            ultimo = usuario.tweets_muro.ultimo;
            foreach (var tweet in listado_tweet)
            {
                listado[contador, 0] = tweet.contenido;
                listado[contador, 1] = tweet.usuario.nombreCompleto;
                listado[contador, 2] = tweet.usuario.ubicacionSinErrorImagen();
                listado[contador, 3] = tweet.usuario.nickname;
                listado[contador, 4] = tweet.fechaHora.Day + "/" + tweet.fechaHora.Month + "/" + tweet.fechaHora.Year + " " + tweet.fechaHora.Hour + ":" + tweet.fechaHora.Minute;
                contador++;
            }
            string json = JsonConvert.SerializeObject(listado);
            return Content(json, "application/json");
        }
        public ActionResult ListadoTuitPropios()
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            int tamanio = 0;
            NodoDoblementeEnlazado primero = usuario.tweets_muro.primero;
            while (primero != null)
            {
                if (primero.tweet.usuario.nickname == username)
                {
                    tamanio++;
                }
                primero = primero.siguiente;

            }
            String[,] listado = new String[tamanio, 5];
            NodoDoblementeEnlazado ultimo = usuario.tweets_muro.ultimo;
            int contador = 0;
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            while (ultimo != null)
            {
                if (ultimo.tweet.usuario.nickname == username)
                {
                    listado[contador, 0] = ultimo.tweet.contenido;
                    listado[contador, 1] = ultimo.tweet.usuario.nombreCompleto;
                    string imagen = "avatar1.png";
                    if (System.IO.File.Exists(assemblyFile + "/../../Content/img/" + ultimo.tweet.usuario.ubicacionImagen))
                    {
                        imagen = ultimo.tweet.usuario.ubicacionImagen;
                    }
                    listado[contador, 2] = imagen;
                    listado[contador, 3] = ultimo.tweet.usuario.nickname;
                    listado[contador, 4] = ultimo.tweet.fechaHora.Day + "/" + ultimo.tweet.fechaHora.Month + "/" + ultimo.tweet.fechaHora.Year + " " + ultimo.tweet.fechaHora.Hour + ":" + ultimo.tweet.fechaHora.Minute;
                    contador++;
                }
                ultimo = ultimo.anterior;

            }
            string json = JsonConvert.SerializeObject(listado);
            return Content(json, "application/json");
        }
        public ActionResult ListadoTuitExternos(String username)
        {
            Arbol arbol = new Arbol();
            Usuario usuario = arbol.obtiene_usuario(username);
            if (usuario == null)
            {
                return new EmptyResult();
            }
            int tamanio = 0;
            NodoDoblementeEnlazado primero = usuario.tweets_muro.primero;
            while (primero != null)
            {
                if (primero.tweet.usuario.nickname == username)
                {
                    tamanio++;
                }
                primero = primero.siguiente;

            }
            String[,] listado = new String[tamanio, 5];
            NodoDoblementeEnlazado ultimo = usuario.tweets_muro.ultimo;
            int contador = 0;
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            while (ultimo != null)
            {
                if (ultimo.tweet.usuario.nickname == username)
                {
                    listado[contador, 0] = ultimo.tweet.contenido;
                    listado[contador, 1] = ultimo.tweet.usuario.nombreCompleto;
                    string imagen = "avatar1.png";
                    if (System.IO.File.Exists(assemblyFile + "/../../Content/img/" + ultimo.tweet.usuario.ubicacionImagen))
                    {
                        imagen = ultimo.tweet.usuario.ubicacionImagen;
                    }
                    listado[contador, 2] = imagen;
                    listado[contador, 3] = ultimo.tweet.usuario.nickname;
                    listado[contador, 4] = ultimo.tweet.fechaHora.Day + "/" + ultimo.tweet.fechaHora.Month + "/" + ultimo.tweet.fechaHora.Year + " " + ultimo.tweet.fechaHora.Hour + ":" + ultimo.tweet.fechaHora.Minute;
                    contador++;
                }
                ultimo = ultimo.anterior;

            }
            string json = JsonConvert.SerializeObject(listado);
            return Content(json, "application/json");
        }
        public ActionResult EditaUsuario()
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            return View(usuario);
        }
        [HttpPost]
        public ActionResult ConfirmaEditaUsuario(Usuario usuario)
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            HttpPostedFileBase file = usuario.imagen;

            Usuario usuario_viejo = arbol.obtiene_usuario(username);
            usuario_viejo.nombreCompleto = usuario.nombreCompleto;
            usuario_viejo.fechaNacimiento = usuario.fechaNacimiento;
            if (usuario.clave != null && !usuario.clave.Equals(""))
            {
                usuario_viejo.clave = Usuario.GenerarSha1(usuario.clave);
            }
            if (file != null)
            {
                string nameAndLocation = "~/Content/img/" + file.FileName;
                file.SaveAs(Server.MapPath(nameAndLocation));
                usuario_viejo.ubicacionImagen = file.FileName;
                Session["Imagen"] = usuario.ubicacionImagen;
            }
            arbol.modifica_usuario(usuario_viejo);
            arbol.recorre_arbol_in_orden_guardar();
            Session["Mensaje_Exito"] = "Usuario editado correctamente";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Seguir(String nickname)
        {
            Arbol arbol = new Arbol();
            String username = Session["Usuario"].ToString();
            Usuario usuario = arbol.obtiene_usuario(username);
            Usuario usuario_seguir = arbol.obtiene_usuario(nickname);
            if (usuario != null && usuario_seguir != null)
            {
                if (usuario.busca_seguidos(usuario_seguir))
                {
                    usuario.seguidos.Eliminar(usuario_seguir.nickname.GetHashCode());
                    usuario_seguir.seguidores.Eliminar(usuario.nickname.GetHashCode());
                    Session["Mensaje_Exito"] = "Usuario eliminado de amigos";
                }
                else
                {
                    usuario.seguidos.Insertar(usuario_seguir, usuario_seguir.nickname.GetHashCode());
                    usuario_seguir.seguidores.Insertar(usuario, usuario.nickname.GetHashCode());
                    Session["Mensaje_Exito"] = "Usuario seguido correctamente";
                }

                arbol.modifica_usuario(usuario);
                arbol.modifica_usuario(usuario_seguir);
                arbol.recorre_arbol_in_orden_seguidores();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}