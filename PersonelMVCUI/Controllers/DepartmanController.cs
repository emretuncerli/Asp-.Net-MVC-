using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntitiyFramework;

namespace PersonelMVCUI.Controllers
{
    public class DepartmanController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        // GET: Departman
        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Yeni()
        {
            return View("DepartmanForm");
        }

        [HttpPost]
        public ActionResult Kaydet(Departman departman)
        {
            if (departman.Id == 0)
            {
                db.Departman.Add(departman);
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman == null)
                {
                    return HttpNotFound();
                }
                guncellenecekDepartman.Ad = departman.Ad;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Departman");
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
                return HttpNotFound();
            return View("DepartmanForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman == null)
                return HttpNotFound();
            db.Departman.Remove(silinecekDepartman);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
    }
}