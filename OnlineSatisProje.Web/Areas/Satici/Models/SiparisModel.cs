using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class SiparisModel
    {
        public int? KargoDurumId { get; set; }
        public int? OdemeDurumId { get; set; }
        public int? SiparisDurumId { get; set; }

        public SiparisItem SiparisItem { get; set; }
    }
}