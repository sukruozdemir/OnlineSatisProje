using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Core.Enums
{
    public enum SiparisDurumu
    {
        [Display(Name = "Beklemede")] Bekliyor = 10,
        [Display(Name = "İşlemde")] Islemde = 20,
        [Display(Name = "Tamamlandı")] Tamamlandi = 30,
        [Display(Name = "İptal edildi")] IptalEdildi = 40
    }
}