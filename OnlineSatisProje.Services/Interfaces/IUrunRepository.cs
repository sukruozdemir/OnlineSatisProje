using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface IUrunRepository
    {
        IList<Urun> GetAll();

        void ResimEkle(int urunId, Resim resim);
    }
}
