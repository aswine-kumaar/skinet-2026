using System.Collections.Generic;
using System.Threading.Tasks;
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ProductsController(IGenericRepository<Product> repo) : BaseAPIController
    {

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(
            [FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            return await CreatePagedResult(spec, repo, specParams.PageIndex, specParams.PageSize);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if (await repo.SaveChangesAsync())
            {
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            return BadRequest("Failed to create product");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot Update this product");
            repo.Update(product);
            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Failed to update product");
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            repo.Delete(product);
            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Failed to delete product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            var brands = await repo.ListAsync(spec);
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            var types = await repo.ListAsync(spec);
            return Ok(types);
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
