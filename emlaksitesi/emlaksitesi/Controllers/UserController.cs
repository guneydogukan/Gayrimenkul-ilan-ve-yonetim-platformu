using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlaksitesi.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            var userBilgi = db.yonetim.ToList();
            return View(userBilgi);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(yonetim model)
        {
            db.yonetim.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
           

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int adminID)
        {
            var userSil = db.yonetim.Find(adminID);
            if (userSil != null)
            {
                db.yonetim.Remove(userSil);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var ajansDuzenle = db.yonetim.Find(id);
            if (ajansDuzenle == null)
            {
                return HttpNotFound();
            }

            return View(ajansDuzenle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(yonetim model)
        {
            if (ModelState.IsValid)
            {
                var duzenle = db.yonetim.Find(model.adminID);
                if (duzenle == null)
                {
                    return HttpNotFound();
                }

                duzenle.ad = model.ad;
                duzenle.soyad = model.soyad;
                duzenle.kullanici = model.kullanici;
                duzenle.sifre = model.sifre;
               

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}