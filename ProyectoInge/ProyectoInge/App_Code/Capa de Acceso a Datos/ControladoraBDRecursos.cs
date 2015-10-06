using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ProyectoInge.App_Code.Capa_de_Acceso_a_Datos;
using System.Data.SqlClient;

namespace ProyectoInge.App_Code.Capa_de_Acceso_a_Datos
{

    
    public class ControladoraBDRecursos
      {
     
         //RHTableAdapter adapterRH;                  
        AccesoBaseDatos acceso = new AccesoBaseDatos();
     public ControladoraBDRecursos()
      {
          //adapterRH = new RHTableAdapter();	
      }

     public DataTable consultarUsuario(String user, String pass)
     {
         DataTable dt = new DataTable();
         //dt = adapterRH.GetUsuario(user,pass);
         return dt;
     }

     public Boolean modificarContrasena(String user, String pass, String newPass)
     {
         DataTable dt = new DataTable();
         Boolean resultado = false;
         try
         {
             //dt = adapterRH.GetData(user, pass,newPass);
             //this.adapterRH.Insert(venta.ID, venta.Fecha, venta.Proveedor, venta.Descripcion, venta.NombreProducto, venta.CantidadInventario, venta.CantidadSolicitada); 
             resultado = true;
         }
         catch (SqlException e)
         {
             
         }

         return resultado;

     }


     public DataRow validarUsuario(string nombreUsuario, string password)
     {
         DataTable usuarioValidado = new DataTable();
         //= adapterRH.validarUsuario(nombreUsuario, password);
         if (usuarioValidado.Rows.Count == 1)
             return usuarioValidado.Rows[0];
         return null;
     }

   
     public DataTable consultarRH(string cedula) {
        DataTable dt = new DataTable();
        //dt = adapterRH.GetData(cedula);
        return dt;
    }

          /*
     
 /////De aquí hacía abajo hay que modificarlo para funcionarios porque lo copie de ventas
 public void insertarVenta(EntidadRH recurso) {
    try
    {
        //Hay que modificarlo para funcionario
        //this.adapterRH.Insert(venta.ID, venta.Fecha, venta.Proveedor, venta.Descripcion, venta.NombreProducto, venta.CantidadInventario, venta.CantidadSolicitada); 
    }
    catch (SqlException e)
    {
        int r = e.Number;
        if (r == 2627)
        {
            // "Ya existe una venta con este id";
        }
        else
        {
            //"Se ha producido un error al insertar la venta";
        }
    }
}

public void modificarVenta(EntidadVenta venta)
{
    try
    {
        //this.adapterVentas.Update(venta.ID, venta.Fecha, venta.Proveedor, venta.Descripcion, venta.NombreProducto, venta.CantidadInventario, venta.CantidadSolicitada, venta.ID); 
    }
    catch (SqlException e)
    {
        int r = e.Number;
        if (r == 2627)
        {
            // "Ya existe una venta con este id";
        }
        else
        {
            //"Se ha producido un error al modificar la venta";
        }
    }
}

public void eliminarCuenta(int idVenta) {
    try
    {
        //adapterVentas.Delete(idVenta);

    }
    catch (SqlException e)
    {
        //"Ha ocurrido un error al eliminar la venta";
    }
}

public DataTable consultarVenta(int idVenta) {
    DataTable dt = new DataTable();
    //dt = this.adapterVentas.consultarFila(idVenta);
    return dt;
}
      // GET: ControladoraBDRecursos
      public ActionResult Index()
      {
          return View();
      }

      // GET: ControladoraBDRecursos/Details/5
      public ActionResult Details(int id)
      {
          return View();
      }

      // GET: ControladoraBDRecursos/Create
      public ActionResult Create()
      {
          return View();
      }

      // POST: ControladoraBDRecursos/Create
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

      // GET: ControladoraBDRecursos/Edit/5
      public ActionResult Edit(int id)
      {
          return View();
      }

      // POST: ControladoraBDRecursos/Edit/5
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

      // GET: ControladoraBDRecursos/Delete/5
      public ActionResult Delete(int id)
      {
          return View();
      }

      // POST: ControladoraBDRecursos/Delete/5
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
      }*/
      }
}
