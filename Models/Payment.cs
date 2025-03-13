
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace unBuiltApi.Models;

public class Payment{
    [BsonId]
    public ObjectId Id {get; set;}
    public bool approved {get;set;} = false;
}

