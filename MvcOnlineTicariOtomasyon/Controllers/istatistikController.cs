using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        // GET: İstatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1= c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;
            var deger5 = c.Uruns.Sum(x=>x.Stok).ToString(); 
            ViewBag.d5 = deger5;
            var deger6 = c.Uruns.Select(x => x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;
            var deger7 = c.Uruns.Count(x => x.Stok<=20).ToString();
            ViewBag.d7 = deger7;
            //var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            var deger8 = c.Uruns.OrderByDescending(x => x.SatisFiyat).Select(x => x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;
            var deger9 = c.Uruns.OrderBy(c => c.SatisFiyat).Select(x => x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;
            var deger10 = c.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10= deger10;
            var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;
            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault();
            ViewBag.d12 = deger12;
            var deger13 = c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault())).Select(p=>p.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;
            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;
            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
            ViewBag.d15 = deger15;

            var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(x =>(decimal?) x.ToplamTutar).ToString();
            ViewBag.d16 = deger16;
            return View();
        }

        public ActionResult KolayTablolar()
        {
            var sorgu = (from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        }).OrderByDescending(x=>x.Sayi).Take(5);
            int deger = c.Carilers.Where(z => z.Durum == true).Count();
            ViewBag.dgr1 = deger;
            return View(sorgu.ToList());
        }

        public PartialViewResult Partial1()
        {
            var sorgu2 = (from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         }).OrderByDescending(x => x.Sayi).Take(5);
            int deger = c.Carilers.Count();
            ViewBag.dgr1 = deger;

           
            return PartialView(sorgu2.ToList());
        }

        public PartialViewResult Partial2()
        {
            var degerler = c.Carilers.Where(x => x.Durum == true).ToList();
            return PartialView(degerler);
        }

        public PartialViewResult Partial3()
        {
            var deger = c.Uruns.ToList();
            return PartialView(deger);
        }

        public PartialViewResult Partial4()
        {
            var sorgu = (from x in c.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             Sayi = g.Count(),
                             Marka = g.Key
                         }).OrderByDescending(x => x.Sayi).Take(5);
            int deger = c.Uruns.Count();
            ViewBag.dgr1 = deger;
            return PartialView(sorgu);
        }
    }
}