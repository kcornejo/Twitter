using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Twitter.Models
{
    public class Usuario
    {
        
        public String nombreCompleto { get; set; }
        public String nickname { get; set; }
        public String clave { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public String ubicacionImagen { get; set; }
        public ListaDoblementeEnlazada tweets_muro { get; set; }
        public TablaHash seguidos { get; set; }
        public TablaHash seguidores { get; set; }
        public Usuario() {
            nombreCompleto = "";
            nickname = "";
            ubicacionImagen = "";
            clave = "";
            fechaNacimiento = new DateTime(2018, 1, 1);
            seguidos = new TablaHash();
            seguidores = new TablaHash();
        }
        public Usuario(String nombreCompleto, String clave, String nickname, String ubicacionImagen, DateTime fechaNacimiento)
        {
            this.nombreCompleto = nombreCompleto;
            this.nickname = nickname;
            this.ubicacionImagen = ubicacionImagen;
            this.fechaNacimiento = fechaNacimiento;
            this.clave = Usuario.GenerarSha1(clave);
            this.seguidos = new TablaHash();
            this.seguidores = new TablaHash();
        }
        public Usuario(String nombreCompleto, String clave , String nickname,  String ubicacionImagen, DateTime fechaNacimiento, TablaHash seguidores, TablaHash seguidos)
        {
            this.nombreCompleto = nombreCompleto;
            this.nickname = nickname;
            this.ubicacionImagen = ubicacionImagen;
            this.fechaNacimiento = fechaNacimiento;
            this.clave = Usuario.GenerarSha1(clave);
            this.seguidores = seguidores;
            this.seguidos = seguidos;
        }
        public static string GenerarSha1(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        public static void carga_xml_usuario() {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(assemblyFile+"/../../Content/XML/Usuarios.xml");
            XmlNodeList usuarios = xDoc.GetElementsByTagName("main");
            XmlNodeList lista = ((XmlElement)usuarios[0]).GetElementsByTagName("DATA_RECORD");
            String nombre = "";
            String nickname = "";
            DateTime fecha = new DateTime();
            String foto = "";
            foreach (XmlElement nodo in lista)
            {
                XmlNodeList nNombre = nodo.GetElementsByTagName("NOMBRE");
                nombre = nNombre[0].InnerText;
                XmlNodeList nNickName = nodo.GetElementsByTagName("NICK_NAME");
                nickname = nNickName[0].InnerText;
                XmlNodeList nFecha = nodo.GetElementsByTagName("FECHA");
                fecha = Convert.ToDateTime(nFecha[0].InnerText);
                XmlNodeList nFoto = nodo.GetElementsByTagName("FOTO");
                foto = nFoto[0].InnerText;
            }
        }
        public static void inserta_xml_usuario()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Usuarios.xml");
            writer.WriteStartElement("main");
            for(int i = 0; i< 3; i++)
            {
                writer.WriteStartElement("DATA_RECORD");
                writer.WriteElementString("NOMBRE", "2");
                writer.WriteElementString("NICK_NAME", "2");
                writer.WriteElementString("FECHA", "2");
                writer.WriteElementString("FOTO", "2");
                writer.WriteEndElement();
            }
            

            writer.WriteEndElement();
            writer.Flush();
        }
        public static void carga_xml_tuits()
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
        public static void carga_xml_seguidores() {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(assemblyFile + "/../../Content/XML/Usuarios_seguidos.xml");
            XmlNodeList usuarios = xDoc.GetElementsByTagName("main");
            XmlNodeList lista = ((XmlElement)usuarios[0]).GetElementsByTagName("DATA_RECORD");
            String nickname = "";
            String nicknameSeguido = "";
            foreach (XmlElement nodo in lista)
            {
                XmlNodeList nNickName = nodo.GetElementsByTagName("USUARIO");
                nickname = nNickName[0].InnerText;
                XmlNodeList nNickNameSeguido = nodo.GetElementsByTagName("USUARIO_SEGUIDO");
                nicknameSeguido = nNickNameSeguido[0].InnerText;
            }
        }
        public static void inserta_xml_seguidores()
        {
            string assemblyFile = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            XmlWriter writer = XmlWriter.Create(assemblyFile + "/../../Content/XML/Usuarios_seguidos.xml");
            writer.WriteStartElement("main");
            for (int i = 0; i < 3; i++)
            {
                writer.WriteStartElement("DATA_RECORD");
                writer.WriteElementString("USUARIO", "2");
                writer.WriteElementString("USUARIO_SEGUIDO", "2");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
        }
    }
}
