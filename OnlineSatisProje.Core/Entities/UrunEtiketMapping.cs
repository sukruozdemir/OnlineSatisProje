using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunEtiketMapping")]
    public class UrunEtiketMapping : BaseEntity
    {
        public int UrunId { get; set; }
        public int EtiketId { get; set; }

        public virtual Etiket Etiket { get; set; }
        public virtual Urun Urun { get; set; }
    }
}