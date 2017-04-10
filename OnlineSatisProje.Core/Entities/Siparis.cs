using System;
using System.Collections.Generic;

namespace OnlineSatisProje.Core.Entities
{
    public class Siparis : BaseEntity
    {
        public Siparis()
        {
            Kargo = new HashSet<Kargo>();
            SiparisItem = new HashSet<SiparisItem>();
        }

        public Guid SiparisGuid { get; set; }
        public string KullaniciId { get; set; }
        public int KargoAdresId { get; set; }
        public int? SiparisDurumId { get; set; }
        public int? OdemeDurumId { get; set; }
        public int? KargoDurumId { get; set; }
        public decimal SiparisToplam { get; set; }
        public bool Aktif { get; set; }
        public bool Silindi { get; set; }

        public virtual Kullanici Kullanici { get; set; }
        public virtual Adres Adres { get; set; }
        public virtual ICollection<Kargo> Kargo { get; set; }
        public virtual ICollection<SiparisItem> SiparisItem { get; set; }
    }
}