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
        private readonly IRepository<UrunIndirimMapping> _urunIndirimRepository;
        private readonly IRepository<Urun> _urunRepository;

        public UrunRepository(IRepository<Urun> urunRepository, IRepository<UrunIndirimMapping> urunIndirimRepository)
        {
            _urunRepository = urunRepository;
            _urunIndirimRepository = urunIndirimRepository;
        }

        public IEnumerable<Urun> GetAllProducts()
        {
            return _urunRepository.Table.ToList();
        }

        public IEnumerable<Urun> GetPublishedProducts()
        {
            return GetAllProductsWithDiscount().Where(u => u.Yayinlandi).ToList();
        }

        public IList<Urun> GetDeletedProducts()
        {
            return GetAllProductsWithDiscount().Where(u => u.Silindi).ToList();
        }

        public IList<Urun> GetShowOnHomePageProducts()
        {
            return GetAllProductsWithDiscount().Where(u => u.AnasayfadaGoster).ToList();
        }

        public IList<Urun> GetPublishedAndHomePageProducts()
        {
            return GetAllProductsWithDiscount()
                .Where(u => u.AnasayfadaGoster)
                .ToList();
        }

        public IList<Urun> GetHomePageProducts()
        {
            return GetAllProductsWithDiscount()
                .Where(u => u.AnasayfadaGoster && !u.Silindi && u.Yayinlandi && u.Aktif)
                .ToList();
        }

        public IList<Urun> GetAllProductsWithDiscount()
        {
            var liste = new List<Urun>();

            foreach (var item in GetAllProducts())
                liste.Add(GetDiscountPrice(item.Id));

            return liste;
        }

        public IList<Urun> GetUrunsByCategoryId(int kategoriId)
        {
            var liste = new List<Urun>();
            foreach (var urun in GetAllProductsWithDiscount())
                if (urun.UrunKategoriMapping.Any(k => k.KategoriId == kategoriId))
                    liste.Add(urun);
            return liste;
        }

        public bool IsAvailable(int? id)
        {
            if (id == null) return false;
            var urun = _urunRepository.GetById(id);
            return urun.Aktif && !urun.Silindi && urun.Yayinlandi;
        }

        public bool IsAvailable(Urun urun)
        {
            return IsAvailable(urun.Id);
        }

        public Urun GetDiscountPrice(int id)
        {
            var urun = _urunRepository.GetById(id);
            var indirim = _urunIndirimRepository.Table.OrderByDescending(k => k.Indirim.BaslangicTarihi)
                .FirstOrDefault(k => k.UrunId == urun.Id);

            if (indirim != null)
                if (indirim.Indirim.BitisTarihi > Convert.ToDateTime(DateTime.Now.ToString("d")) &&
                    indirim.Indirim.Aktif)
                    if (indirim.Indirim.YuzdeKullan)
                        urun.Fiyat -= urun.Fiyat / 100 * indirim.Indirim.IndirimYuzdesi;
                    else
                        urun.Fiyat -= indirim.Indirim.IndirimMiktari;
            return urun;
        }

        public IList<Urun> GetAvailableProductsWithDiscount()
        {
            return GetAllProductsWithDiscount().Where(u => u.Aktif && !u.Silindi && u.Yayinlandi).ToList();
        }
    }
}