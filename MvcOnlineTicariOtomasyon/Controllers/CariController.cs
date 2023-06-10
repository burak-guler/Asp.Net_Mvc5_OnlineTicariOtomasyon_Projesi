using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var car = c.Carilers.Where(p=>p.Durum==true).ToList();
            return View(car);
        }

        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniCari(Cariler cari)
        {
            c.Carilers.Add(cari);
            c.SaveChanges();
            return RedirectToAction("Index");   
        }

        public ActionResult CariSil(int id)
        {
            var degerler = c.Carilers.Find(id);
            degerler.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var deger = c.Carilers.Find(id);
            return View(deger);
        }

        public ActionResult CariGuncelle (Cariler cari)
        {
            //models'in geçerlemesi doğru değil ise if içine girer.
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }

            var degerler = c.Carilers.Find(cari.Cariid);
            degerler.CariAd = cari.CariAd;
            degerler.CariSoyad=cari.CariSoyad;
            degerler.CariSehir=cari.CariSehir;
            degerler.CariMail = cari.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(z => z.Cariid == id).ToList();
            var Musteri = c.Carilers.Where(z => z.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.mus = Musteri;
            return View(degerler);
        }
    }
}