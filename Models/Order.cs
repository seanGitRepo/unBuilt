using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace unBuiltApi.Models;

public class Order{
    [BsonId]
    public ObjectId id{get; set;}
   
    public List<orderProduct> Items {get; set;} = new List<orderProduct>();

    [BsonRepresentation(BsonType.String)]
    public orderStatus status {get;set;}
    //shipped address needs to come from the customer card or from manually entering it 
    public Address shippedAddress {get;set;}
    //requries all informatoin relating to the customer
    public ObjectId customerId {get;set;}
    public Payment payment {get;set;}
    //only needs to be referenced.  
    public ObjectId? salesPersonId {get; set;}
}

public enum orderStatus{
    processeed,
    picked,
    shipped,
    delviered,
    undelivered,
    cancelled,
    completed,
    refunded,
    lost
}


public class orderProduct{
  [BsonId]    
    public ObjectId Id {get; set;}
    public string productName {get;set;}
    public double price {get; set;}
    public string description {get; set;}
    public bool tested{get;set;}
    public decimal? maximumWattageOutput {get;set;}
    public int quantity {get;set;}
    public double TotalPrice => quantity * price; 
}