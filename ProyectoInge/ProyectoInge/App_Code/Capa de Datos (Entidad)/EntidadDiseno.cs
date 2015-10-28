using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadDiseno
    {
         //Atributos de la tabla Diseño de pruebas
            private String proposito;
            private String fecha;
            private String procedimiento;
            private String ambiente;
            private String criterioDeAceptacion;
            private String tecnica;
            private String nivel;
            private String tipo;
            private int idProyecto;
            private String cedulaResponsable;



            /* 
             * Encapsula los atributos de un usuario.
             * @param atributosUsuario Vector de tipo Object con los datos del usuario que se desea encapsular.
             */
            public EntidadDiseno(Object[] datos)
            {
                this.proposito = datos[0].ToString();
                this.fecha = datos[1].ToString();
                this.procedimiento = datos[2].ToString();
                this.ambiente = datos[3].ToString();
                this.criterioDeAceptacion = datos[4].ToString();
                this.tecnica = datos[5].ToString();
                this.nivel = datos[6].ToString();
                this.tipo = datos[7].ToString();       
                this.idProyecto = Convert.ToInt32(datos[8].ToString());
                this.cedulaResponsable = datos[9].ToString();
            }

            //Metodos set y get del atributo proposito
            public String getProposito
            {
                get { return proposito; }
                set { proposito = value; }
            }

            //Metodos set y get del atributo fecha
            public String getFecha
            {
                get { return fecha; }
                set { fecha = value; }
            }

            //Metodos set y get del atributo procedimiento
            public String getProcedimiento
            {
                get { return procedimiento; }
                set { procedimiento = value; }
            }

            //Metodos set y get del atributo ambiente
            public String getAmbiente
            {
                get { return ambiente; }
                set { ambiente = value; }
            }

            //Metodos set y get del atributo criterio de aceptacion
            public String getCriterio
            {
                get { return criterioDeAceptacion; }
                set { criterioDeAceptacion = value; }
            }

            //Metodos set y get del atributo tecnica
            public String getTecnica
            {
                get { return tecnica; }
                set { tecnica = value; }
            }

            //Metodos set y get del atributo nivel
            public String getNivel
            {
                get { return nivel; }
                set { nivel = value; }
            }

            //Metodos set y get del atributo tipo
            public String getTipo
            {
                get { return tipo; }
                set { tipo = value; }
            }

          //Metodos set y get del atributo idProyecto
            public int getIdProyecto
            {
                get { return idProyecto; }
                set { idProyecto = value; }
            }

            //Metodos set y get del atributo cedulaResponsable
            public String getCedulaResponsable
            {
                get { return cedulaResponsable; }
                set { cedulaResponsable = value; }
            }

        }
}