 using Microsoft.AspNetCore.Mvc;
using unBuiltApi.Data;
using unBuiltApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver; 
using MongoDB.Driver.Linq; 
using MongoDB.Bson;

namespace unBuiltApi.Controller{

    

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMongoCollection<Product> _products;
    public ProductController(contextDB context)
    {
        _products = context.Database?.GetCollection<Product>("product");
    }

   [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _products.Find(_ => true).ToListAsync();
            return Ok(products);
        }

       [HttpGet("{id}")]
    public async Task<ActionResult<Product?>> GetbyId(ObjectId id)
    {
        var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
        var product = _products.Find(filter).FirstOrDefault();
        return product is not null ? Ok(product) : NotFound();
    }
     

     [HttpPost]

     public async Task<ActionResult> Create(Product product){
        await _products.InsertOneAsync(product);
        return CreatedAtAction(nameof(GetbyId), new {id = product.Id}, product);

    }

    [HttpPost("gpu")]

        public async Task<ActionResult> TestCreateGPU(GraphicCard gpu){
        await _products.InsertOneAsync(gpu);
        return CreatedAtAction(nameof(GetbyId), new {id = gpu.Id}, gpu);

    
}

   
}

}

