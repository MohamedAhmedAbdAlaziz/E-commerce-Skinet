using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Core;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using API.Errors;
using Core.Specification;
using API.Helpers;

namespace API.Controllers
{ 
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(
          IGenericRepository<Product> productsRepo ,
          IGenericRepository<ProductBrand> ProductBrandRepo ,
          IGenericRepository<ProductType> ProductTypeRepo ,
          IMapper mapper
          
          )
        {
            _productsRepo = productsRepo;
            _productBrandRepo = ProductBrandRepo;
            _productTypeRepo = ProductTypeRepo;
            _mapper = mapper;
        }

        [HttpGet]
          public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
         [FromQuery] ProductSpecParams productParams
            )
            {
          var spec= new ProductsWithTypesAndBrandsSpecification(productParams);
         var countSpec= new ProductWithFiltersFoCountSpecification(productParams);

         var totalItems= await _productsRepo.CountAsync(countSpec);
          var products= await _productsRepo.ListAsync(spec);
     
         //  var products= await _productsRepo.ListAllAsync();
         var data= _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);



  return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize
  ,totalItems,data));
           }
      
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
            public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id){
          var spec= new ProductsWithTypesAndBrandsSpecification(id);

            var product= await _productsRepo.GetEntiyWithSpec(spec);
           // var product= await _productsRepo.GetbyIdAsync(id);
          // return product;

   
     if(product == null) return NotFound(new ApiResponse(404));
         return  _mapper.Map<Product,ProductToReturnDto>(product);
            }

          [HttpGet("brands")]
          public async Task<ActionResult<List<ProductBrand>>> GetProductBrands(){
           var productBrands= await  _productBrandRepo.ListAllAsync();
            return Ok(productBrands);
           }


         [HttpGet("types")]
          public async Task<ActionResult<List<ProductType>>> GetProductTypes(){
           var productTypes= await _productTypeRepo.ListAllAsync();
            return Ok(productTypes);
           }

    }
}
