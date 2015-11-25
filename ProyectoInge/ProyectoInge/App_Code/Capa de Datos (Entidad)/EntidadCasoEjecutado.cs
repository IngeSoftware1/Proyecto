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
        //private byte[] imagen;
        private String estadoEjecucion;

        public EntidadCasoEjecutado(Object[] datos)
        {
            idCaso = Convert.ToInt32(datos[0].ToString());
            idEjecucion = Convert.ToInt32(datos[1].ToString());
            idTipoNC = datos[2].ToString();
            justificacion = datos[3].ToString();
            //imagen = Convert.ToByte(datos[4]);
            //estadoEjecucion = datos[5].ToString();
            estadoEjecucion = datos[4].ToString();

            //imagen = new byte[datos[4].ToString().Length * sizeof(char)];
            //System.Buffer.BlockCopy(datos[4].ToString().ToCharArray(), 0, imagen, 0, imagen.Length);
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

        //////Metodos set y get del atributo imagen
        //public byte[] getImagen
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