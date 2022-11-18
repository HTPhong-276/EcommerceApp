using Domain.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DataSeeding
    {
        public static async Task seedData(AppDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Brands.Any())
                {
                    List<Brand> list = new List<Brand>
                    {
                        new Brand{ BrandId=1, Name="Brand1"},
                        new Brand{ BrandId=2, Name="Brand2"}
                    };

                    context.Brands.AddRange(list);
                    await context.SaveChangesAsync();
                }

                if (!context.Types.Any())
                {
                    List<ProductType> list = new List<ProductType>
                    {
                        new ProductType{ ProductTypeId=1, Name="Type1"},
                        new ProductType{ ProductTypeId=2, Name="Type2"}
                    };

                    context.Types.AddRange(list);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    List<Product> list = new List<Product>
                    {
                        new Product{ ProductId=1, Name="Product1", Description="Des1", PictureUrl="url1", Price=1000000, BrandId=1, ProductTypeId=2, Brand = new Brand(), ProductType = new ProductType()},
                        new Product{ ProductId=2, Name="Product2", Description="Des2", PictureUrl="url2", Price=2000000, BrandId=2, ProductTypeId=1, Brand = new Brand(), ProductType = new ProductType()}
                    };

                    context.Products.AddRange(list);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<DataSeeding>();
                logger.LogError(e.Message);
            }
        } 
    }
}
