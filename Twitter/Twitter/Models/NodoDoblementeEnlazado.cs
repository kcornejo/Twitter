using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class NodoDoblementeEnlazado
    {
        private NodoDoblementeEnlazado siguiente;
        private NodoDoblementeEnlazado anterior;
        private Tweet tweet;
        public void setSiguiente(NodoDoblementeEnlazado siguiente)
        {
            this.siguiente = siguiente;
        }
        public NodoDoblementeEnlazado getSiguiente() {
            return siguiente;
        }
        public void setAnterior(NodoDoblementeEnlazado anterior)
        {
            this.anterior = anterior;
        }
        public NodoDoblementeEnlazado getAnterior()
        {
            return anterior;
        }
        public void setTweet(Tweet tweet)
        {
            this.tweet = tweet;
        }
        public Tweet getTweet()
        {
            return tweet;
        }
    }
}