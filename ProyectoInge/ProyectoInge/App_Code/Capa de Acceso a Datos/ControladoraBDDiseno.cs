using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDDiseno
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();

        /*Método para buscar si en la base de datos existe un usuario con una cedula particular, que sea líder de diseño
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera la consulta para mandarla a la base de datos para saber si hay un usuario líder de diseño.
        * Retorna: true si se encontro que el funcionario tenía era líder y false si no.
        */
        public bool buscarAsignacionMiembrosDiseno(string cedulaDeFuncionario)
        {

        bool resultado = false;

            try
            {
                string consultaPruebas = "SELECT * FROM Diseno_Pruebas WHERE cedula_responsable ='" + cedulaDeFuncionario + "'";
                DataTable dataDiseno = acceso.ejecutarConsultaTabla(consultaPruebas);
                if (dataDiseno.Rows.Count >= 1)
                {
                    resultado = true;
                }
                else
                {
                        resultado = false;
                }
            }
            catch (SqlException e)
            {
                resultado = false;
            }

        return resultado;
    }

        public int obtenerIdDisenoPorProposito(string proposito)
        {
            int resultado = -1;

            try
            {
                string consultaDiseno = "SELECT id_diseno FROM Diseno_Pruebas WHERE proposito_diseno ='" + proposito + "'";
                DataTable dataDiseno = acceso.ejecutarConsultaTabla(consultaDiseno);
                if (dataDiseno.Rows.Count >= 1)
                {
                    resultado = Convert.ToInt32(dataDiseno.Rows[0][0].ToString());
                }
            }
            catch (SqlException e)
            {
            }

            return resultado;
        }

        /* Método para obtener los niveles que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los niveles
        */
        public DataTable consultarNiveles()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT nivel_Prueba FROM Nivel_Prueba ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        /* Método para obtener los tipos que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los tipos
        */
        public DataTable consultarTipos()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT tipo_Prueba FROM Tipo_Prueba ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }


        /* Método para obtener las tecnicas que puede tener un diseño
        * Requiere: no requiere informacion
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene las tecnicas
        */
        public DataTable consultarTecnicas()
        {
            DataTable dt = new DataTable();
            string consulta;

            try
            {

                consulta = "SELECT tipo_Tecnica FROM Tecnica ";
                dt = acceso.ejecutarConsultaTabla(consulta);

            }
            catch
            {
                dt = null;
            }

            return dt;
        }




  }
}