using System;
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
        private readonly IRepository<UrunIndirimMapping> _urunIndirimRepository;

        public UrunRepository(IRepository<Urun> urunRepository, IRepository<UrunIndirimMapping> urunIndirimRepository)
        {
            _urunRepository = urunRepository;
            _urunIndirimRepository = urunIndirimRepository;
        }

        public IEnumerable<Urun> GetAllProducts() => _urunRepository.Table.ToList();

        public IEnumerable<Urun> GetPublishedProducts() => GetAllProductsWithDiscount().Where(u => u.Yayinlandi).ToList();

        public IList<Urun> GetDeletedProducts() => GetAllProductsWithDiscount().Where(u => u.Silindi).ToList();

        public IList<Urun> GetShowOnHomePageProducts() => GetAllProductsWithDiscount().Where(u => u.AnasayfadaGoster).ToList();

        public IList<Urun> GetPublishedAndHomePageProducts() => GetAllProductsWithDiscount()
            .Where(u => u.AnasayfadaGoster)
            .ToList();

        public IList<Urun> GetHomePageProducts() => GetAllProductsWithDiscount()
            .Where(u => u.AnasayfadaGoster && !u.Silindi && u.Yayinlandi && u.Aktif)
            .ToList();

        public IList<Urun> GetAllProductsWithDiscount()
        {
            var liste = new List<Urun>();

            foreach (var item in GetAllProducts())
            {
                liste.Add(GetDiscountPrice(item.Id));
            }

            return liste;
        }

        public bool IsAvailable(int? id)
        {
            if (id == null) return false;
            var urun = _urunRepository.GetById(id);
            return urun.Aktif && !urun.Silindi && urun.Yayinlandi;
        }

        public bool IsAvailable(Urun urun) => IsAvailable(urun.Id);

        public Urun GetDiscountPrice(int id)
        {
            var urun = _urunRepository.GetById(id);
            var indirim = _urunIndirimRepository.Table.OrderByDescending(k => k.Indirim.BaslangicTarihi).FirstOrDefault(k => k.UrunId == urun.Id);

            if (indirim != null)
            {
                if (indirim.Indirim.BitisTarihi > Convert.ToDateTime(DateTime.Now.ToString("d")) && indirim.Indirim.Aktif)
                {
                    if (indirim.Indirim.YuzdeKullan)
                    {
                        urun.Fiyat -= ((urun.Fiyat / 100) * indirim.Indirim.IndirimYuzdesi);
                    }
                    else
                    {
                        urun.Fiyat -= indirim.Indirim.IndirimMiktari;
                    }
                }
            }
            return urun;
        }

        public IList<Urun> GetAvailableProductsWithDiscount() => GetAllProductsWithDiscount().Where(u => u.Aktif && !u.Silindi && u.Yayinlandi).ToList();
    }
}