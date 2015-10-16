using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadTelOficina
    {
        private int idOficina;
        private string numTelefono;

        public EntidadTelOficina(object[] datos)
        {
            this.idOficina = Convert.ToInt32(datos[0].ToString());
            this.numTelefono = datos[1].ToString();
        }

        //Metodos set y get del atributo idOficina
        public int getIdOficina
        {
            get { return idOficina; }
            set { idOficina = value; }
        }
        //Metodos set y get del atributo numTelefono
        public String getNumTelefono
        {
            get { return numTelefono; }
            set { numTelefono = value; }
        }
    }
}