using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSatisProje.Core.Entities
{
    [Table("KargoItem")]
    public class KargoItem : BaseEntity
    {
        public int KargoId { get; set; }
        public int SiparisItemId { get; set; }
        public int Miktar { get; set; }

        public virtual Kargo Kargo { get; set; }
        public virtual SiparisItem SiparisItem { get; set; }
    }
}