using FormulaIFS.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FormulaIFSWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Visao()
        {
            return View(GetCategorias());
        }

        IEnumerable<Categoria> GetCategorias()
        {
            using (FormulaIFSContext db = new FormulaIFSContext())
            {
                return db.Categorias.ToList<Categoria>();
            }
        }
        public ActionResult AdicionarEditar(int id = 0)
        {
          
            Categoria categoria = new Categoria();
            if (id != 0)
            {
                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    categoria = db.Categorias.Where(x => x.Id == id).FirstOrDefault<Categoria>();
                }
            }
            return View(categoria);
        }
        [HttpPost]
        public ActionResult AdicionarEditar(Categoria emp)
        {
            try
            {

                using (FormulaIFSContext db = new FormulaIFSContext())
                {
                    if (emp.Id == 0)
                    {
                        db.Categorias.Add(emp);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(emp).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetCategorias()), message = "Salvo com Sucesso" }, JsonRequestBehavior.AllowGet);
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
                    Categoria emp = db.Categorias.Where(x => x.Id == id).FirstOrDefault<Categoria>();
                    db.Categorias.Remove(emp);
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Visao", GetCategorias()), message = "Deletado com sucesso" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}







