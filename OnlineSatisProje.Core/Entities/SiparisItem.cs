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
        [Display(Name = "Sipari�")]
        public int SiparisId { get; set; }
        [Display(Name = "�r�n")]
        public int UrunId { get; set; }
        public int Miktar { get; set; }
        [Display(Name = "Sat�c�")]
        public int SaticiId { get; set; }
        [Display(Name = "�ndirim miktar�")]
        public decimal IndirimMiktari { get; set; }
        public decimal Fiyat { get; set; }

        public virtual ICollection<KargoItem> KargoItem { get; set; }
        public virtual Siparis Siparis { get; set; }
        public virtual Satici Satici { get; set; }
        public virtual Urun Urun { get; set; }
    }
}