using emlaksitesi.Models.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlaksitesi.Controllers
{
    public class MessagesController : Controller
    {
        emlakEntities db = new emlakEntities();
        // GET: Messages
        public ActionResult Index()
        {
            var mesajlar = db.mesaj.ToList();
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult Read(int id)
        {
            var mesajdetay = db.mesaj.Find(id);

            if (mesajdetay == null)
            {
                return HttpNotFound();
            }
            return View(mesajdetay);
        }

    }
}