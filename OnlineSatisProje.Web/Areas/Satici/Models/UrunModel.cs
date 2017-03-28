using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class UrunModel
    {
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Display(Name = "Kısa açıklama")]
        public string KisaAciklama { get; set; }

        [Display(Name = "Uzun açıklama")]
        public string UzunAciklama { get; set; }

        [Display(Name = "Anasayfada göster")]
        public bool AnasayfadaGoster { get; set; }

        [Display(Name = "Kullanıcıların yorum yapmasına izin ver")]
        public bool YorumIzinVer { get; set; }

        [Display(Name = "Kargo aktif")]
        public bool KargoAktif { get; set; }

        [Display(Name = "Ücretsiz kargo")]
        public bool UcretsizKargo { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }

        [Display(Name = "Ürünü yayınla")]
        public bool Yayinlandi { get; set; }
    }
}