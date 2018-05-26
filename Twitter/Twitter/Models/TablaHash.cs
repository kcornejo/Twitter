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
        Usuario[] tabla = new Usuario[M];

        public int HashMod(int x)
        {
            return x % M;
        }

        public void Insertar(Usuario Dato, int Clave)
        {
            if(Clave < 0)
            {
                Clave = Clave * -1;
            }
            Posicion = HashMod(Clave);
            if(Posicion < 1027)
            {
                tabla[Posicion] = Dato;
            }
        }

        public void Eliminar(int Clave)
        {
            if (Clave < 0)
            {
                Clave = Clave * -1;
            }
            Posicion = HashMod(Clave);
            if (Posicion < 1027)
                tabla[Posicion] = null;
        }

        public Usuario Buscar(int Clave)
        {
            if (Clave < 0)
            {
                Clave = Clave * -1;
            }
            Posicion = HashMod(Clave);
            if (Posicion < 1027)
                return tabla[Posicion];
            return null;
        }
    }
}