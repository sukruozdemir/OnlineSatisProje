using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Kargo")]
    public class Kargo : BaseEntity
    {
        public Kargo()
        {
            KargoItem = new HashSet<KargoItem>();
            CreatedDate = DateTime.Now;
        }

        public int SiparisId { get; set; }
        public DateTime? KargolamaTarihi { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Siparis Siparis { get; set; }
        public virtual ICollection<KargoItem> KargoItem { get; set; }
    }
}