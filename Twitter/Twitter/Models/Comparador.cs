using System;

namespace Twitter.Models
{
    public class Comparador
    {
        public static Boolean menor_que(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.nickname.ToUpper(), comparador2.nickname.ToUpper(), ignoreCase: true);
            return numero < 0;
        }
        public static Boolean mayor_que(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.nickname.ToUpper(), comparador2.nickname.ToUpper(), ignoreCase: true);
            return numero > 0;
        }
        public static Boolean iguales(Usuario comparador, Usuario comparador2)
        {
            int numero;
            numero = String.Compare(comparador.nickname.ToUpper(), comparador2.nickname.ToUpper(), ignoreCase: true);
            return numero == 0;
        }
    }
}
