using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Comparador
    {
        public static Boolean menor_que(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.getNickname(), comparador2.getNickname(), ignoreCase: true);
            return numero < 0;
        }
        public static Boolean mayor_que(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.getNickname(), comparador2.getNickname(), ignoreCase: true);
            return numero > 0;
        }
        public static Boolean iguales(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.getNickname(), comparador2.getNickname(), ignoreCase: true);
            return numero == 0;
        }
    }
}
