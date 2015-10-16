using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadProyecto
    {
        public String nombreProyecto;
        public String objGeneral;
        public String fechaAsignacion;
        public String tipoEstado;
        public String cedulaCreador;
        public String cedulaLider;
        public int idOficina;
  

        /* 
         * Encapsula los atributos de un proyecto.
         * @param atributosUsuario Vector de tipo Object con los datos del usuario que se desea encapsular.
         */
        public EntidadProyecto(Object[] datos)
        {
            nombreProyecto = datos[0].ToString();
            objGeneral = datos[1].ToString();
            fechaAsignacion = datos[2].ToString();
            tipoEstado = datos[3].ToString();
            cedulaCreador = datos[4].ToString();
            cedulaLider = datos[5].ToString();
            idOficina = Convert.ToInt32(datos[6].ToString());
        }

        //Metodos set y get del atributo nombreProyecto
        public String getNombreProyecto
        {
            get { return nombreProyecto; }
            set { nombreProyecto = value; }
        }
        //Metodos set y get del atributo objGeneral
        public String getObjGeneral
        {
            get { return objGeneral; }
            set { objGeneral = value; }
        }
        //Metodos set y get del atributo fechaAsignacion
        public String getFechaAsignacion
        {
            get { return fechaAsignacion; }
            set { fechaAsignacion = value; }
        }
        //Metodos set y get del atributo tipoEstado
        public String getTipoEstado
        {
            get { return tipoEstado; }
            set { tipoEstado = value; }
        }
        //Metodos set y get del atributo cedulaCreador
        public String getCedulaCreador
        {
            get { return cedulaCreador; }
            set { cedulaCreador = value; }
        }

        //Metodos set y get del atributo cedulaLider
        public String getCedulaLider
        {
            get { return cedulaLider; }
            set { cedulaLider = value; }
        }
        //Metodos set y get del atributo idOficina
        public int getIdOficina
        {
            get { return idOficina; }
            set { idOficina = value; }
        }
    }
}