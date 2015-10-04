using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace ProyectoInge.App_Code.Capa_de_Control
{
    /*  public class ControladoraRecursos 
      {
          ControladoraBDRecursos controladoraBDRecursos = new ControladoraBDRecursos();


          public ControladoraRecursos()
          {

          }

         //Método para llenar obtener todos los recursos humanos del sistema
         //Requiere: Requiere la cedula del usuario actual
         //Modifica: Obtiene el datable de todos los recursos humanos del sistema
         //Retorna: Un datatable
         public DataTable consultarRecursosHumanos(string cedula)
          {
              return controladoraBDRecursos.consultarRH(cedula);
          }
     
     
     //Método para determinar la acción que se esta realizando en la interfaz
     //Requiere: El modo de operación del IMEC y el array de datos del Funcionario
     //Modifica: De acuerdo al modo del IMEC realiza una acción distinta
     //Retorna: No retorna ningún valor
      public void ejecutarAccion(int modo, Object [] datos) {
        switch (modo) {
            case 1: { // INSERTAR
                Funcionario funcionario = new Funcionario(datos);
                controladoraBDRecursos.insertarRecurso(funcionario);
                
            };
            break;
            case 2:
            { // MODIFICAR
               Funcionario funcionario = new Funcionario(datos);
                controladoraBDRecursos.modificarRecurso(funcionario);

            };
            break;
        }
    }
     
      
     //Metodo para llamar a eliminar 
     //Requiere: el modo de operación del IMEC y la string de la cedula a eliminar
     //Modifica: Elimina de la base de datos el funcionario
     //Retorna: No retorna ningún valor
       public void ejecutarAccion(int modo, string cedula)
            {
                if(modo==3){
                    controladoraBDVenta.eliminarRecurso(cedula);
                }
            }

       //Metodo para consultar a la base de datos un recurso
       //Requiere:  la string de la cedula a consultar
       //Modifica: Consulta en la base de datos el funcionario especifico
      //Retorna: Retorna un datatable
       public DataTable consultarRecurso(string cedula) {
                return controladoraBDRecursos.consultarRH(cedula);
            }
      
      
          public DataTable consultarLoginRecursosHumanos(string user, string password)
          {
              return controladoraBDRecursos.consultarVenta(user, password);
          }

          // GET: ControladoraRecursos
          public ActionResult Index()
          {
              return View();
          }

          // GET: ControladoraRecursos/Details/5
          public ActionResult Details(int id)
          {
              return View();
          }

          // GET: ControladoraRecursos/Create
          public ActionResult Create()
          {
              return View();
          }

          // POST: ControladoraRecursos/Create
          [HttpPost]
          public ActionResult Create(FormCollection collection)
          {
              try
              {
                  // TODO: Add insert logic here

                  return RedirectToAction("Index");
              }
              catch
              {
                  return View();
              }
          }

          // GET: ControladoraRecursos/Edit/5
          public ActionResult Edit(int id)
          {
              return View();
          }

          // POST: ControladoraRecursos/Edit/5
          [HttpPost]
          public ActionResult Edit(int id, FormCollection collection)
          {
              try
              {
                  // TODO: Add update logic here

                  return RedirectToAction("Index");
              }
              catch
              {
                  return View();
              }
          }

          // GET: ControladoraRecursos/Delete/5
          public ActionResult Delete(int id)
          {
              return View();
          }

          // POST: ControladoraRecursos/Delete/5
          [HttpPost]
          public ActionResult Delete(int id, FormCollection collection)
          {
              try
              {
                  // TODO: Add delete logic here

                  return RedirectToAction("Index");
              }
              catch
              {
                  return View();
              }
          }
      }*/
}
