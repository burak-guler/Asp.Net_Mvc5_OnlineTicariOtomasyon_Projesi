using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Models.Siniflar.Context c = new Models.Siniflar.Context();
        
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }

        [Authorize(Roles ="A")]
        [HttpGet]
       
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult DepartmanEkle(Departman D)
        {
            c.Departmans.Add(D);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var deger = c.Departmans.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanGetir(int id)
        {
            var degerler = c.Departmans.Find(id);
            return View(degerler);
        }

        public ActionResult DepartmanGuncelle(Departman D)
        {
            var degerler = c.Departmans.Find(D.Departmanid);
            degerler.DepartmanAd = D.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.Departmanid == id).ToList();
            //Departman Adını Çekip viewbag ile gönderme
            //1.yol
            var deger = c.Departmans.Find(id).DepartmanAd;
            //2.yol
            //var deger = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.dgr = deger;
            return View(degerler);
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var deger = c.Personels.Where(x => x.Personelid == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.dgr = deger;
            return View(degerler);
        }
    }
}