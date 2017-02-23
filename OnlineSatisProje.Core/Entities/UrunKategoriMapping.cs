using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunKategoriMapping")]
    public class UrunKategoriMapping : BaseEntity
    {
        public UrunKategoriMapping()
        {
            CreatedDate = DateTime.Now;
        }

        public int UrunId { get; set; }
        public int KategoriId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Kategori Kategori { get; set; }
        public virtual Urun Urun { get; set; }
    }
}