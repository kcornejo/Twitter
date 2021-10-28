using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Twitter.Controllers;
using Twitter.Models;

namespace PruebasUnitarias
{
    [TestClass]
    public class TwitterTest
    {
        private HomeController _homecontroller;
        private UsuarioController _usuariocontroller;
        private Arbol _arbol;
        public TwitterTest()
        {
            _homecontroller = new HomeController();
            _usuariocontroller = new UsuarioController();
            _arbol = new Arbol();
        }
        [TestMethod]
        public void CreacionUsuario()
        {
            Random rnd = new Random();
            int int_usuario = rnd.Next(10000, 30000);
            String _username = "Usuario" + int_usuario;
            Usuario objeto_usuario = new Usuario();
            objeto_usuario.nickname = _username;
            objeto_usuario.clave = _username;
            _arbol.agregar_usuario(objeto_usuario);
            Usuario valida = _arbol.valida_sesion(_username, _username);
            if(valida != null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false, "Error en creacion de usuario");
            }
        }
        [TestMethod]
        public void EliminarUsuario()
        {
            Random rnd = new Random();
            int int_usuario = rnd.Next(10000, 30000);
            String _username = "Usuario" + int_usuario;
            Usuario objeto_usuario = new Usuario();
            objeto_usuario.nickname = _username;
            objeto_usuario.clave = _username;
            _arbol.agregar_usuario(objeto_usuario);
            Usuario valida = _arbol.valida_sesion(_username, _username);
            if (valida == null)
            {
                Assert.IsTrue(false, "Error en creacion de usuario");
            }
            Usuario usuario = _arbol.obtiene_usuario(_username);
            if (usuario != null)
            {
                _arbol.eliminar_nodo(usuario);
                _arbol.recorre_arbol_in_orden_guardar();
                Usuario valida2 = _arbol.valida_sesion(_username, _username);
                if (valida2 == null)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false, "Error en eliminar usuario");
                }
            }
        }
        [TestMethod]
        public void PruebaBitacora()
        {
            try
            {
                _arbol.inserta_texto("Prueba de bitacora");
                Assert.IsTrue(true);
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Assert.IsTrue(false, "Error en escribir en bitacora");
            }
           
        }
        [TestMethod]
        public void PruebaSeguir()
        {
            //Agregar usuario 1
            Random rnd = new Random();
            int int_usuario = rnd.Next(10000, 30000);
            String _username = "Usuario" + int_usuario;
            Usuario objeto_usuario = new Usuario();
            objeto_usuario.nickname = _username;
            objeto_usuario.clave = _username;
            _arbol.agregar_usuario(objeto_usuario);
            //Agregar usuario 2
            String _username2 = "Usuario2" + int_usuario;
            Usuario objeto_usuario2 = new Usuario();
            objeto_usuario2.nickname = _username2;
            objeto_usuario2.clave = _username2;
            _arbol.agregar_usuario(objeto_usuario2);
            objeto_usuario.seguidos.Insertar(objeto_usuario2, objeto_usuario2.nickname.GetHashCode());
            objeto_usuario2.seguidores.Insertar(objeto_usuario, objeto_usuario.nickname.GetHashCode());
            if (objeto_usuario.busca_seguidos(objeto_usuario2))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false, "Error al seguir usuario");
            }
        }
        [TestMethod]
        public void PruebaTwitear()
        {
            //Agregar usuario 1
            Random rnd = new Random();
            int int_usuario = rnd.Next(10000, 30000);
            String _username = "Usuario" + int_usuario;
            Usuario objeto_usuario = new Usuario();
            objeto_usuario.nickname = _username;
            objeto_usuario.clave = _username;
            _arbol.agregar_usuario(objeto_usuario);
            //Twitear
            Tweet tweet = new Tweet(objeto_usuario, "Prueba MSG");
            objeto_usuario.tweets_muro.insertarTweet(tweet);
            _arbol.modifica_usuario(objeto_usuario);
            _arbol.inserta_xml_tuits();
            //Revisar tweet
            NodoDoblementeEnlazado primero = objeto_usuario.tweets_muro.primero;
            if(primero.tweet.contenido.ToString() == "Prueba MSG")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false, "Error al twittear");
            }
        }
    }
}
