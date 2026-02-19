using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace emlaksitesi.Controllers
{
    public class SliderController : Controller
    {
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            var propertiesBilgi = db.slider.ToList();
            return View(propertiesBilgi);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(slider model, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                if (resim != null && resim.ContentLength > 0)
                {
                    FileInfo imginfo = new FileInfo(resim.FileName);
                    string[] allowedExtensions = new[] { ".jpg", ".png" };
                    if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                    {
                        WebImage img = new WebImage(resim.InputStream);
                        string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                        img.Resize(1024, 360);
                        img.Save("~/uploads/images/" + yeniResimAdi);
                        model.resim = yeniResimAdi;
                    }
                    else
                    {
                        ModelState.AddModelError("profil_resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                    }
                    db.slider.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            var s = db.slider.Find(id);
            if (s != null)
            {
                db.slider.Remove(s);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var sliderDuzenle = db.slider.Find(id);
            if (sliderDuzenle == null)
            {
                return HttpNotFound();
            }

            return View(sliderDuzenle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(slider model, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                var duzenle = db.slider.Find(model.slider_id);
                if (duzenle == null)
                {
                    return HttpNotFound();
                }

                duzenle.baslik = model.baslik;
                duzenle.sehir = model.sehir;
                duzenle.ilce = model.ilce;
                duzenle.adres = model.adres;
                duzenle.fiyat = model.fiyat;
                duzenle.yatak_sayisi = model.yatak_sayisi;
                duzenle.banyo_sayisi = model.banyo_sayisi;
                duzenle.durum = model.durum;


                if (ModelState.IsValid)
                {
                    if (resim != null && resim.ContentLength > 0)
                    {
                        FileInfo imginfo = new FileInfo(resim.FileName);
                        string[] allowedExtensions = new[] { ".jpg", ".png" };
                        if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                        {
                            if (System.IO.File.Exists(Server.MapPath(duzenle.resim)))
                            {
                                System.IO.File.Delete(Server.MapPath(duzenle.resim));
                            }
                            WebImage img = new WebImage(resim.InputStream);
                            string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                            img.Resize(1024, 360);
                            img.Save("~/uploads/images/" + yeniResimAdi);
                            duzenle.resim = yeniResimAdi;
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