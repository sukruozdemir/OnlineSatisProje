using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("Kategori")]
    public class Kategori : BaseEntity
    {
        public Kategori()
        {
            UrunKategoriMapping = new HashSet<UrunKategoriMapping>();
            CreatedDate = DateTime.Now;
            UpdatedDate = CreatedDate;
        }

        [Required]
        [StringLength(150)]
        public string Ad { get; set; }

        public string Aciklama { get; set; }
        public int? AnaKategoriId { get; set; }
        public int? ResimId { get; set; }
        public bool Aktif { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<UrunKategoriMapping> UrunKategoriMapping { get; set; }
    }
}