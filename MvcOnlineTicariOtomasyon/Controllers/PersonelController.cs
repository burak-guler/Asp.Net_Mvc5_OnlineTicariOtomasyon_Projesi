using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var per = c.Personels.ToList();
            return View(per);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel perso)
        {
            if (Request.Files.Count>0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                perso.PersonelGorsel= "/image/" + dosyaadi + uzanti; 
            }

            c.Personels.Add(perso);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var personeller = c.Personels.Find(id);
            return View(personeller);
        }

        public ActionResult PersonelGuncelle(Personel perso)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                perso.PersonelGorsel = "/image/" + dosyaadi + uzanti;
            }

            var per = c.Personels.Find(perso.Personelid);
            per.PersonelAd= perso.PersonelAd;
            per.PersonelSoyad = perso.PersonelSoyad;
            per.PersonelGorsel = perso.PersonelGorsel;
            per.Departmanid= perso.Departmanid;
            c.SaveChanges();
            return RedirectToAction("PersoneListe");
        }

        public ActionResult PersonelListe()
        {
            var deger = c.Personels.ToList();
            return View(deger);
        }

    }
}