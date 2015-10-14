using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadOficinaUsuaria
    {
        
        public String nombre_oficina;
        public String nombre_rep;
        public String ape1_rep;
        public String ape2_rep;
        
        /* 
         * Encapsula los atributos de una oficina.
         * @param atributosOficinaUsuaria Vector de tipo Object con los datos de la oficina que se desea encapsular.
         */
        public EntidadOficinaUsuaria(Object[] datos)
        {
            

            
            nombre_oficina = datos[0].ToString();
            nombre_rep = datos[1].ToString();
            ape1_rep = datos[2].ToString();
            ape2_rep = datos[3].ToString();
            

        }
        
        //Metodos set y get del atributo nombre_oficina
        public String get_nombre_oficina
        {
            get { return nombre_oficina; }
            set { nombre_oficina = value; }
        }
        //Metodos set y get del atributo nombre_rep
        public String get_nombre_rep
        {
            get { return nombre_rep; }
            set { nombre_rep = value; }
        }
        //Metodos set y get del atributo ape1_rep
        public String get_ape1_rep
        {
            get { return ape1_rep; }
            set { ape1_rep = value; }
        }
        //Metodos set y get del atributo ape2_rep
        public String get_ape2_rep
        {
            get { return ape2_rep; }
            set { ape2_rep = value; }
        }
        
    }
}