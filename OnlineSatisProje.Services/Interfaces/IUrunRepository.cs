using System.Collections.Generic;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface IUrunRepository
    {
        IEnumerable<Urun> GetAllProducts();
        IEnumerable<Urun> GetPublishedProducts();
        IList<Urun> GetDeletedProducts();
        IList<Urun> GetShowOnHomePageProducts();
        IList<Urun> GetPublishedAndHomePageProducts();
        IList<Urun> GetHomePageProducts();
        IList<Urun> GetAllProductsWithDiscount();
        IList<Urun> GetAvailableProductsWithDiscount();
        Urun GetDiscountPrice(int id);
        bool IsAvailable(int? id);
        bool IsAvailable(Urun urun);
    }
}
