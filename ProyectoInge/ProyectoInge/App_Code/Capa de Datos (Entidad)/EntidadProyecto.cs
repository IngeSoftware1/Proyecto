using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadProyecto
    {
        public String id_proyecto;
        public String nombre_proyecto;
        public String obj_general;
        public String fecha_asignacion;
        public String tipo_estado;
        public String cedula_lider;
        public String id_oficina;
        public String creador;

        /* 
         * Encapsula los atributos de un proyecto.
         * @param atributosUsuario Vector de tipo Object con los datos del usuario que se desea encapsular.
         */
        public EntidadProyecto(Object[] datos)
        {
            //el atributo creador no lo pongo porque no tengo de donde sacarlo?, pensando desde el modificar

            id_proyecto = datos[0].ToString();
            nombre_proyecto = datos[1].ToString();
            obj_general = datos[2].ToString();
            fecha_asignacion = datos[3].ToString();
            tipo_estado = datos[4].ToString();
            cedula_lider = datos[5].ToString();
            id_oficina = datos[6].ToString();
            //creador = datos[7].ToString();

        }
        //Metodos set y get del atributo id_proyecto
        public String get_id_proyecto
        {
            get { return id_proyecto; }
            set { id_proyecto = value; }
        }
        //Metodos set y get del atributo nombre_proyecto
        public String get_nombre_proyecto
        {
            get { return nombre_proyecto; }
            set { nombre_proyecto = value; }
        }
        //Metodos set y get del atributo obj_general
        public String get_obj_general
        {
            get { return obj_general; }
            set { obj_general = value; }
        }
        //Metodos set y get del atributo fecha_asignacion
        public String get_fecha_asignacion
        {
            get { return fecha_asignacion; }
            set { fecha_asignacion = value; }
        }
        //Metodos set y get del atributo tipo_estado
        public String get_tipo_estado
        {
            get { return tipo_estado; }
            set { tipo_estado = value; }
        }
        //Metodos set y get del atributo cedula_lider
        public String get_cedula_lider
        {
            get { return cedula_lider; }
            set { cedula_lider = value; }
        }
        //Metodos set y get del atributo id_oficina
        public String get_id_oficina
        {
            get { return id_oficina; }
            set { id_oficina = value; }
        }
    }
}