using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Twitter.Models
{
    public class Usuario
    {
        
        public String nombreCompleto { get; set; }
        public String nickname { get; set; }
        public String clave { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public String ubicacionImagen { get; set; }
        public ListaDoblementeEnlazada tweets_muro { get; set; }
        public TablaHash seguidos { get; set; }
        public TablaHash seguidores { get; set; }
        public HttpPostedFileBase imagen { get; set; }
        public String ubicacionSinErrorImagen() {
            string assemblyFile = (new System.Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            string imagen = "avatar1.png";
            if (System.IO.File.Exists(assemblyFile + "/../../Content/img/" + this.ubicacionImagen))
            {
                imagen = this.ubicacionImagen;
            }
            return imagen;
        }
        public Usuario() {
            nombreCompleto = "";
            nickname = "";
            ubicacionImagen = "";
            clave = "";
            fechaNacimiento = new DateTime(2018, 1, 1);
            seguidos = new TablaHash();
            seguidores = new TablaHash();
            tweets_muro = new ListaDoblementeEnlazada();
        }
        public Usuario(String nombreCompleto, String clave, String nickname, String ubicacionImagen, DateTime fechaNacimiento)
        {
            this.nombreCompleto = nombreCompleto;
            this.nickname = nickname;
            this.ubicacionImagen = ubicacionImagen;
            this.fechaNacimiento = fechaNacimiento;
            this.clave = clave;
            this.seguidos = new TablaHash();
            this.seguidores = new TablaHash();
            this.tweets_muro = new ListaDoblementeEnlazada();
        }
        public Usuario(String nombreCompleto, String clave , String nickname,  String ubicacionImagen, DateTime fechaNacimiento, TablaHash seguidores, TablaHash seguidos)
        {
            this.nombreCompleto = nombreCompleto;
            this.nickname = nickname;
            this.ubicacionImagen = ubicacionImagen;
            this.fechaNacimiento = fechaNacimiento;
            this.clave = clave;
            this.seguidores = seguidores;
            this.seguidos = seguidos;
            this.tweets_muro = new ListaDoblementeEnlazada();
        }
        public static string GenerarSha1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public int cantidadTweets() {
            int cantidad = 0;
            NodoDoblementeEnlazado nodo = tweets_muro.primero;
            while(nodo != null)
            {
                if(nodo.tweet.usuario.nickname == this.nickname)
                {
                    cantidad++;
                }
                nodo = nodo.siguiente;
            }
            return cantidad;
        }
        public int cantidadSeguidores()
        {
            int cantidad = 0;
            for(int i = 0; i < 1027; i++)
            {
                if(seguidores.Buscar(i) != null)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }
        public int cantidadSeguidos()
        {
            int cantidad = 0;
            for (int i = 0; i < 1027; i++)
            {
                if (seguidos.Buscar(i) != null)
                {
                    cantidad++;
                }
            }
            return cantidad;
        }

        public Usuario[] getListaSeguidores() {
            Usuario[] lista = new Usuario[this.cantidadSeguidores()];
            int contador = 0;
            for(int i = 0; i < 1027; i++)
            {
                if(this.seguidores.Buscar(i) != null)
                {
                    lista[contador] = this.seguidores.Buscar(i);
                    contador++;
                }
            }
            return lista;
        }
        public Usuario[] getListaSeguidos()
        {
            Usuario[] lista = new Usuario[this.cantidadSeguidos()];
            int contador = 0;
            for (int i = 0; i < 1027; i++)
            {
                if (this.seguidos.Buscar(i) != null)
                {
                    lista[contador] = this.seguidos.Buscar(i);
                    contador++;
                }
            }
            return lista;
        }
        public Boolean busca_seguidos(Usuario usuario)
        {
            Boolean sigue = false;
            if (this.seguidos.Buscar(usuario.nickname.GetHashCode()) != null)
            {
                sigue = true;
            }
            return sigue;
        }
        public Boolean busca_seguidores(Usuario usuario)
        {
            Boolean sigue = false;
            if (this.seguidores.Buscar(usuario.nickname.GetHashCode()) != null)
            {
                sigue = true;
            }
            return sigue;
        }
        public Usuario[] recomendaciones() {
            Usuario[] recomendaciones = new Usuario[4];
            int contador = 0;
            foreach (var item in this.getListaSeguidores())
            {
                if (!this.busca_seguidos(item) && item.nickname != this.nickname)
                {
                    recomendaciones[contador] = item;
                    contador++;
                    if(contador == 4)
                    {
                        break;
                    }
                }
            }
            if(contador < 4)
            {
                foreach (var item in this.getListaSeguidos())
                {
                    foreach(var item_item in item.getListaSeguidos())
                    {

                        if (!this.busca_seguidos(item_item) && contador < 4 && item_item.nickname != this.nickname)
                        {
                            recomendaciones[contador] = item_item;
                            contador++;
                            if (contador == 4)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if(contador < 4)
            {
                Arbol arbol = new Arbol();
                List <dynamic> lista = arbol.listar();
                foreach(var item in lista)
                {
                    if(item.nickname != this.nickname && !this.busca_seguidos(item))
                    {
                        recomendaciones[contador] = item;
                        contador++;
                        if (contador == 4)
                        {
                            break;
                        }
                    }
                    
                }
            }
            return recomendaciones;
        }
    }
}
