using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class ListaDoblementeEnlazada
    {
        public NodoDoblementeEnlazado primero { get; set; }
        public NodoDoblementeEnlazado ultimo { get; set; }

        public ListaDoblementeEnlazada()
        {
            primero = null;
            ultimo = null;
        }
       
        public void insertar(NodoDoblementeEnlazado nodo)
        {
            if(primero == null || ultimo == null)
            {
                primero = nodo;
                ultimo = nodo;
            }
            else
            {
                nodo.anterior = ultimo;
                nodo.anterior.siguiente = nodo;
                ultimo = nodo;
            }
        }
        public void insertarTweet(Tweet tweet)
        {
            NodoDoblementeEnlazado nodo = new NodoDoblementeEnlazado();
            nodo.tweet = tweet;
            this.insertar(nodo);
            for (int i = 0; i <= 1026; i++)
            {
                if (tweet.usuario.seguidores.Buscar(i) != null)
                {
                    tweet.usuario.seguidores.Buscar(i).tweets_muro.insertarTweetOtro(tweet);
                }
            }
        }
        public void insertarTweetOtro(Tweet tweet)
        {
            NodoDoblementeEnlazado nodo = new NodoDoblementeEnlazado();
            nodo.tweet = tweet;
            this.insertar(nodo);
        }
        public void eliminar(Tweet tweet)
        {
            primero = this.EliminarNodo(primero, tweet);
        }
        public NodoDoblementeEnlazado EliminarNodo(NodoDoblementeEnlazado nodo, Tweet tweet)
        {
            if(nodo.tweet.contenido == tweet.contenido && nodo.tweet.fechaHora == tweet.fechaHora)
            {
                nodo = nodo.siguiente;
                if(nodo.anterior != null)
                {
                    nodo.anterior.siguiente = nodo;
                }
            }else {
                nodo.siguiente = EliminarNodo(nodo.siguiente, tweet);
            }
            return nodo;
        }
    }
}