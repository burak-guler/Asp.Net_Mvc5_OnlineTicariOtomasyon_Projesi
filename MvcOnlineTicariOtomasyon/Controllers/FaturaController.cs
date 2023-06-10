using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Faturalars.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar F)
        {
            c.Faturalars.Add(F);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var deger =c.Faturalars.Find(id);
            return View(deger);
        }

        public ActionResult FaturaGuncelle(Faturalar F)
        {
            var degerler = c.Faturalars.Find(F.Faturaid);
            degerler.FaturaSiraNo = F.FaturaSiraNo;
            degerler.FaturaSeriNo = F.FaturaSeriNo;
            degerler.Tarih = F.Tarih;
            degerler.VergiDairesi = F.VergiDairesi;
            degerler.Saat = F.Saat;
            degerler.Toplam = F.Toplam;
            degerler.TeslimAlan = F.TeslimAlan;
            degerler.TeslimEden = F.TeslimEden;
            c.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult FaturaDetay(int id)
        {
            ViewBag.dgr = id;
            var degerler = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKalem(int id)
        {
            ViewBag.dgr = id;
            return View(id);
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem kalem)
        {
       
            c.FaturaKalems.Add(kalem);
            c.SaveChanges();
            return RedirectToAction("FaturaDetay");

            
        }

        
    }
}