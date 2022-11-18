using Domain.Entity;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            return await context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<Brand>> GetAllBrandAsync()
        {
            return await context.Brands
                .ToListAsync();
        }

        public async Task<IReadOnlyCollection<ProductType>> GetAllTypeAsync()
        {
            return await context.Types
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(c => c.ProductId == id);
        }
    }
}
