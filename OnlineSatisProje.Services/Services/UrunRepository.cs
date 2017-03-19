using System.Collections.Generic;
using System.Linq;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class UrunRepository : IUrunRepository
    {
        private readonly IRepository<Urun> _repository;

        public UrunRepository(IRepository<Urun> repository)
        {
            _repository = repository;
        }

        public IList<Urun> GetAll()
        {
            return _repository.Table.ToList();
        }

        public void ResimEkle(int urunId, Resim resim)
        {
            throw new System.NotImplementedException();
        }
    }
}
