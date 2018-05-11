using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final_Web
{
    public class Comparador
    {
        public static Boolean MenorQue(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.getNickname(), comparador2.getNickname(), ignoreCase: true);
            return numero < 0;
        }
        public static Boolean MayorQue(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.getNickname(), comparador2.getNickname(), ignoreCase: true);
            return numero > 0;
        }
        public static Boolean IgualQue(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.getNickname(), comparador2.getNickname(), ignoreCase: true);
            return numero == 0;
        }
    }
}
