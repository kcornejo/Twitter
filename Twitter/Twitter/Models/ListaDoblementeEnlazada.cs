using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class ListaDoblementeEnlazada
    {
        private NodoDoblementeEnlazado primero;
        private NodoDoblementeEnlazado ultimo;
        public void setPrimero(NodoDoblementeEnlazado primero)
        {
            this.primero = primero;
        }
        public NodoDoblementeEnlazado getPrimero()
        {
            return primero;
        }
        public void setUltimo(NodoDoblementeEnlazado ultimo)
        {
            this.ultimo = ultimo;
        }
        public NodoDoblementeEnlazado getUltimo()
        {
            return ultimo;
        }
        public void insertar(NodoDoblementeEnlazado nodo)
        {
            nodo.setAnterior(ultimo);
            ultimo.setSiguiente(nodo);
            ultimo = nodo;
        }
        public void eliminar(Tweet tweet)
        {
            primero = this.EliminarNodo(primero, tweet);
        }
        public NodoDoblementeEnlazado EliminarNodo(NodoDoblementeEnlazado nodo, Tweet tweet)
        {
            if(nodo.getTweet().getContenido() == tweet.getContenido() && nodo.getTweet().getFechaHora() == tweet.getFechaHora())
            {
                nodo = nodo.getSiguiente();
                if(nodo.getAnterior() != null)
                {
                    nodo.getAnterior().setSiguiente(nodo);
                }
            }else {
                nodo.setSiguiente(EliminarNodo(nodo.getSiguiente(), tweet));
            }
            return nodo;
        }
    }
}