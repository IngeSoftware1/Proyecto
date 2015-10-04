using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace ProyectoInge.App_Code.Capa_de_Control
{
    public class ControladoraRecursos 
    {
        ControladoraBDRecursos controladoraBDRecursos = new ControladoraBDRecursos();


        public ControladoraRecursos()
	    {

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
    }
}
