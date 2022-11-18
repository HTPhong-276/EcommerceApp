using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IReadOnlyCollection<Product>> GetAllAsync();
        Task<IReadOnlyCollection<Brand>> GetAllBrandAsync();
        Task<IReadOnlyCollection<ProductType>> GetAllTypeAsync();
    }
}
