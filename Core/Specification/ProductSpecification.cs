using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams) : base(x =>
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand))
        && (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
        && (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
    {
        ApplyPaging((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);

        switch (specParams.Sort)
        {
            case "priceAsc":
                AddOrderby(p => p.Price);
                break;
            case "priceDesc":
                AddOrderbyDesc(p => p.Price);
                break;
            default:
                AddOrderby(p => p.Name);
                break;
        }
    }
}
