using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Etiket")]
    public class Etiket : BaseEntity
    {
        public Etiket()
        {
            SaticiEtiketMapping = new HashSet<SaticiEtiketMapping>();
            UrunEtiketMapping = new HashSet<UrunEtiketMapping>();
            CreatedDate = DateTime.Now;
        }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<SaticiEtiketMapping> SaticiEtiketMapping { get; set; }

        public virtual ICollection<UrunEtiketMapping> UrunEtiketMapping { get; set; }
    }
}