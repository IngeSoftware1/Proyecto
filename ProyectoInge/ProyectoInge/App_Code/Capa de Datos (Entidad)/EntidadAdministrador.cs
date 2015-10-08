using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadAdministrador
    {
        //Atributos de la tabla Administrador
        private String cedula_admin;

        public EntidadAdministrador(Object[] datos)
            {
                this.cedula_admin = datos[0].ToString();
            }

        //Metodos set y get del atributo cedula
        public String getCedula_Admin
        {
            get { return cedula_admin; }
            set { cedula_admin = value; }
        }
    }
}