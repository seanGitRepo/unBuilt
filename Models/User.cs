using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace unBuiltApi.Models;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(Admin), typeof(Guest), typeof(AccountUser),typeof(Salesperson))]
public abstract class User{
    [BsonId]
    public ObjectId id {get; set;}
    public string nameFirst{get; set;}
    public string nameLast{get;set;} 

    public string password {get; set;}
    public string email {get;set;}

    
}

[BsonDiscriminator("Admin")]
public class Admin: User{
}

[BsonDiscriminator("Guest")]
public class Guest: User{
}

[BsonDiscriminator("AccountUser")]
public class AccountUser : User{
    [BsonRepresentation(BsonType.Document)]
    public Address postalAddress {get;set;}= new Address();
    //requries list of orders
}

[BsonDiscriminator("Salesperson")]
public class Salesperson : User{
    
    //requries list of orders
}


public class Address{

[BsonId]
    public ObjectId id{get;set;}
    public string? unitNumber{get;set;}
    public string streetNumber {get; set;}
    public string streetName {get; set;}
    public string suburbName{get;set;}
    public int postcode{get;set;}
    
}