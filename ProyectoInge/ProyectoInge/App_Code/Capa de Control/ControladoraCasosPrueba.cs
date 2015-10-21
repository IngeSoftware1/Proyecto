using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using ProyectoInge.App_Code.Capa_de_Datos__Entidad_;

namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraCasosPrueba
    {
        ControladoraBDCasosPrueba controladoraBDCasosPrueba = new ControladoraBDCasosPrueba();

        public bool eliminarProyectoCasoPueba(int idProyecto)
        {
            return controladoraBDCasosPrueba.eliminarProyectoCasoPueba(idProyecto);
        }
    }
}