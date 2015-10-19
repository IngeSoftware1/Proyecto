using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadOficinaUsuaria
    {
        
        private String nombreOficina;
        private String nombreRep;
        private String ape1Rep;
        private String ape2Rep;
        
        /* 
         * Encapsula los atributos de una oficina.
         * @param atributosOficinaUsuaria Vector de tipo Object con los datos de la oficina que se desea encapsular.
        */

        public EntidadOficinaUsuaria(Object[] datos)
        {
            nombreOficina = datos[0].ToString();
            nombreRep = datos[1].ToString();
            ape1Rep = datos[2].ToString();
            ape2Rep = datos[3].ToString();
        }

        //Metodos set y get del atributo nombreOficina
        public String getNombreOficina
        {
            get { return nombreOficina; }
            set { nombreOficina = value; }
        }
        //Metodos set y get del atributo nombreRep
        public String getNombreRep
        {
            get { return nombreRep; }
            set { nombreRep = value; }
        }
        //Metodos set y get del atributo ape1Rep
        public String getApe1Rep
        {
            get { return ape1Rep; }
            set { ape1Rep = value; }
        }
        //Metodos set y get del atributo ape2Rep
        public String getApe2Rep
        {
            get { return ape2Rep; }
            set { ape2Rep = value; }
        }
        
    }
}