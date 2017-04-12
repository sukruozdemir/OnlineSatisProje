using System.Collections.Generic;
using System.Linq;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class UrunRepository : IUrunRepository
    {
        private readonly IRepository<Urun> _urunRepository;

        public UrunRepository(IRepository<Urun> urunRepository)
        {
            _urunRepository = urunRepository;
        }

        public IEnumerable<Urun> GetAllProducts() => _urunRepository.Table.ToList();

        public IEnumerable<Urun> GetPublishedProducts() => GetAllProducts().Where(u => u.Yayinlandi).ToList();

        public IList<Urun> GetDeletedProducts() => GetAllProducts().Where(u => u.Silindi).ToList();

        public IList<Urun> GetShowOnHomePageProducts() => GetAllProducts().Where(u => u.AnasayfadaGoster).ToList();

        public IList<Urun> GetPublishedAndHomePageProducts() => GetPublishedProducts()
            .Where(u => u.AnasayfadaGoster)
            .ToList();

        public IList<Urun> GetHomePageProducts() => GetAllProducts()
            .Where(u => u.AnasayfadaGoster && !u.Silindi && u.Yayinlandi && u.Aktif)
            .ToList();

        public bool IsAvailable(int? id)
        {
            if (id == null) return false;
            var urun = _urunRepository.GetById(id);
            return urun.Aktif && !urun.Silindi && urun.Yayinlandi;
        }

        public bool IsAvailable(Urun urun) => IsAvailable(urun.Id);
    }
}