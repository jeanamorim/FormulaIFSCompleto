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
    public class CircuitoController : Controller
    {
        // GET: Circuito
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visao()
        {
            return View(GetCircuitos());
        }

        IEnumerable<Circuito> GetCircuitos()
        {
            using (FormulaIFSContext db = new FormulaIFSContext())
            {
                return db.Circuitos.ToList<Circuito>();
            }
        }
        public ActionResult AdicionarEditar(int id = 0)
        {
            Circuito circuito = new Circuito();
            if (id != 0)
            {
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    circuito = db.Circuitos.Where(x => x.Id == id).FirstOrDefault<Circuito>();
                }
            }
            return View(circuito);
        }




        [HttpPost]
        public ActionResult AdicionarEditar(Circuito emp)
        {
            try
            {
                if (emp.ImagemUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(emp.ImagemUpload.FileName);
                    string extension = Path.GetExtension(emp.ImagemUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    emp.Imagem = "~/Content/Imagens/" + fileName;
                    emp.ImagemUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Imagens/"), fileName));
                }
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    if (emp.Id == 0)
                    {
                        db.Circuitos.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetCircuitos()), message = "Salvo com Sucesso" }, JsonRequestBehavior.AllowGet);
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
                    Circuito emp = db.Circuitos.Where(x => x.Id == id).FirstOrDefault<Circuito>();
                    db.Circuitos.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetCircuitos()), message = "Deletado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
