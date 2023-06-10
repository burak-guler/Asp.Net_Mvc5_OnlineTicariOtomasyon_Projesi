using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int Cariid { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage ="En Fazla 30 Karakter Yazabilirsniz.!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz.!")]
        public string CariAd { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En Fazla 30 Karakter Yazabilirsniz.!")]
        [Required(ErrorMessage ="Bu Alanı Boş Geçemezsiniz.!")]
        public string CariSoyad { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string CariSehir { get; set; }

        //kısaltma(uzunluk) ve degisken tipi
        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En Fazla 50 Karakter Yazabilirsniz.!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz.!")]
        public string CariMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20, ErrorMessage = "En Fazla 20 Karakter Yazabilirsniz.!")]
        [Required(ErrorMessage = "Bu Alanı Boş Geçemezsiniz.!")]
        public string CariSifre { get; set; }

        public bool Durum { get; set; } 

        //iliskiler
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}