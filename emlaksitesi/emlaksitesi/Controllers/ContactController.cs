using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using emlaksitesi.Models.database;


namespace emlaksitesi.Controllers
{
    public class ContactController : Controller
    {
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            var contactbilgi = db.bize_ulasin.ToList();
            return View(contactbilgi);
        }

        public ActionResult Edit(int id)
        {
            var contactDuzenle = db.bize_ulasin.Find(id);
            if (contactDuzenle == null)
            {
                return HttpNotFound();
            }

            return View(contactDuzenle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(bize_ulasin model, HttpPostedFileBase arkaplan_resim)
        {
            if (ModelState.IsValid)
            {
                var duzenle = db.bize_ulasin.Find(model.id);
                if (duzenle == null)
                {
                    return HttpNotFound();
                }

                
                duzenle.konum_aciklama = model.konum_aciklama;
                duzenle.hizmet_saatleri = model.hizmet_saatleri;
                duzenle.email = model.email;
                duzenle.telefon_numarasi = model.telefon_numarasi;


                if (ModelState.IsValid)
                {
                    if (arkaplan_resim != null && arkaplan_resim.ContentLength > 0)
                    {
                        FileInfo imginfo = new FileInfo(arkaplan_resim.FileName);
                        string[] allowedExtensions = new[] { ".jpg", ".png" };
                        if (allowedExtensions.Contains(imginfo.Extension.ToLower()))
                        {
                            if (System.IO.File.Exists(Server.MapPath(duzenle.arkaplan_resim)))
                            {
                                System.IO.File.Delete(Server.MapPath(duzenle.arkaplan_resim));
                            }
                            WebImage img = new WebImage(arkaplan_resim.InputStream);
                            string yeniResimAdi = Guid.NewGuid().ToString() + imginfo.Extension;
                            img.Resize(1024, 360);
                            img.Save("~/uploads/images/" + yeniResimAdi);
                            duzenle.arkaplan_resim = yeniResimAdi;
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
            return View(model);
        }
    }
}