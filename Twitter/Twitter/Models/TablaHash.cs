using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class TablaHash
    {
        public static readonly int M = 1027;

        int Posicion;
        Object[] tabla = new Object[M];

        public int HashMod(int x)
        {
            return x % M;
        }

        public void Insertar(Object Dato, int Clave)
        {
            Posicion = HashMod(Clave);
            tabla[Posicion] = Dato;
        }

        public void Eliminar(int Clave)
        {
            Posicion = HashMod(Clave);
            tabla[Posicion] = null;
        }

        public object Buscar(int Clave)
        {
            Posicion = HashMod(Clave);
            return tabla[Posicion];
        }
    }
}