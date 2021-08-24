using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        //
        // GET: /Departamento/
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            ML.Result result = BL.Departamento.GetAll();
            departamento.Departamentos = result.Objects.ToList();
            return View(departamento);
        }
        [HttpPost]
        public ActionResult Importar(HttpPostedFileBase ImportarArchivo)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            string Ruta = string.Empty;

            if (ImportarArchivo != null)
            {

                string line;
                bool PrimeraLinea = true;

                using (var reader = new StreamReader(ImportarArchivo.InputStream))
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!PrimeraLinea)
                        {
                            var temp = line.Split('|');

                            ML.Departamento departamento = new ML.Departamento();

                            //departamento.DepartamentoID = Convert.ToInt16(temp[0]);
                            departamento.Descripcion = temp[0];
                            result.Objects.Add(departamento);

                            BL.Departamento.Add(departamento);

                        }
                        else
                        {
                            PrimeraLinea = false;
                        }

                    }
                }
            }
            return RedirectToAction("GetAll");
        }
        public ActionResult Delete(int DepartamentoID)
        {
            ML.Result result = BL.Departamento.Delete(DepartamentoID);
            return RedirectToAction("GetAll");
        }
	}
}