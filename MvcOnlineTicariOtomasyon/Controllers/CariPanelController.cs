using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(p => p.Cariid).FirstOrDefault();
            ViewBag.mid=mailid;
            var toplamsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.satissayi=toplamsatis;
            var toplamtutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.ToplamTutar);
            ViewBag.tutar=toplamtutar;
            var toplamurunsayisi= c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.toplamadet=toplamurunsayisi;

            ViewBag.AdSoyad = c.Carilers.Where(x => x.Cariid == mailid).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.CariMail= c.Carilers.Where(x => x.Cariid == mailid).Select(y => y.CariMail).FirstOrDefault();
            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(a => a.Cariid == id).ToList() ;
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(y=>y.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1=gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderici == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;  
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(y => y.MesajID).ToList();
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderici == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderici == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;

            var mesaj = c.Mesajlars.Where(x=>x.MesajID==id).ToList();
            return View(mesaj);
        }

        [HttpGet]
        [Authorize]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = c.Mesajlars.Where(x => x.Alici == mail).Count().ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = c.Mesajlars.Where(x => x.Gonderici == mail).Count().ToString();
            ViewBag.d2 = gidensayisi;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult YeniMesaj(Mesajlar mesaj)
        {
            var mail = (string)Session["CariMail"];
            mesaj.Gonderici = mail;
            mesaj.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.Mesajlars.Add(mesaj);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var kargo = from x in c.KargoDetays select x;            
            kargo = kargo.Where(y => y.TakipKodu.Contains(p));
            return View(kargo.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault() ;
            var degerler = c.Carilers.Find(id);
            return PartialView(degerler);
        }

        
        public ActionResult Guncelle(Cariler cari)
        {
            var degerler = c.Carilers.Find(cari.Cariid);
            degerler.CariAd=cari.CariAd;
            degerler.CariSoyad=cari.CariSoyad;
            degerler.CariSehir = cari.CariSehir;
            degerler.CariMail = cari.CariMail;
            degerler.CariSifre=cari.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }



    }
}