using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Twitter.Models
{
    public class Arbol
    {
        private Nodo nodo_raiz;

        public Arbol()
        {
            carga_xml_usuario();
            carga_xml_seguidores();
            carga_xml_tuits();
        }
        public void inserta_texto(String texto)
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            String path = assemblyFile + "/../../Content/Bitacora/Bitacora.txt";
            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
            try
            {
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine(texto);
                tw.Close();
#pragma warning disable CS0168 // The variable 'e' is declared but never used
            }
            catch (Exception e)
#pragma warning restore CS0168 // The variable 'e' is declared but never used
            {

            }

        }
        private Nodo rotacionII(Nodo n, Nodo n1)
        {
            n.setIzquierda(n1.getDerecha());
            n1.setDerecha(n);
            // actualización de los factores de equilibrio
            if (n1.fe == -1) // se cumple en la inserción
            {
                n.fe = 0;
                n1.fe = 0;
            }
            else
            {
                n.fe = -1;
                n1.fe = 1;
            }
            return n1;
        }


        private Nodo rotacionDD(Nodo n, Nodo n1)
        {
            n.setDerecha(n1.getIzquierda());
            n1.setIzquierda(n);
            // actualización de los factores de equilibrio
            if (n1.fe == +1) // se cumple en la inserción
            {
                n.fe = 0;
                n1.fe = 0;
            }
            else
            {
                n.fe = +1;
                n1.fe = -1;
            }
            return n1;
        }


        private Nodo rotacionID(Nodo n, Nodo n1)
        {
            Nodo n2;
            n2 = (Nodo)n1.getDerecha();
            n.setIzquierda(n2.getDerecha());
            n2.setDerecha(n);
            n1.setDerecha(n2.getIzquierda());
            n2.setIzquierda(n1);
            // actualización de los factores de equilibrio
            if (n2.fe == +1)
                n1.fe = -1;
            else
                n1.fe = 0;
            if (n2.fe == -1)
                n.fe = 1;
            else
                n.fe = 0;
            n2.fe = 0;
            return n2;
        }


        private Nodo rotacionDI(Nodo n, Nodo n1)
        {
            Nodo n2;
            n2 = (Nodo)n1.getIzquierda();
            n.setDerecha(n2.getIzquierda());
            n2.setIzquierda(n);
            n1.setIzquierda(n2.getDerecha());
            n2.setDerecha(n1);
            // actualización de los factores de equilibrio
            if (n2.fe == +1)
                n.fe = -1;
            else
                n.fe = 0;
            if (n2.fe == -1)
                n1.fe = 1;
            else
                n1.fe = 0;
            n2.fe = 0;
            return n2;
        }
        public List<dynamic> listar()
        {
            var lista = new List<dynamic>();
            lista = InOrden(nodo_raiz, lista);
            return lista;
        }
        public List<dynamic> InOrden(Nodo nodo, List<dynamic> listado_usuario)
        {
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

        public void insertar(Usuario usuario)
        {
            Logical h = new Logical(false);
            nodo_raiz = InsertarValor(nodo_raiz, usuario, h);
        }
        public Nodo InsertarValor(Nodo nodo, Usuario usuario, Logical h)
        {
            Nodo n1;
            if (nodo == null || nodo.getUsuario() == null)
            {
                nodo = new Nodo();
                nodo.setUsuario(usuario);
                h.setLogical(true);

            }
            else if (Comparador.menor_que(usuario, nodo.getUsuario()))
            {
                Nodo izquierda;
                izquierda = InsertarValor(nodo.getIzquierda(), usuario, h);
                nodo.setIzquierda(izquierda);
                if (h.booleanValue())
                {
                    // decrementa el fe por aumentar la altura de rama izquierda
                    switch (nodo.fe)
                    {
                        case 1:
                            nodo.fe = 0;
                            h.setLogical(false);
                            break;
                        case 0:
                            nodo.fe = -1;
                            break;
                        case -1: // aplicar rotación a la izquierda
                            n1 = (Nodo)nodo.getIzquierda();
                            if (n1.fe == -1)
                            {
                                nodo = rotacionII(nodo, n1);
                                this.inserta_texto("Factor de equilibrio = -2; Nodos en Rotacion =" + nodo.getUsuario().nickname + "," + n1.getUsuario().nickname + " Rotacion II");
                            }
                            else
                            {
                                nodo = rotacionID(nodo, n1);
                                this.inserta_texto("Factor de equilibrio = -2; Nodos en Rotacion =" + nodo.getUsuario().nickname + "," + n1.getUsuario().nickname + " Rotacion ID");
                            }

                            h.setLogical(false);
                            break;
                    }
                }
            }
            else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
            {
                Nodo derecha;
                derecha = InsertarValor(nodo.getDerecha(), usuario, h);
                nodo.setDerecha(derecha);
                if (h.booleanValue())
                {
                    // incrementa el fe por aumentar la altura de rama izquierda
                    switch (nodo.fe)
                    {
                        case 1: // aplicar rotación a la derecha
                            n1 = (Nodo)nodo.getDerecha();
                            if (n1.fe == +1)
                            {
                                nodo = rotacionDD(nodo, n1);
                                this.inserta_texto("Factor de equilibrio = 2; Nodos en Rotacion =" + nodo.getUsuario().nickname + "," + n1.getUsuario().nickname + " Rotacion DD");
                            }
                            else
                            {
                                nodo = rotacionDI(nodo, n1);
                                this.inserta_texto("Factor de equilibrio = 2; Nodos en Rotacion =" + nodo.getUsuario().nickname + "," + n1.getUsuario().nickname + " Rotacion DI");
                            }
                            h.setLogical(false);
                            break;
                        case 0:
                            nodo.fe = +1;
                            break;
                        case -1:
                            nodo.fe = 0;
                            h.setLogical(false);
                            break;
                    }
                }
            }
            else
            {
                //IGUALES
            }
            return nodo;
        }
        public void eliminar_nodo(Usuario usuario)
        {
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
            }
            else if (nodo.getDerecha() == null && nodo.getIzquierda() != null)
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
            }
            else if (nodo.getDerecha() != null && nodo.getIzquierda() == null)
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

        public Usuario valida_sesion(String nickname, String clave)
        {
            Usuario usuario_comodin = new Usuario("", clave, nickname, "", new DateTime());
            return Sesion(nodo_raiz, usuario_comodin);
        }
        public Usuario Sesion(Nodo nodo, Usuario usuario)
        {
            if (nodo != null)
            {
                if (nodo.getUsuario().nickname == usuario.nickname)
                {
                    if (nodo.getUsuario().clave == Usuario.GenerarSha1(usuario.clave))
                    {
                        return nodo.getUsuario();
                    }
                    return null;
                }
                else if (Comparador.mayor_que(usuario, nodo.getUsuario()))
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
                if (nPassword != null && nPassword[0] != null)
                {
                    password = nPassword[0].InnerText;
                }
                usuario = new Usuario(nombre, password, nickname, foto, fecha);
                this.insertar(usuario);
            }

        }
        public void recorre_arbol_in_orden_guardar()
        {
            string test = System.AppContext.BaseDirectory;
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Usuarios.xml");
            writer.WriteStartElement("main");
            this.RecorreArbolInOrdenGuardar(nodo_raiz, writer);
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        public void RecorreArbolInOrdenGuardar(Nodo nodo, XmlWriter writer)
        {
            if (nodo.getIzquierda() != null)
            {
                RecorreArbolInOrdenGuardar(nodo.getIzquierda(), writer);
            }
            if (nodo.getUsuario() != null)
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
                XmlNodeList nFechaHora = nodo.GetElementsByTagName("FECHA_HORA");
                DateTime fechaHora = DateTime.Now;
                if (nFechaHora != null && nFechaHora[0] != null)
                {
                    fechaHora = DateTime.ParseExact(nFechaHora[0].InnerText, "yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                Usuario usuario = this.obtiene_usuario(nickname);
                if (usuario != null)
                {
                    Tweet tweet = new Tweet(usuario, tuit);
                    tweet.fechaHora = fechaHora;
                    usuario.tweets_muro.insertarTweet(tweet);
                }

            }
        }
        public void inserta_xml_tuits()
        {
            string test = System.AppContext.BaseDirectory;
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Tuits_usuarios.xml");
            writer.WriteStartElement("main");
            this.RecorreArbolInOrdenTuits(nodo_raiz, writer);
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
        }
        public void RecorreArbolInOrdenTuits(Nodo nodo, XmlWriter writer)
        {
            if (nodo != null)
            {
                if (nodo.getIzquierda() != null)
                {
                    RecorreArbolInOrdenTuits(nodo.getIzquierda(), writer);
                }
                if (nodo.getUsuario() != null)
                {
                    NodoDoblementeEnlazado tuitNodo = nodo.getUsuario().tweets_muro.primero;
                    while (tuitNodo != null)
                    {
                        if (tuitNodo != null && tuitNodo.tweet != null && tuitNodo.tweet.usuario.nickname == nodo.getUsuario().nickname)
                        {
                            writer.WriteStartElement("DATA_RECORD");
                            writer.WriteElementString("NICK_NAME", tuitNodo.tweet.usuario.nickname);
                            writer.WriteElementString("TUIT", tuitNodo.tweet.contenido);
                            writer.WriteElementString("FECHA_HORA", String.Format("{0:yyyy/MM/dd HH:mm:ss}", tuitNodo.tweet.fechaHora));
                            writer.WriteEndElement();
                        }
                        tuitNodo = tuitNodo.siguiente;

                    }

                }
                if (nodo.getDerecha() != null)
                {
                    RecorreArbolInOrdenTuits(nodo.getDerecha(), writer);
                }
            }

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
                if (usuario != null && usuarioSeguido != null)
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
            if (objetoUsuarioSeguir != null && objetoUsuario != null)
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
            usuario.clave = Usuario.GenerarSha1(usuario.clave);
            this.insertar(usuario);
            this.recorre_arbol_in_orden_guardar();
        }

        public void RecorreArbolInOrdenSeguidores(Nodo nodo, XmlWriter writer)
        {
            if (nodo != null)
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