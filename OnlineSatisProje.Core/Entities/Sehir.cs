using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Sehir")]
    public class Sehir : BaseEntity
    {
        public Sehir()
        {
            Ilce = new HashSet<Ilce>();
            CreatedDate = DateTime.Now;
        }

        [Required]
        [StringLength(30)]
        public string Ad { get; set; }

        [Required]
        [StringLength(2)]
        public string Plaka { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Ilce> Ilce { get; set; }
    }
}