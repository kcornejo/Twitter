using System;

namespace Twitter.Models
{
    public class Tweet
    {
        public Usuario usuario { get; set; }
        public String contenido { get; set; }
        public DateTime fechaHora { get; set; }
        public Tweet()
        {
            usuario = null;
            contenido = "";
            fechaHora = DateTime.Now;
        }
        public Tweet(Usuario usuario, String contenido)
        {
            this.usuario = usuario;
            this.contenido = contenido;
            this.fechaHora = DateTime.Now;
        }
    }
}