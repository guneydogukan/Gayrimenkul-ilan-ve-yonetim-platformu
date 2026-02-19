using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;


namespace emlaksitesi.Controllers
{
    public class DetayController : Controller
    {
        // GET: Detay
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            var detayBilgi = db.detay.Include(x => x.ajans).ToList();
            return View(detayBilgi);
        }

        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            ViewBag.KisiID = new SelectList(db.ajans, "kisi_id", "ad_soyad");
            ViewBag.EvID = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(detay model, HttpPostedFileBase ev_resim1, HttpPostedFileBase ev_resim2, HttpPostedFileBase ev_resim3, int? evID)
        {
            if (evID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           // ID'yi model üzerinde kullanabilirsiniz
            model.ev_id = evID.Value;
            if (ModelState.IsValid)
            {
                
                if (ev_resim1 != null && ev_resim1.ContentLength > 0)
                {
                    FileInfo imginfo = new FileInfo(ev_resim1.FileName);
                    string[] allowedExtensions = new[] { ".jpg", ".png" };
                    if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                    {
                        WebImage img = new WebImage(ev_resim1.InputStream);
                        string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                        img.Resize(1024, 360);
                        img.Save("~/uploads/images/" + yeniResimAdi);
                        model.ev_resim1 = yeniResimAdi;
                    }
                    else
                    {
                        ModelState.AddModelError("Resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                    }
                   
                }


                if (ev_resim2 != null && ev_resim2.ContentLength > 0)
                {
                    FileInfo imginfo = new FileInfo(ev_resim2.FileName);
                    string[] allowedExtensions = new[] { ".jpg", ".png" };
                    if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                    {
                        WebImage img = new WebImage(ev_resim2.InputStream);
                        string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                        img.Resize(1024, 360);
                        img.Save("~/uploads/images/" + yeniResimAdi);
                        model.ev_resim2 = yeniResimAdi;
                    }
                    else
                    {
                        ModelState.AddModelError("Resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                    }
                   
                }


                if (ev_resim3 != null && ev_resim3.ContentLength > 0)
                {
                    FileInfo imginfo = new FileInfo(ev_resim3.FileName);
                    string[] allowedExtensions = new[] { ".jpg", ".png" };
                    if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                    {
                        WebImage img = new WebImage(ev_resim3.InputStream);
                        string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                        img.Resize(1024, 360);
                        img.Save("~/uploads/images/" + yeniResimAdi);
                        model.ev_resim3 = yeniResimAdi;
                    }
                    else
                    {
                        ModelState.AddModelError("Resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                    }
                    
                }
                db.detay.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KisiID = new SelectList(db.ajans, "kisi_id", "ad_soyad", model.kisidetay);
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var detaySil = db.detay.Find(id);
            if (detaySil != null)
            {
                db.detay.Remove(detaySil);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.KisiID = new SelectList(db.ajans, "kisi_id", "ad_soyad");
            var detayDuzenle = db.detay.Find(id);
            if (detayDuzenle == null)
            {
                return HttpNotFound();
            }

            return View(detayDuzenle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(detay model, HttpPostedFileBase ev_resim1, HttpPostedFileBase ev_resim2, HttpPostedFileBase ev_resim3)
        {
            if (ModelState.IsValid)
            {
                var duzenle = db.detay.Find(model.id);
                if (duzenle == null)
                {
                    return HttpNotFound();
                }

                
                duzenle.aciklama = model.aciklama;
                duzenle.kisidetay = model.kisidetay;
               


                if (ModelState.IsValid)
                {
                    if (ev_resim1 != null && ev_resim1.ContentLength > 0)
                    {
                        FileInfo imginfo = new FileInfo(ev_resim1.FileName);
                        string[] allowedExtensions = new[] { ".jpg", ".png" };
                        if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                        {
                            if (System.IO.File.Exists(Server.MapPath(duzenle.ev_resim1)))
                            {
                                System.IO.File.Delete(Server.MapPath(duzenle.ev_resim1));
                            }
                            WebImage img = new WebImage(ev_resim1.InputStream);
                            string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                            img.Resize(1024, 360);
                            img.Save("~/uploads/images/" + yeniResimAdi);
                            duzenle.ev_resim1 = yeniResimAdi;
                        }
                        else
                        {
                            ModelState.AddModelError("Resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                        }
                    }


                    if (ev_resim2 != null && ev_resim2.ContentLength > 0)
                    {
                        FileInfo imginfo = new FileInfo(ev_resim2.FileName);
                        string[] allowedExtensions = new[] { ".jpg", ".png" };
                        if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                        {
                            if (System.IO.File.Exists(Server.MapPath(duzenle.ev_resim2)))
                            {
                                System.IO.File.Delete(Server.MapPath(duzenle.ev_resim2));
                            }
                            WebImage img = new WebImage(ev_resim2.InputStream);
                            string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                            img.Resize(1024, 360);
                            img.Save("~/uploads/images/" + yeniResimAdi);
                            duzenle.ev_resim2 = yeniResimAdi;
                        }
                        else
                        {
                            ModelState.AddModelError("Resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                        }
                    }

                    if (ev_resim3 != null && ev_resim3.ContentLength > 0)
                    {
                        FileInfo imginfo = new FileInfo(ev_resim3.FileName);
                        string[] allowedExtensions = new[] { ".jpg", ".png" };
                        if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                        {
                            if (System.IO.File.Exists(Server.MapPath(duzenle.ev_resim3)))
                            {
                                System.IO.File.Delete(Server.MapPath(duzenle.ev_resim3));
                            }
                            WebImage img = new WebImage(ev_resim3.InputStream);
                            string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                            img.Resize(1024, 360);
                            img.Save("~/uploads/images/" + yeniResimAdi);
                            duzenle.ev_resim3 = yeniResimAdi;
                        }
                        else
                        {
                            ModelState.AddModelError("Resim", "Sadece .jpg, .png formatları desteklenmektedir.");
                        }
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.KisiID = new SelectList(db.ajans, "kisi_id", "ad_soyad", model.kisidetay);
            return View(model);
        }
    }
}