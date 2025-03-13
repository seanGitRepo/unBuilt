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
public class UserController : ControllerBase
{
    private readonly IMongoCollection<User> _Users;
    public UserController(contextDB context)
    {
        _Users = context.Database?.GetCollection<User>("user");
    }

   [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _Users.Find(_ => true).ToListAsync();
            return Ok(products);
        }

       [HttpGet("{id}")]
    public async Task<ActionResult<User?>> GetbyId(ObjectId id)
    {
        var filter = Builders<User>.Filter.Eq(x => x.id, id);
        var user = _Users.Find(filter).FirstOrDefault();
        return user is not null ? Ok(user) : NotFound();
    }
     

     [HttpPost]

     public async Task<ActionResult> Create(User user){
        await _Users.InsertOneAsync(user);
        return CreatedAtAction(nameof(GetbyId), new {id = user.id}, user);

    }

//     [HttpPost("Admin")]

//         public async Task<ActionResult> TestCreateAdmin(Admin gpu){

            
//         await _Users.InsertOneAsync(gpu);
//         return CreatedAtAction(nameof(GetbyId), new {id = gpu.id}, gpu);

    
// }

   
}

}

