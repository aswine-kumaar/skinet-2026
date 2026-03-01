using System;
using Core.Entities;

namespace Core.Specification;

public class ProductSpecification : BaseSpecification<Product>
{
public ProductSpecification(string? brand, string? type, string? sort) : base(x =>
    (string.IsNullOrEmpty(brand) || x.Brand == brand) &&
    (string.IsNullOrEmpty(type) || x.Type == type))
{
    switch (sort)
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
