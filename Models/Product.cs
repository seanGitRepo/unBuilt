using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace unBuiltApi.Models;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(GraphicCard), typeof(Motherboard), typeof(RAM),typeof(Storage),typeof(CPUCooler),typeof(Case),typeof(Cables), typeof(PSU))]
public class Product{
    [BsonId]    
    public ObjectId Id {get; set;}
    public string productName {get;set;}
    public double price {get; set;}
    public string description {get; set;}
    [BsonRepresentation(BsonType.String)]
    public Wear wear {get;set;}
    public bool tested{get;set;}
    public decimal? maximumWattageOutput {get;set;}
}

public class GraphicCard : Product{
    public string brand {get;set;}
    public string VRAM {get;set;}
    [BsonRepresentation(BsonType.String)]
    public Cooling cooling {get;set;}
}

public class Motherboard : Product{
    [BsonRepresentation(BsonType.String)]
    public Socket socket{get;set;}
    public bool ioShield {get;set;}
    [BsonRepresentation(BsonType.String)]
    public DDR ddr{get;set;}

}

public class RAM: Product{

    public int size{get;set;} 
    [BsonRepresentation(BsonType.String)]
    public DDR ddr {get;set;}

}


public class Storage: Product{

    public string type {get;set;}
    public int capacity {get;set;}
}


public class CPUCooler: Product{

[BsonRepresentation(BsonType.String)]
    public Cooling cooling {get;set;}
    [BsonRepresentation(BsonType.String)]
    public Socket socket {get;set;}
}

public class Case: Product{
    public Socket socket {get;set;}
}

public class Cables: Product{
    public decimal? length {get;set;}
}

public class PSU: Product{
    public decimal? length {get;set;}
    public bool powerCable {get;set;}
    public int watts {get;set;}
}
public enum DDR{
    DDR,
    DDR2,
    DDR3,
    DDR4,
    DDR5
}


//for the compabitality linked it to the motherboard type but with gpu's this is not really a problem 

public enum Cooling{

    fan,
    heatsync,
    waterCooled,
}

public enum Socket{
//AMD
AM3, FM2, AM4, TR4, sTRX4, WRX80, AM5,
//intel
LGA1150, LGA1151, LGA1200, LGA1700, LGA2011, LGA2066, LGA4189, LGA4677, LGA1851,

}

public enum Wear{

    battleScared,
    fieldTested,
    minimalWear,
    factoryNew
}