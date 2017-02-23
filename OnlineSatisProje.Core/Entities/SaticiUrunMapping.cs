using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("SaticiUrunMapping")]
    public class SaticiUrunMapping : BaseEntity
    {
        public SaticiUrunMapping()
        {
            CreatedDate = DateTime.Now;
        }

        public int SaticiId { get; set; }
        public int UrunId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Satici Satici { get; set; }
        public virtual Urun Urun { get; set; }
    }
}