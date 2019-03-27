using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntitiyFramework;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    public class PersonelController : Controller
    {
        PersonelDbEntities1 db = new PersonelDbEntities1();
        // GET: Personel
        public ActionResult Index()
        {
            var model = db.Personel.Include(x=>x.Departman).ToList();
             return View(model);
        }

        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel() {
                Departmanlar=db.Departman.ToList()
            };
            return View("PersonelForm",model);
        }

        public ActionResult Kaydet(Personel personel)
        {
            if (personel.Id == 0) //Ekleme işlemi
                db.Personel.Add(personel);
            else //Güncelle
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Personel");
        }

        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar=db.Departman.ToList(),
                Personel=db.Personel.Find(id)
            };
            return View("PersonelForm",model);
        }

        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel == null)
                return HttpNotFound();
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}