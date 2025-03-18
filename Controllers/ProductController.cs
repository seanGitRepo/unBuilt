 using Microsoft.AspNetCore.Mvc;
using unBuiltApi.Data;
using unBuiltApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver; 
using MongoDB.Driver.Linq; 
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

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
//..api/product
   [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _products.Find(_ => true).ToListAsync();
            //TODO: add a way for all products to be called at once.
            return Ok(products);
        }


// ..api/product/{id}
       [HttpGet("{id}")]
    public async Task<ActionResult<Product?>> GetbyId(ObjectId id)
    {
        var filter = Builders<Product?>.Filter.Eq(x => x.Id, id);
        var product = _products.Find(filter).FirstOrDefault();
        return product is not null ? Ok(product) : NotFound();
    }
     
// .. api/product/create/{type} (json file required in body of request)
     [HttpPost ("create/{type}")]
    public async Task<ActionResult> Create(string type)
{
       try
        {
           
            using var reader = new StreamReader(Request.Body);
            var json = await reader.ReadToEndAsync();

            //correclty identifies the model type that is being called. from the url
            var productType = Type.GetType($"unBuiltApi.Models.{type}", false, true);
            //fails heres if the type does not exist, or is unable to idenitf
            if (productType == null || !typeof(Product).IsAssignableFrom(productType))
            {
                return BadRequest("Invalid product type.");
            }
         
            //id is not to be inlcuded in the json file, even as a null entry.
            //json file needs to be capitalised
            //essentially the ui will create the json speicifc to what is required on here.
            //in the sense of automation an inherited class from Product is to be created, then the datbase should be able to understand whatever message is sent in the url.
            var product = (Product)BsonSerializer.Deserialize(json, productType);
            
            await _products.InsertOneAsync(product);

            return CreatedAtAction(nameof(GetbyId), new { id = product.Id }, product);
        }
        catch (Exception ex)
        {
            //if something is not correct in the json file that is sent.
            return BadRequest($"Error, ceate failed,: {ex.Message}");
        }
    }
//   [HttpPut ("edit/{id}")]
//     public async Task<ActionResult> EditProducts( ObjectId id)
// {
//        try
//         {

//             foreach (var item in await _products.Find(_ => true).ToListAsync())
//             {
//                 Console.WriteLine(item.Id);


//             }

//            using var reader = new StreamReader(Request.Body);
//            var json = await reader.ReadToEndAsync();

//             //correclty identifies the model type that is being called. from the url
//             var productType = Type.GetType($"unBuiltApi.Models.{type}", false, true);
//             //fails heres if the type does not exist, or is unable to idenitf
//             if (productType == null || !typeof(Product).IsAssignableFrom(productType))
//             {
//                 return BadRequest("Invalid product type.");
//             }
         
//             //id is not to be inlcuded in the json file, even as a null entry.
//             //json file needs to be capitalised
//             //essentially the ui will create the json speicifc to what is required on here.
//             //in the sense of automation an inherited class from Product is to be created, then the datbase should be able to understand whatever message is sent in the url.
//             var product = (Product)BsonSerializer.Deserialize(json, productType);
            
//             await _products.InsertOneAsync(product);
           
//             return CreatedAtAction(nameof(GetbyId), new { id = product.Id }, product);
//         }
//         catch (Exception ex)
//         {
//             //if something is not correct in the json file that is sent.
//             return BadRequest($"Error, ceate failed,: {ex.Message}");
//         }

//     }

    
}

}

