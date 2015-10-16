using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadTrabajaEn
    {
        private string cedulaMiembro;
        private int idProyecto;

        public EntidadTrabajaEn(object[] datos)
        {
            this.cedulaMiembro = datos[0].ToString();
            this.idProyecto = Convert.ToInt32(datos[1].ToString());
        }

        //Metodos set y get del atributo cedulaMiembro
        public String getCedulaMiembro
        {
            get { return cedulaMiembro; }
            set { cedulaMiembro = value; }
        }
        //Metodos set y get del atributo idProyecto
        public int geIdProyecto
        {
            get { return idProyecto; }
            set { idProyecto = value; }
        }
    }
}