using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data; // Eklenecek 2 kod
using System.Data.Entity;
using PagedList;

namespace emlaksitesi.Controllers
{
    public class HomeController : Controller
    {
        emlakEntities db = new emlakEntities();
        public ActionResult Index()
        {
            ViewBag.Anasayfa = db.anasayfa;
            ViewBag.Slider = db.slider.Include(b => b.detay);
            ViewBag.Ajans = db.ajans;
            return View();
        }


        public ActionResult About()
        {
            var about = db.hakkimizda;
            return View(about.ToList());
        }

        public ActionResult Contact()
        {
           ViewBag.iletisim = db.bize_ulasin;
            return View();
        }
        public ActionResult Properties(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var sliders = db.slider.OrderByDescending(m => m.slider_id).ToPagedList<slider>(_sayfaNo, 3);
            ViewBag.P_Slider = db.slider.Include(b => b.detay);
            return View(sliders);
        }
       

        public ActionResult Detay(int ID)
        {
            var detaylar = db.detay.Include(b => b.ajans).FirstOrDefault(h => h.ev_id == ID);
            return View(detaylar);
        }

        

        [HttpPost]
        public ActionResult Mesaj(mesaj model)
        {
            if (ModelState.IsValid)
            {
                db.mesaj.Add(model);
                db.SaveChanges();
                RedirectToAction("Contact");
            }
            return View(model);
        }




    }

}