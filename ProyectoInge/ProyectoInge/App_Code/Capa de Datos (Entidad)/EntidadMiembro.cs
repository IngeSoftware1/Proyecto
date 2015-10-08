using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadMiembro
    {
        private String cedula_miembro;
        private String tipo_rol;

        public EntidadMiembro(Object[] datos)
            {
                this.cedula_miembro = datos[0].ToString();
                this.tipo_rol = datos[1].ToString();
            }

        //Metodos set y get del atributo cedula_miembro
        public String getCedulaMiembro
        {
            get { return cedula_miembro; }
            set { cedula_miembro = value; }
        }

        //Metodos set y get del atributo rol
        public String getTipoRol
        {
            get { return tipo_rol; }
            set { tipo_rol = value; }
        }
    }
}