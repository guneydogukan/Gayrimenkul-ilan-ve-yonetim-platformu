using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using emlaksitesi.Models.database;

namespace emlaksitesi.Controllers
{
    public class AjansController : Controller
    {
        // GET: Ajans
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            var ajansBilgi = db.ajans.ToList();
            return View(ajansBilgi);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ajans model, HttpPostedFileBase profil_resim)
        {
            if (ModelState.IsValid)
            {
                if (profil_resim != null && profil_resim.ContentLength > 0)
                {
                    FileInfo imginfo = new FileInfo(profil_resim.FileName);
                    string[] allowedExtensions = new[] { ".jpg", ".png" };
                    if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                    {
                        WebImage img = new WebImage(profil_resim.InputStream);
                        string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                        img.Resize(1024, 360);
                        img.Save("~/uploads/images/" + yeniResimAdi);
                        model.profil_resim = yeniResimAdi;
                    }
                    else
                    {
                        ModelState.AddModelError("profil_resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                    }
                    db.ajans.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int kisi_id)
        {
            var ajansSil = db.ajans.Find(kisi_id);
            if (ajansSil != null)
            {
                db.ajans.Remove(ajansSil);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var ajansDuzenle = db.ajans.Find( id);
            if (ajansDuzenle == null)
            {
                return HttpNotFound();
            }
           
            return View(ajansDuzenle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ajans model, HttpPostedFileBase profil_resim)
        {
            if (ModelState.IsValid)
            {
                var duzenle = db.ajans.Find(model.kisi_id);
                if (duzenle == null)
                {
                    return HttpNotFound();
                }

                duzenle.ad_soyad = model.ad_soyad;
                duzenle.gorev = model.gorev;
                duzenle.aciklama = model.aciklama;
                duzenle.twitter = model.twitter;
                duzenle.instagram = model.instagram;
                duzenle.facebook = model.facebook;


                if (ModelState.IsValid)
                {
                    if (profil_resim != null && profil_resim.ContentLength > 0)
                    {
                        FileInfo imginfo = new FileInfo(profil_resim.FileName);
                        string[] allowedExtensions = new[] { ".jpg", ".png" };
                        if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                        {
                            if (System.IO.File.Exists(Server.MapPath(duzenle.profil_resim)))
                            {
                                System.IO.File.Delete(Server.MapPath(duzenle.profil_resim));
                            }
                            WebImage img = new WebImage(profil_resim.InputStream);
                            string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                            img.Resize(1024, 360);
                            img.Save("~/uploads/images/" + yeniResimAdi);
                            duzenle.profil_resim = yeniResimAdi;
                        }
                        else
                        {
                            ModelState.AddModelError("profil_resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                        }
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

