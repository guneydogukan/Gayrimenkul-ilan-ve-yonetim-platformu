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
    public class AboutController : Controller
    {
        // GET: About
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            var aboutbilgi = db.hakkimizda.ToList();
            return View(aboutbilgi);
        }

        public ActionResult Edit(int id)
        {
            var AboutDuzenle = db.hakkimizda.Find(id);
            if (AboutDuzenle == null)
            {
                return HttpNotFound();
            }

            return View(AboutDuzenle);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(hakkimizda model, HttpPostedFileBase arkaplan_resim, HttpPostedFileBase resim)
        {
            if (ModelState.IsValid)
            {
                var duzenle = db.hakkimizda.Find(model.id);
                if (duzenle == null)
                {
                    return HttpNotFound();
                }


                duzenle.aciklama = model.aciklama;
                duzenle.aciklama1 = model.aciklama1;
                duzenle.aciklama2 = model.aciklama2;
                duzenle.aciklama3 = model.aciklama3;


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