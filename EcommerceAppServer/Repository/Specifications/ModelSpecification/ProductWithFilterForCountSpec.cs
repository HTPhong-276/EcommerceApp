using Domain.Entity;
using Repository.Specifications.SpecificationParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Specifications.ModelSpecification
{
    public class ProductWithFilterForCountSpec : Specification<Product>
    {
        public ProductWithFilterForCountSpec(ProductSpecParams productParams)
            : base(x =>
               (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
               (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )

        {
        }
    }
}
