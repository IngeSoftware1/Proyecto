using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadCasoEjecutado
    {
        private int idCaso;
        private int idEjecucion;
        private String idTipoNC;
        private String justificacion;
        //private vara para la imagen
        private String estadoEjecucion;

        public EntidadCasoEjecutado(Object[] datos)
        {
            idCaso = Convert.ToInt32(datos[0].ToString());
            idEjecucion = Convert.ToInt32(datos[1].ToString());
            idTipoNC = datos[2].ToString();
            justificacion = datos[3].ToString();
            //imagen
            estadoEjecucion = datos[4].ToString();
        }

        //Metodos set y get del atributo idCaso
        public int getIdCaso
        {
            get { return idCaso; }
            set { idCaso = value; }
        }

        //Metodos set y get del atributo idDiseno
        public int getIdEjecucion
        {
            get { return idEjecucion; }
            set { idEjecucion = value; }
        }

        //Metodos set y get del atributo idTipoNC
        public String getIdTipoNC
        {
            get { return idTipoNC; }
            set { idTipoNC = value; }
        }

        //Metodos set y get del atributo justificacion
        public String getJustificacion
        {
            get { return justificacion; }
            set { justificacion = value; }
        }

        ////Metodos set y get del atributo imagen
        //public String getImagen
        //{
        //    get { return imagen; }
        //    set { imagen = value; }
        //}

        //Metodos set y get del atributo estadoEjecucion
        public String getEstadoEjecucion
        {
            get { return estadoEjecucion; }
            set { estadoEjecucion = value; }
        }

    }
}