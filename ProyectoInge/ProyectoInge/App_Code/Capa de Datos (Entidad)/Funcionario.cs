using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoInge.App_Code.Capa_de_Datos__Entidad_
{
    /* public class Funcionario : Controller
     {

     //Atributos de la tabla Funcionario
  
         private String cedula;
         private String nombre;
         private String apellido1;
         private String apellido2;
         private String usuario;
         private String contrasena;
         private int login;

     //Metodo constructor de la clase Entidad Funcionario
     //Requiere: objeto con la colección de atributos de la tabla Funcionario
     //Modifica: Los valores de los atributos de la clase con los que vienen en el objeto
     //Retorna: No retorna ningún valor por ser constructor
         public Funcionario(Object[] datos)
         {
             this.cedula = datos[0].ToString();
             this.nombre = datos[1].ToString();
             this.apellido1 = datos[2].ToString();
             this.apellido2 = datos[3].ToString();
             this.usuario = datos[4].ToString();
             this.contrasena = datos[4].ToString();
             this.login = Convert.ToInt32(datos[6].ToString());
         }

        //Metodos set y get del atributo cedula
         public String getCedula
         {
             get { return cedula; }
             set { cedula = value; }
         }
     
         //Metodos set y get del atributo nombre
         public String getNombre
         {
             get { return nombre; }
             set { nombre = value; }
         }
      
         //Metodos set y get del atributo apellido1
         public String getApellido1
         {
             get { return apellido1; }
             set { apellido1 = value; }
         }
     
         //Metodos set y get del atributo apellido2
         public String getApellido2
         {
             get { return apeelido2; }
             set { apellido2 = value; }
         }
      
         //Metodos set y get del atributo usuario
         public String getUsuario
         {
             get { return usuario; }
             set { usuario = value; }
         }

         //Metodos set y get del atributo contraseña
         public String getContrasena
         {
             get { return contrasena; }
             set { contrasena = value; }
         }

         //Metodos set y get del atributo login
         public int getLogin
         {
             get { return login; }
             set { login = value; }
         }


     /////Supongo que de aquí hacía abajo son métodos necesarios para el modulo de seguridad
         // GET: Funcionario
         public ActionResult Index()
         {
             return View();
         }

         // GET: Funcionario/Details/5
         public ActionResult Details(int id)
         {
             return View();
         }

         // GET: Funcionario/Create
         public ActionResult Create()
         {
             return View();
         }

         // POST: Funcionario/Create
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

         // GET: Funcionario/Edit/5
         public ActionResult Edit(int id)
         {
             return View();
         }

         // POST: Funcionario/Edit/5
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

         // GET: Funcionario/Delete/5
         public ActionResult Delete(int id)
         {
             return View();
         }

         // POST: Funcionario/Delete/5
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
