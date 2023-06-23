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

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();
            return View(cs); 
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSiraNo, DateTime Tarih , string VergiDairesi
            ,string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] Kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSiraNo=FaturaSiraNo;
            f.Tarih=Tarih;
            f.VergiDairesi=VergiDairesi;
            f.Saat=Saat;
            f.TeslimEden=TeslimEden;
            f.TeslimAlan=TeslimAlan;
            f.Toplam=decimal.Parse(Toplam);
            c.Faturalars.Add(f);

            foreach (var item in Kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama=item.Aciklama;
                fk.BirimFiyat=item.BirimFiyat;  
                fk.Faturaid = item.FaturaKalemid;
                fk.Miktar=item.Miktar;  
                fk.Tutar=item.Tutar;
                c.FaturaKalems.Add(fk);

            }
             

            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}