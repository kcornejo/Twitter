using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
        public Usuario() {
            nombreCompleto = "";
            nickname = "";
            ubicacionImagen = "";
            clave = "";
            fechaNacimiento = new DateTime(2018, 1, 1);
            seguidos = new TablaHash();
            seguidores = new TablaHash();
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
        
    }
}
