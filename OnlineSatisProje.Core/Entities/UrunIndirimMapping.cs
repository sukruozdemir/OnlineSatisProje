using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("UrunIndirimMapping")]
    public class UrunIndirimMapping : BaseEntity
    {
        public int UrunId { get; set; }
        public int IndirimId { get; set; }

        public virtual Indirim Indirim { get; set; }
        public virtual Urun Urun { get; set; }
    }
}