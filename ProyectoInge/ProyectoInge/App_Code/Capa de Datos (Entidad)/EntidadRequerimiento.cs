using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadRequerimiento
    {
        String id_req;
        int id_proyecto;
        int id_diseno;
        String nombre_req;

        public EntidadRequerimiento(Object[] datos)
            {
                this.id_req = datos[0].ToString();
                this.id_proyecto = Convert.ToInt32(datos[1].ToString());
                this.id_diseno = Convert.ToInt32(datos[2].ToString());
                this.nombre_req = datos[3].ToString();
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

        //Metodos set y get del atributo id_diseno
        public int getIdDiseno
        {
            get { return id_diseno; }
            set { id_diseno = value; }
        }

        //Metodos set y get del atributo nombre_req
        public String getNombreReq
        {
            get { return nombre_req; }
            set { nombre_req = value; }
        }

    }

}