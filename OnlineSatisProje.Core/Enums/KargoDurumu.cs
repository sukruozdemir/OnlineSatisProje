using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Core.Enums
{
    public enum KargoDurumu
    {
        [Display(Name = "Kargo yok")]
        KargoYok = 10,
        [Display(Name = "Beklemede")]
        Bekliyor = 20,
        [Display(Name = "Kargolandı")]
        Kargolandi = 30,
        [Display(Name = "İletildi")]
        Iletildi = 40
    }
}