using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("SaticiEtiketMapping")]
    public class SaticiEtiketMapping : BaseEntity
    {
        public SaticiEtiketMapping()
        {
            CreatedDate = DateTime.Now;
        }

        public int SaticiId { get; set; }
        public int EtiketId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Etiket Etiket { get; set; }
        public virtual Satici Satici { get; set; }
    }
}