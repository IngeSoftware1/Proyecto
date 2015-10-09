using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    public class EntidadTelFuncionario
    {
        private string cedula_funcionario;
        private string num_telefono;


         /*Encapsula los atributos de un un telefono
              Requiere: un objeto con los datos a encapsular
          * Modifica: Llena con lso valores del objeto los atributos de la clase
          * Retorna: no retorna ningùn valor
             */
        public EntidadTelFuncionario(Object[] datos)
            {
                this.cedula_funcionario = datos[0].ToString();
                this.num_telefono = datos[1].ToString();
            }

            //Metodos set y get del atributo cedula del funcioario
            public String getCedulaFuncionario
            {
                get { return cedula_funcionario; }
                set { cedula_funcionario = value; }
            }

            //Metodos set y get del atributo telefono
            public String getNumTelefono
            {
                get { return num_telefono; }
                set { num_telefono = value; }
            }
    }
}