using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("SaticiIndirimMapping")]
    public class SaticiIndirimMapping : BaseEntity
    {
        public int SaticiId { get; set; }
        public int IndirimId { get; set; }

        public virtual Indirim Indirim { get; set; }
        public virtual Satici Satici { get; set; }
    }
}