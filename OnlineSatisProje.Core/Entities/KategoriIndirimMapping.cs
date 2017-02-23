using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("KategoriIndirimMapping")]
    public class KategoriIndirimMapping : BaseEntity
    {
        public int KategoriId { get; set; }
        public int IndirimId { get; set; }

        public virtual Indirim Indirim { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}