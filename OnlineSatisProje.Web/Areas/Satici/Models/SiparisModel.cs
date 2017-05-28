using System.ComponentModel.DataAnnotations;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class SiparisModel
    {
        [Display(Name = "Sipariş")]
        public int SiparisId { get; set; }

        [Display(Name = "Kargo durumu")]
        public int? KargoDurumId { get; set; }

        [Display(Name = "Ödeme durumu")]
        public int? OdemeDurumId { get; set; }

        [Display(Name = "Sipariş durumu")]
        public int? SiparisDurumId { get; set; }
    }
}