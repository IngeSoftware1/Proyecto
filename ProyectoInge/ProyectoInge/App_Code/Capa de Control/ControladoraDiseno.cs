using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraDiseno
    {
        ControladoraBDDiseno controladoraBDDiseno = new ControladoraBDDiseno();

        /*Método para buscar, yendo a la base de datos, si existe un usuario con una cedula particular, que sea líder de diseño
        * Requiere: un string con la cedula de un funcionario específico
        * Modifica: Genera la consulta para mandarla a la base de datos para saber si hay un usuario líder de diseño.
        * Retorna: true si se encontro que el funcionario tenía era líder y false si no.
        */
        public bool buscarAsignacionMiembrosDiseno(string cedula)
        {
            return controladoraBDDiseno.buscarAsignacionMiembrosDiseno(cedula);
        }
    }
}