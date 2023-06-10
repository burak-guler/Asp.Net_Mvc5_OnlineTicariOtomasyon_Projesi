using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Personel
    {
        [Key]
        public int Personelid { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelAd { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelSoyad { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string PersonelGorsel { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string PersonelMail { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string PersonelTel { get; set; }

        public int Departmanid { get; set; }

        //iliskiler
        public ICollection<SatisHareket> SatisHarekets { get; set; }
        public virtual Departman Departman { get; set; }    
    }
}