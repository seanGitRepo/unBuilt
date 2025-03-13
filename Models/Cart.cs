using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace unBuiltApi.Models;

public class Cart{
    [BsonId]
    public ObjectId Id {get; set;}
    // I want this list to update with current prices.
    public List<Product> Items {get; set;} = new List<Product>();
    public ObjectId customerId {get;set;}
    
}