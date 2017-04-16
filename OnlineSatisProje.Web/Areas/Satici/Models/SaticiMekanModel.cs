using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSatisProje.Web.Areas.Satici.Models
{
    public class SaticiMekanModel
    {
        public string Baslik { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }

        public int IlceId { get; set; }
        
        public string Aciklama { get; set; }
        
    }
}