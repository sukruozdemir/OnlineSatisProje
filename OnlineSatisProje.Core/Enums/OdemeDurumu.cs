using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Core.Enums
{
    public enum OdemeDurumu
    {
        [Display(Name = "Beklemede")]
        Bekliyor = 10,
        [Display(Name = "Ödendi")]
        Odendi = 20,
        [Display(Name = "İptal edildi")]
        IptalEdildi = 30
    }
}
