using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final_Web
{
    public class Nodo
    {
        private Nodo izquierda;
        private Nodo derecha;
        private Usuario usuario;
        public Nodo getIzquierda()
        {
            return izquierda;
        }
        public void setIzquierda(Nodo izquierda) {
            this.izquierda = izquierda;
        }
        public Nodo getDerecha()
        {
            return derecha;
        }
        public void setDerecha(Nodo derecha)
        {
            this.derecha = derecha;
        }
        public Usuario getUsuario() {
            return usuario;
        }
        public void setUsuario(Usuario usuario)
        {
            this.usuario = usuario;
        }
        public Nodo() {
            derecha = null;
            izquierda = null;
            usuario = null;
        }
        public Nodo(Nodo derecha, Nodo izquierda, Usuario usuario)
        {
            this.derecha = derecha;
            this.izquierda = izquierda;
            this.usuario = usuario;
        }
    }
}
