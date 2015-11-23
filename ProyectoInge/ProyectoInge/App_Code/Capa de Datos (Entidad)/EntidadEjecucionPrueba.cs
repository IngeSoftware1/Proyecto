using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadEjecucionPrueba
    {
        //Atributos de la tabla EjecucionPrueba
        private String descIncidencia;
        private String fecha;
        private String cedulaResponsable;
        private int idDiseño;

        public EntidadEjecucionPrueba(Object[] datos)
            {
                this.descIncidencia = datos[0].ToString();
                this.fecha = datos[1].ToString();
                this.cedulaResponsable = datos[2].ToString();
                this.idDiseño = (Int32.Parse(datos[3].ToString()));
            }

        //Metodos set y get del atributo descripción incidencias
        public String getDescIncidencia
        {
            get { return descIncidencia; }
            set { descIncidencia = value; }
        }

        //Metodos set y get del atributo descripción incidencias
        public String getFecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        //Metodos set y get del atributo fecha
        public String getCedulaResponsable
        {
            get { return cedulaResponsable; }
            set { cedulaResponsable = value; }
        }

        //Metodos set y get del atributo idDiseño
        public int getIdDiseño
        {
            get { return idDiseño; }
            set { idDiseño = value; }
        }
    }
}