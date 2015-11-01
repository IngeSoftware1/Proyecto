using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadRequerimientoDiseño
    {
        int id_diseno;
        String id_req;
        int id_proyecto;


        public EntidadRequerimientoDiseño(Object[] datos)
        {

            this.id_diseno = Convert.ToInt32(datos[0].ToString());
            this.id_req = datos[1].ToString();
            this.id_proyecto = Convert.ToInt32(datos[2].ToString());
        }

        //Metodos set y get del atributo id_diseno
        public int getIdDiseno
        {
            get { return id_diseno; }
            set { id_diseno = value; }
        }

        //Metodos set y get del atributo id_req
        public String getIdReq
        {
            get { return id_req; }
            set { id_req = value; }
        }

        //Metodos set y get del atributo id_proyecto
        public int getIdProyecto
        {
            get { return id_proyecto; }
            set { id_proyecto = value; }
        }
    }
}