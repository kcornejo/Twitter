using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Arbol
    {
        private Nodo nodo_raiz;

        public List<dynamic> listar() {
            var lista = new List<dynamic>();
            lista = InOrden(nodo_raiz, lista);
            return lista;
        }
        public List<dynamic> InOrden(Nodo nodo, List<dynamic> listado_usuario) {
            if (nodo != null && nodo.getUsuario() != null)
            {
                if (nodo.getIzquierda() != null)
                {
                    listado_usuario = InOrden(nodo.getIzquierda(), listado_usuario);
                }
                listado_usuario.Add(nodo.getUsuario());
                if (nodo.getDerecha() != null)
                {
                    listado_usuario = InOrden(nodo.getDerecha(), listado_usuario);
                }
            }
            return listado_usuario;
        }

        public void insertar(Usuario usuario) {
            nodo_raiz = InsertarValor(nodo_raiz, usuario);
        }
        public Nodo InsertarValor(Nodo nodo, Usuario usuario) {
            if (nodo == null)
            {
                nodo = new Nodo();
                nodo.setUsuario(usuario);
            } else if (Comparador.menor_que(usuario, nodo.getUsuario()))
            {
                Nodo izquierda;
                izquierda = InsertarValor(nodo.getIzquierda(), usuario);
                nodo.setIzquierda(izquierda);
            }
            else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
            {
                Nodo derecha;
                derecha = InsertarValor(nodo.getDerecha(), usuario);
                nodo.setDerecha(derecha);
            }
            else
            {
                //IGUALES
            }
            return nodo;
        }
        public void eliminar_nodo(Usuario usuario) {
            nodo_raiz = BusquedaEliminarNodo(nodo_raiz, usuario);
        }
        public Nodo BusquedaEliminarNodo(Nodo nodo, Usuario usuario)
        {
            if (nodo == null)
            {
                //NO EXISTE
            }
            else if (Comparador.iguales(usuario, nodo.getUsuario()))
            {
                nodo = RemplazoNodo(nodo);
            }
            else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
            {
                Nodo derecha;
                derecha = BusquedaEliminarNodo(nodo.getDerecha(), usuario);
                nodo.setDerecha(derecha);
            }
            else if (Comparador.menor_que(usuario, nodo.getUsuario()))
            {
                Nodo izquierda;
                izquierda = BusquedaEliminarNodo(nodo.getIzquierda(), usuario);
                nodo.setIzquierda(izquierda);

            }
            return nodo;
        }
        public Nodo RemplazoNodo(Nodo nodo)
        {
            if (nodo.getIzquierda() == null && nodo.getDerecha() != null)
            {
                nodo = nodo.getDerecha();
            } else if (nodo.getDerecha() == null && nodo.getIzquierda() != null)
            {
                nodo = nodo.getIzquierda();
            }
            else if (nodo.getDerecha() == null && nodo.getIzquierda() == null)
            {
                nodo = null;
            }
            else
            {
                Nodo nodo_menor = BusquedaMenor(nodo.getDerecha(), nodo.getDerecha());
                Nodo nodo_nuevo = RemplazoMenor(nodo.getDerecha(), nodo_menor);
                nodo_menor.setIzquierda(nodo.getIzquierda());
                nodo_menor.setDerecha(nodo_nuevo);
                nodo = nodo_menor;
            }
            return nodo;
        }
        public Nodo RemplazoMenor(Nodo nodo, Nodo nodo_menor)
        {
            if (Comparador.iguales(nodo.getUsuario(), nodo_menor.getUsuario()))
            {
                nodo = nodo.getDerecha();
            }
            else if (nodo.getIzquierda() != null && nodo.getDerecha() == null)
            {
                Nodo izquierda;
                izquierda = RemplazoMenor(nodo.getIzquierda(), nodo_menor);
                nodo.setIzquierda(izquierda);
            } else if (nodo.getDerecha() != null && nodo.getIzquierda() == null)
            {
                Nodo derecha;
                derecha = RemplazoMenor(nodo.getDerecha(), nodo_menor);
                nodo.setDerecha(derecha);
            }
            return nodo;
        }
        public Nodo BusquedaMenor(Nodo nodo, Nodo nodo_menor)
        {
            if (nodo != null)
            {
                if (Comparador.mayor_que(nodo.getUsuario(), nodo_menor.getUsuario()))
                {
                    nodo_menor = nodo;
                }
                else
                {
                    nodo_menor = BusquedaMenor(nodo.getIzquierda(), nodo_menor);
                }

            }
            return nodo_menor;
        }
        public void modifica_usuario(Usuario usuario)
        {
            nodo_raiz = this.ModificaUsuario(nodo_raiz, usuario);
        }
        public Nodo ModificaUsuario(Nodo nodo, Usuario usuario)
        {
            if (nodo != null)
            {
                if (nodo.getUsuario().getNickname() == usuario.getNickname())
                {
                    nodo.setUsuario(usuario);
                    return nodo;
                }
                else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
                {
                    nodo.setDerecha(ModificaUsuario(nodo.getDerecha(), usuario));
                }
                else if (Comparador.menor_que(usuario, nodo.getUsuario()))
                {
                    nodo.setIzquierda(ModificaUsuario(nodo.getIzquierda(), usuario));

                }
                return null;
            }
            return null;
        }
        public Usuario obtiene_usuario(String nickname)
        {
            Usuario usuario = new Usuario("", "", nickname, "", new DateTime());
            return ObtieneUsuario(nodo_raiz, usuario);
        }
        public Usuario ObtieneUsuario(Nodo nodo, Usuario usuario)
        {
            if (nodo != null)
            {
                if (nodo.getUsuario().getNickname() == usuario.getNickname())
                {
                    return nodo.getUsuario();
                }
                else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
                {
                    return ObtieneUsuario(nodo.getDerecha(), usuario);
                }
                else if (Comparador.menor_que(usuario, nodo.getUsuario()))
                {
                    return ObtieneUsuario(nodo.getIzquierda(), usuario);

                }
                return null;
            }
            return null;
        }

        public Usuario valida_sesion(String nickname, String clave) {
            Usuario usuario_comodin = new Usuario("", clave, nickname, "", new DateTime());
            return Sesion(nodo_raiz, usuario_comodin);
        }
        public Usuario Sesion(Nodo nodo, Usuario usuario) { 
            if (nodo != null)
            {
                if (nodo.getUsuario().getNickname() == usuario.getNickname())
                {
                    if (nodo.getUsuario().getClave() == Usuario.GenerarSha1(usuario.getClave())){
                        return nodo.getUsuario();
                    }
                    return null;
                } else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
                {
                    return Sesion(nodo.getDerecha(), usuario);
                }
                else if (Comparador.menor_que(usuario, nodo.getUsuario()))
                {
                    return Sesion(nodo.getIzquierda(), usuario);

                }
                return null;
            }
            return null;
        }

    }
}