using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Usuario
    {
        private String nombreCompleto;
        private String nickname;
        private String clave;
        private DateTime fechaNacimiento;
        private String ubicacionImagen;
        private ListaDoblementeEnlazada tweets_muro;
        public ListaDoblementeEnlazada getTweetsMuro()
        {
            return tweets_muro;
        }
        public void setTweetsMuro(ListaDoblementeEnlazada tweets_muro)
        {
            this.tweets_muro = tweets_muro;
        }
        public String getClave()
        {
            return clave;
        }
        public void setClave(String clave)
        {
            this.clave = Usuario.GenerarSha1(clave); ;
        }
        public String getNombreCompleto() {
            return nombreCompleto;
        }
        public void setNombreCompleto(String nombreCompleto)
        {
            this.nombreCompleto = nombreCompleto;
        }
        public String getNickname() {
            return nickname;
        }
        public void setNickname(String nickname)
        {
            this.nickname = nickname;
        }
        public DateTime getFechaNacimiento()
        {
            return fechaNacimiento;
        }
        public void setFechaNacimiento(DateTime fechaNacimiento)
        {
            this.fechaNacimiento = fechaNacimiento;
        }
        public String getUbicacionImagen()
        {
            return ubicacionImagen;
        }
        public void setUbicacionImagen(String ubicacionImagen)
        {
            this.ubicacionImagen = ubicacionImagen;
        }
        public Usuario() {
            nombreCompleto = "";
            nickname = "";
            ubicacionImagen = "";
            clave = "";
            fechaNacimiento = new DateTime(2018, 1, 1);
        }
        public Usuario(String nombreCompleto, String clave , String nickname,  String ubicacionImagen, DateTime fechaNacimiento)
        {
            this.nombreCompleto = nombreCompleto;
            this.nickname = nickname;
            this.ubicacionImagen = ubicacionImagen;
            this.fechaNacimiento = fechaNacimiento;
            this.clave = Usuario.GenerarSha1(clave);
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
