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
    [Authorize(Roles = "Docente")]
    public class EquipeController : Controller
    {
        // GET: Equipe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visao()
        {
            return View(GetEquipes());
        }

        IEnumerable<Equipe> GetEquipes()
        {
            using (FormulaIFSContext db = new FormulaIFSContext())
            {
                return db.Equipes.ToList<Equipe>();
            }

        }
        public ActionResult AdicionarEditar(int id = 0)
        {
            Equipe equipe = new Equipe();
            if (id != 0)
            {
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    equipe = db.Equipes.Where(x => x.Id == id).FirstOrDefault<Equipe>();
                }
            }
            return View(equipe);
        }




        [HttpPost]
        public ActionResult AdicionarEditar(Equipe emp)
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
                        db.Equipes.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetEquipes()), message = "Salvo com Sucesso" }, JsonRequestBehavior.AllowGet);
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
                    Equipe emp = db.Equipes.Where(x => x.Id == id).FirstOrDefault<Equipe>();
                    db.Equipes.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetEquipes()), message = "Deletado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}

    
