using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace Twitter.Models
{
    public class Arbol
    {
        private Nodo nodo_raiz;

        public Arbol() {
            carga_xml_usuario();
            carga_xml_seguidores();
        }

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
                if (nodo.getUsuario().nickname == usuario.nickname)
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
                return nodo;
            }
            return nodo;
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
                if (nodo.getUsuario().nickname == usuario.nickname)
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
                if (nodo.getUsuario().nickname == usuario.nickname)
                {
                    if (nodo.getUsuario().clave == Usuario.GenerarSha1(usuario.clave)){
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
        public void carga_xml_usuario()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(assemblyFile + "/../../Content/XML/Usuarios.xml");
            XmlNodeList usuarios = xDoc.GetElementsByTagName("main");
            XmlNodeList lista = ((XmlElement)usuarios[0]).GetElementsByTagName("DATA_RECORD");
            String nombre = "";
            String nickname = "";
            DateTime fecha = new DateTime();
            String foto = "";
            String password = "";
            Usuario usuario;
            foreach (XmlElement nodo in lista)
            {
                nombre = "";
                nickname = "";
                fecha = new DateTime();
                foto = "";
                password = "";
                XmlNodeList nNombre = nodo.GetElementsByTagName("NOMBRE");
                nombre = nNombre[0].InnerText;
                XmlNodeList nNickName = nodo.GetElementsByTagName("NICK_NAME");
                nickname = nNickName[0].InnerText;
                XmlNodeList nFecha = nodo.GetElementsByTagName("FECHA");
                fecha = Convert.ToDateTime(nFecha[0].InnerText);
                XmlNodeList nFoto = nodo.GetElementsByTagName("FOTO");
                foto = nFoto[0].InnerText;
                XmlNodeList nPassword = nodo.GetElementsByTagName("PASSWORD");
                if(nPassword != null && nPassword[0] != null)
                {
                    password = nPassword[0].InnerText;
                }
                usuario = new Usuario(nombre, password, nickname, foto, fecha);
                this.insertar(usuario);
            }
            
        }
        public void recorre_arbol_in_orden_guardar()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Usuarios.xml");
            writer.WriteStartElement("main");
            this.RecorreArbolInOrdenGuardar(nodo_raiz, writer);
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        public void RecorreArbolInOrdenGuardar(Nodo nodo, XmlWriter writer) {
            if(nodo.getIzquierda() != null)
            {
                RecorreArbolInOrdenGuardar(nodo.getIzquierda(), writer);
            }
            if(nodo.getUsuario() != null)
            {
                writer.WriteStartElement("DATA_RECORD");
                writer.WriteElementString("NOMBRE", nodo.getUsuario().nombreCompleto);
                writer.WriteElementString("NICK_NAME", nodo.getUsuario().nickname);
                writer.WriteElementString("FECHA", String.Format("{0:yyyy/MM/dd HH:mm:ss}", nodo.getUsuario().fechaNacimiento));
                writer.WriteElementString("FOTO", nodo.getUsuario().ubicacionImagen);
                writer.WriteElementString("PASSWORD", nodo.getUsuario().clave);
                writer.WriteEndElement();
            }
            if (nodo.getDerecha() != null)
            {
                RecorreArbolInOrdenGuardar(nodo.getDerecha(), writer);
            }
        }
        public void carga_xml_tuits()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(assemblyFile + "/../../Content/XML/Tuits_usuarios.xml");
            XmlNodeList usuarios = xDoc.GetElementsByTagName("main");
            XmlNodeList lista = ((XmlElement)usuarios[0]).GetElementsByTagName("DATA_RECORD");
            String nickname = "";
            String tuit = "";
            foreach (XmlElement nodo in lista)
            {
                XmlNodeList nNickName = nodo.GetElementsByTagName("NICK_NAME");
                nickname = nNickName[0].InnerText;
                XmlNodeList nTuit = nodo.GetElementsByTagName("TUIT");
                tuit = nTuit[0].InnerText;

            }
        }
        public static void inserta_xml_tuits()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Tuits_usuarios.xml");
            writer.WriteStartElement("main");
            for (int i = 0; i < 3; i++)
            {
                writer.WriteStartElement("DATA_RECORD");
                writer.WriteElementString("NICK_NAME", "2");
                writer.WriteElementString("TUIT", "2");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
        }
        public void carga_xml_seguidores()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(assemblyFile + "/../../Content/XML/Usuarios_seguidos.xml");
            XmlNodeList usuarios = xDoc.GetElementsByTagName("main");
            XmlNodeList lista = ((XmlElement)usuarios[0]).GetElementsByTagName("DATA_RECORD");
            String nickname = "";
            String nicknameSeguido = "";
            Usuario usuario;
            Usuario usuarioSeguido;
            foreach (XmlElement nodo in lista)
            {
                XmlNodeList nNickName = nodo.GetElementsByTagName("USUARIO");
                nickname = nNickName[0].InnerText;
                XmlNodeList nNickNameSeguido = nodo.GetElementsByTagName("USUARIO_SEGUIDO");
                nicknameSeguido = nNickNameSeguido[0].InnerText;
                usuario = this.obtiene_usuario(nickname);
                usuarioSeguido = this.obtiene_usuario(nicknameSeguido);
                if(usuario!= null && usuarioSeguido != null)
                {
                    usuario.seguidos.Insertar(usuarioSeguido, usuarioSeguido.nickname.GetHashCode());
                    usuarioSeguido.seguidores.Insertar(usuario, usuario.nickname.GetHashCode());
                }
            }
        }
        public void recorre_arbol_in_orden_seguidores()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Usuarios_seguidos.xml");
            writer.WriteStartElement("main");
            this.RecorreArbolInOrdenSeguidores(nodo_raiz, writer);
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        public void seguir(String usuario, String usuarioSeguir)
        {
            Usuario objetoUsuario = this.obtiene_usuario(usuario);
            Usuario objetoUsuarioSeguir = this.obtiene_usuario(usuarioSeguir);
            if(objetoUsuarioSeguir != null && objetoUsuario != null)
            {
                objetoUsuario.seguidos.Insertar(objetoUsuarioSeguir, objetoUsuarioSeguir.nickname.GetHashCode());
                this.modifica_usuario(objetoUsuario);
                objetoUsuarioSeguir.seguidores.Insertar(objetoUsuario, objetoUsuario.nickname.GetHashCode());
                this.modifica_usuario(objetoUsuarioSeguir);
                this.recorre_arbol_in_orden_seguidores();
            }
        }
        public void agregar_usuario(Usuario usuario)
        {
            this.insertar(usuario);
            this.recorre_arbol_in_orden_guardar();
        }

        public void RecorreArbolInOrdenSeguidores(Nodo nodo, XmlWriter writer)
        {
            if(nodo != null)
            {
                if (nodo.getIzquierda() != null)
                {
                    RecorreArbolInOrdenSeguidores(nodo.getIzquierda(), writer);
                }
                if (nodo.getUsuario() != null)
                {
                    for (int i = 0; i <= 1026; i++)
                    {
                        if (nodo.getUsuario().seguidores.Buscar(i) != null)
                        {
                            writer.WriteStartElement("DATA_RECORD");
                            writer.WriteElementString("USUARIO", nodo.getUsuario().seguidores.Buscar(i).nickname);
                            writer.WriteElementString("USUARIO_SEGUIDO", nodo.getUsuario().nickname);
                            writer.WriteEndElement();
                        }
                        if (nodo.getUsuario().seguidos.Buscar(i) != null)
                        {
                            writer.WriteStartElement("DATA_RECORD");
                            writer.WriteElementString("USUARIO", nodo.getUsuario().nickname);
                            writer.WriteElementString("USUARIO_SEGUIDO", nodo.getUsuario().seguidos.Buscar(i).nickname);
                            writer.WriteEndElement();
                        }
                    }
                }
                if (nodo.getDerecha() != null)
                {
                    RecorreArbolInOrdenSeguidores(nodo.getDerecha(), writer);
                }
            }
            
        }
    }
}