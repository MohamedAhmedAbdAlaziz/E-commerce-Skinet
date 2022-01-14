using Core.Entites;
using Core.Specification;

namespace Core.Specifications
{
    public class ProductWithFiltersFoCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersFoCountSpecification(ProductSpecParams productParams)
        :base(x=>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
        (productParams.Search))&&
         ( !productParams.BrandId.HasValue || x.ProductBrandId==productParams.BrandId)  &&
         ( !productParams.TypeId.HasValue || x.ProductTypeId==productParams.TypeId) 
         )
        {
        }
    }
}