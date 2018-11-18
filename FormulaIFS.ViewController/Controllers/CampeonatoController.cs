using FormulaIFS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormulaIFSWeb.Controllers
{
    [Authorize(Roles = "Docente, Admin")]
    public class CampeonatoController : Controller
    {
        // GET: Circuito
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visao()
        {
            return View(GetCampeonatos());
        }

        IEnumerable<Campeonato> GetCampeonatos()
        {
            using (FormulaIFSContext db = new FormulaIFSContext())
            {
               
                return db.Campeonatos.ToList<Campeonato>();
            }
        }
        public ActionResult AdicionarEditar(int id = 0)
        {
            Campeonato campeonato = new Campeonato();
            if (id != 0)
            {
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                   
                    campeonato = db.Campeonatos.Where(x => x.Id == id).FirstOrDefault<Campeonato>();
                }
            }
            return View(campeonato);
        }




        [HttpPost]
        public ActionResult AdicionarEditar(Campeonato emp)
        {
            try
            {
             
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                   
                    if (emp.Id == 0)
                    {
                        db.Campeonatos.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetCampeonatos()), message = "Salvo com Sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    Campeonato emp = db.Campeonatos.Where(x => x.Id == id).FirstOrDefault<Campeonato>();
                    db.Campeonatos.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetCampeonatos()), message = "Deletado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
