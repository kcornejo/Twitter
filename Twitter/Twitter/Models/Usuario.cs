using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto_Final_Web
{
    public class Usuario
    {
        private String nombreCompleto;
        private String nickname;
        private String correo;
        private DateTime fechaNacimiento;
        private String ubicacionImagen;
        public String getNombreCompleto() {
            return nombreCompleto;
        }
        public void setNombreCompleto(String nombreCompleto)
        {
            this.nombreCompleto = nombreCompleto;
        }
        public String getNickname() {
            return nickname;
        }
        public void setNickname(String nickname)
        {
            this.nickname = nickname;
        }
        public String getCorreo()
        {
            return correo;
        }
        public void setCorreo(String correo)
        {
            this.correo = correo;
        }
        public DateTime getFechaNacimiento()
        {
            return fechaNacimiento;
        }
        public void setFechaNacimiento(DateTime fechaNacimiento)
        {
            this.fechaNacimiento = fechaNacimiento;
        }
        public String getUbicacionImagen()
        {
            return ubicacionImagen;
        }
        public void setUbicacionImagen(String ubicacionImagen)
        {
            this.ubicacionImagen = ubicacionImagen;
        }
        public Usuario() {
            nombreCompleto = "";
            nickname = "";
            correo = "";
            ubicacionImagen = "";
            fechaNacimiento = new DateTime(2018, 1, 1);
        }
        public Usuario(String nombreCompleto, String nickname, String correo, String ubicacionImagen, DateTime fechaNacimiento)
        {
            this.nombreCompleto = nombreCompleto;
            this.nickname = nickname;
            this.correo = correo;
            this.ubicacionImagen = ubicacionImagen;
            this.fechaNacimiento = fechaNacimiento; 
        }
    }
}
