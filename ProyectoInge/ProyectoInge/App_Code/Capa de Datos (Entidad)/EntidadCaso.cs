using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadCaso
    {
        int id_caso;
        String identificador_caso;
        String proposito_caso;
        String flujo_central;
        String entrada_datos;
        String resultado_esperado;
        int id_diseno;

        public EntidadCaso(Object[] datos)
        {
            this.id_caso = Convert.ToInt32(datos[1].ToString());
            this.identificador_caso = datos[0].ToString();
            this.proposito_caso = datos[0].ToString();
            this.flujo_central = datos[0].ToString();
            this.entrada_datos = datos[0].ToString();
            this.resultado_esperado = datos[0].ToString();
            this.id_diseno = Convert.ToInt32(datos[1].ToString());
        }

        //Metodos set y get del atributo id_caso
        public int getId_caso
        {
            get { return id_caso; }
            set { id_caso = value; }
        }

        //Metodos set y get del atributo identificador_caso
        public String getIdentificador_caso
        {
            get { return identificador_caso; }
            set { identificador_caso = value; }
        }

        //Metodos set y get del atributo proposito_caso
        public String getproposito_caso
        {
            get { return proposito_caso; }
            set { proposito_caso = value; }
        }

        //Metodos set y get del atributo flujo_central
        public String getFlujo_central
        {
            get { return flujo_central; }
            set { flujo_central = value; }
        }

        //Metodos set y get del atributo entrada_datos
        public String getEntrada_datos
        {
            get { return entrada_datos; }
            set { entrada_datos = value; }
        }

        //Metodos set y get del atributo resultado_esperado
        public String getResultado_esperado
        {
            get { return resultado_esperado; }
            set { resultado_esperado = value; }
        }

        //Metodos set y get del atributo id_diseno
        public int getId_diseno
        {
            get { return id_diseno; }
            set { id_diseno = value; }
        }

    }
}