using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{
    public class ControladoraBDCasosPrueba
    {
        AccesoBaseDatos acceso = new AccesoBaseDatos();
        /*Método para eliminar de la base de datos un caso de prueba asociado a un proyecto
        * Requiere: con el id del proyecto que se va a eliminar
        * Modifica: eliminar el caso de prueba asociado al proyecto
        * Retorna: true si se llevó a cabo correctamente la eliminación y false si no fue existosa.
        */
        public bool eliminarProyectoCasoPueba(int idProyecto)
        {
            try
            {
                string borradoProyecto = "Delete from Caso_Prueba where id_proyecto ='" + idProyecto + "';";
                acceso.eliminarDatos(borradoProyecto);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }

        }

        public bool insertarCasoPrueba(EntidadCaso nuevo)
        {
            bool resultado = false;
            try
            {
                //id_caso no por que es un autonumérico ??
                string insercion = "INSERT INTO Caso_Prueba ( identificador_caso, proposito_caso, flujo_central, entrada_datos, resultado_esperado, id_diseno) VALUES ('" + nuevo.getIdentificador_caso + "', '" + nuevo.getproposito_caso + "', '" + nuevo.getFlujo_central + "', '" + nuevo.getEntrada_datos + "', '" + nuevo.getResultado_esperado + "', '" + nuevo.getId_diseno + "')";

                resultado = acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                resultado = false;
            }

            return resultado;
        }

        /*Método para modificar un caso de prueba
         * Recibe: Una entidad caso de prueba y el id del caso que se va a modificar
         * Modifica: Realiza la modificación en la base de datos del caso respectivo
         * Retorna: true si pudo modificar, false si no pudo modificar
         */
        public bool modificarCasoPrueba(EntidadCaso modificado, int idCaso)
        {
            bool resultado = false;

            try
            {
                //id_caso no por que es un autonumérico ??
                string insercion = "UPDATE Caso_Prueba SET identificador_caso = '" + modificado.getIdentificador_caso + "', proposito_caso = '" + modificado.getproposito_caso + "', flujo_central = '" + modificado.getFlujo_central + "', entrada_datos = '" + modificado.getEntrada_datos + "', resultado_esperado = '" + modificado.getResultado_esperado + "', id_diseno = '" + modificado.getId_diseno + "' WHERE id_caso = '" + idCaso + "'";

                resultado = acceso.insertarDatos(insercion);

            }
            catch (SqlException e)
            {
                resultado = false;
            }

            return resultado;
        }

        //metodo para consultar id de caso de prueba
        public int consultarIdCasoPrueba(string identificador)
        {
            int resultado = -1;

            try
            {
                //id_caso no por que es un autonumérico ??
                string consulta = "SELECT id_caso FROM Caso_Prueba WHERE identificador_caso ='" + identificador + "';";
                resultado = Int32.Parse(acceso.ejecutarConsulta(consulta).ToString());

            }
            catch (SqlException e)
            {
                resultado = -1;
            }

            return resultado;
        }

        //metodo para eliminar un caso de prueba
        public bool eliminarCasoPrueba(int idCaso)
        {
            try
            {
                string borradoProyecto = "Delete from Caso_Prueba where id_caso='" + idCaso + "';";
                acceso.eliminarDatos(borradoProyecto);
                return true;
            }
            catch (SqlException e)
            {
                return false;
            }
        }
        /* Método para consultar casos de prueba asociados al diseno
         * Requiere: el id del diseño 
         * Modifica: no modifica datos
         * Retorna: un DataTable que contiene los casos de prueba asociados al diseño
         */
        internal DataTable consultarCasosDePruebaAsociadoADiseno(int idDiseños)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            try
            {
                consulta = "SELECT C.id_caso, C.identificador_caso, C.proposito_caso, C.flujo_central, C.entrada_datos, C.resultado_esperado FROM Caso_Prueba C WHERE C.id_diseno ='" + idDiseños + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                dt = null;
            }
            return dt;
        }
        /* Método para consultar casos de prueba 
        * Requiere: el id del caso 
        * Modifica: no modifica datos
        * Retorna: un DataTable que contiene los casos de prueba
        */
        internal DataTable consultarCasoPrueba(int idCaso)
        {
            DataTable dt = new DataTable();
            string consulta = "";
            try
            {
                consulta = "SELECT C.id_caso, C.identificador_caso, C.proposito_caso, C.flujo_central, C.entrada_datos, C.resultado_esperado, C.id_diseno FROM Caso_Prueba C WHERE C.identificador_caso ='" + idCaso + "';";
                dt = acceso.ejecutarConsultaTabla(consulta);
            }
            catch (SqlException e)
            {
                dt = null;
            }
            return dt;
        }

    }
}