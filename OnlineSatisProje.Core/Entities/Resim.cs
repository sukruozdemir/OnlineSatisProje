using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Resim")]
    public class Resim : BaseEntity
    {
        public Resim()
        {
            Satici = new HashSet<Satici>();
            UrunResimMapping = new HashSet<UrunResimMapping>();
            CreatedDate = DateTime.Now;
        }

        [Required]
        public byte[] ResimBinary { get; set; }

        [StringLength(50)]
        public string Baslik { get; set; }

        [StringLength(50)]
        public string AltAttr { get; set; }

        [StringLength(50)]
        public string TitleAttr { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Satici> Satici { get; set; }
        public virtual ICollection<Kategori> Kategori { get; set; }
        public virtual ICollection<UrunResimMapping> UrunResimMapping { get; set; }
    }
}