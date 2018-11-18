using FormulaIFS.Model;
using FormulaIFSWeb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormulaIFS.ViewController.Controllers
{
    public class RegistroDisputaController : Controller
    {
        // GET: RegistroDisputa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visao()
        {
            return View(GetRegistroDisputas());
        }

        IEnumerable<RegistroDisputa> GetRegistroDisputas()
        {
            using (FormulaIFSContext db = new FormulaIFSContext())
            {
                return db.RegistroDisputa.ToList<RegistroDisputa>();
            }
        }
        public ActionResult AdicionarEditar(int id = 0)
        {

            RegistroDisputa registroDisputa = new RegistroDisputa();
            if (id != 0)
            {
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    registroDisputa = db.RegistroDisputa.Where(x => x.Id == id).FirstOrDefault<RegistroDisputa>();
                }
            }
            return View(registroDisputa);
        }
        [HttpPost]
        public ActionResult AdicionarEditar(RegistroDisputa emp)
        {
            try
            {

                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    if (emp.Id == 0)
                    {
                        db.RegistroDisputa.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetRegistroDisputas()), message = "Salvo com Sucesso" }, JsonRequestBehavior.AllowGet);
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
                    RegistroDisputa emp = db.RegistroDisputa.Where(x => x.Id == id).FirstOrDefault<RegistroDisputa>();
                    db.RegistroDisputa.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetRegistroDisputas()), message = "Deletado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}

    
