using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Urun
    {
        [Key]
        public int Urunid { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName ="Varchar")]
        [StringLength(30)]
        public string UrunAd { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Marka { get; set; }
        public short Stok { get; set; }
        public decimal AlisFiyat { get; set; }
        public decimal SatisFiyat { get; set; }
        public bool Durum { get; set; }
        public int Kategoriid { get; set; } 

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string UrunGorsel { get; set; }

        //iliskiler
        public ICollection<SatisHareket> SatisHarekets { get; set; }
        public virtual Kategori Kategori { get; set; }
         
    }
}