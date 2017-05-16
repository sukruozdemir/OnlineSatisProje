using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class IndirimModel
    {
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }
        [Display(Name = "Yüzde kullan")]
        public bool YuzdeKullan { get; set; }
        [Display(Name = "Indirim yüzdesi")]
        public int IndirimYuzdesi { get; set; }
        [Display(Name = "Indirim miktarı")]
        public int IndirimMiktari { get; set; }
        [Display(Name = "Başlangıç tarihi")]
        public string BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş tarihi")]
        public string BitisTarihi { get; set; }
    }
}