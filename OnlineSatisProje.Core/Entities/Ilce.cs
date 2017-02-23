using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Ilce")]
    public class Ilce : BaseEntity
    {
        public Ilce()
        {
            Adres = new HashSet<Adres>();
            CreatedDate = DateTime.Now;
        }

        public int SehirId { get; set; }

        [Required]
        [StringLength(30)]
        public string Ad { get; set; }

        [StringLength(5)]
        public string PostaKodu { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Adres> Adres { get; set; }
        public virtual Sehir Sehir { get; set; }
    }
}