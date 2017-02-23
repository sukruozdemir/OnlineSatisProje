using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunResimMapping")]
    public class UrunResimMapping : BaseEntity
    {
        public int UrunId { get; set; }
        public int ResimId { get; set; }

        public virtual Resim Resim { get; set; }
        public virtual Urun Urun { get; set; }
    }
}