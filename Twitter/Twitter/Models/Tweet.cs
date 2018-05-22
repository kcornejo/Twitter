using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class Tweet
    {
        private Usuario usuario;
        private String contenido;
        private DateTime fechaHora;
        public void setUsuario(Usuario usuario)
        {
            this.usuario = usuario;
        }
        public Usuario getUsuario()
        {
            return usuario;
        }
        public void setContenido(String contenido)
        {
            this.contenido = contenido;
        }
        public String getContenido()
        {
            return contenido;
        }
        public void setFechaHora(DateTime fechaHora)
        {
            this.fechaHora = fechaHora;
        }
        public DateTime getFechaHora()
        {
            return fechaHora;
        }
    }
}