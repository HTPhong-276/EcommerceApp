using Domain.Entity;
using Repository.Specifications.SpecificationParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Specifications.ModelSpecification
{
    public class ProductWithTypeAndBrandSpec : Specification<Product>
    {
        public ProductWithTypeAndBrandSpec()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand);
        }

        public ProductWithTypeAndBrandSpec(int id) : base(x => x.ProductId == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand);
        }

        public ProductWithTypeAndBrandSpec(ProductSpecParams productParams) 
            : base (x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Brand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc": 
                        AddOrderBy(p => p.Price); 
                        break;
                    case "priceDesc": 
                        AddOrderByDescending(p => p.Price); 
                        break;
                    default: 
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
    }
}
