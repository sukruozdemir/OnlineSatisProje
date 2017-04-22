using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("SiparisItem")]
    public class SiparisItem : BaseEntity
    {
        public SiparisItem()
        {
            KargoItem = new HashSet<KargoItem>();
        }

        public Guid SiparisItemGuid { get; set; }
        [Display(Name = "Sipariþ")]
        public int SiparisId { get; set; }
        [Display(Name = "Ürün")]
        public int UrunId { get; set; }
        public int Miktar { get; set; }
        [Display(Name = "Satýcý")]
        public int SaticiId { get; set; }
        [Display(Name = "Ýndirim miktarý")]
        public decimal IndirimMiktari { get; set; }
        public decimal Fiyat { get; set; }

        public virtual ICollection<KargoItem> KargoItem { get; set; }
        public virtual Siparis Siparis { get; set; }
        public virtual Satici Satici { get; set; }
        public virtual Urun Urun { get; set; }
    }
}