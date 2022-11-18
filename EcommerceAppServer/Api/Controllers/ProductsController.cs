using Api.Dtos;
using Api.Helpers;
using AutoMapper;
using Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Repository.Specifications.ModelSpecification;
using Repository.Specifications.SpecificationParams;

namespace Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<Brand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IMapper mapper;

        public ProductsController(
            IGenericRepository<Product> productRepo,
            IGenericRepository<Brand> brandRepo,
            IGenericRepository<ProductType> typeRepo,
            IMapper mapper)
        {
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetById(int id)
        {
            var spec = new ProductWithTypeAndBrandSpec(id);
            var product = await productRepo.GetWithSpec(spec);

            return mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<Pagination<Product>>> GetAll(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductWithTypeAndBrandSpec(productParams);
            var countSpec = new ProductWithFilterForCountSpec(productParams);

            var list = await productRepo.ListAsync(spec);
            var totalItem = await productRepo.CountAsync(countSpec);

            var data = mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(list);

            return Ok(new Pagination<ProductToReturnDto>(
                    productParams.PageIndex, productParams.PageSize, totalItem, data
                ));
        }
    }
}
