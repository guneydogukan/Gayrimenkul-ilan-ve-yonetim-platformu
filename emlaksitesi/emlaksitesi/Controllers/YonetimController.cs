using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlaksitesi.Controllers
{
    public class YonetimController : Controller
    {
        emlakEntities db = new emlakEntities();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(yonetim model)
        {
            var girisBilgileri = db.yonetim.Where(y => y.kullanici == model.kullanici).SingleOrDefault();
            if (girisBilgileri != null && girisBilgileri.kullanici == model.kullanici && girisBilgileri.sifre == model.sifre)
            {
                Session["adminID"] = girisBilgileri.adminID;
                Session["kullanici"] = girisBilgileri.kullanici;
                Session["ad"] = girisBilgileri.ad;
                Session["soyad"] = girisBilgileri.soyad;
                return RedirectToAction("Index","Yonetim");
            }
            ViewBag.Uyari = "Kullanıcı Adı ve/veya şifre yanlış!";
            return View(model);
        }

        public ActionResult logout()
        {
            Session["adminID"] = null;
            Session["kullanici"] = null;
            Session["ad"] = null;
            Session["soyad"] = null;
            Session.Abandon();
            return RedirectToAction("login", "Yonetim");
        }

    }
}