using MongoDB.Driver;

namespace unBuiltApi.Data;

public class contextDB{
private readonly IConfiguration _configuration; // for configuration so we can assaing it from the constructuer
private readonly IMongoDatabase? _database; //

public contextDB(IConfiguration configuration)
{
_configuration = configuration;

var connecitonstring = _configuration.GetConnectionString("DbConnection");
var databaseName = _configuration.GetConnectionString("DatabaseName");
var mongoUrl = MongoUrl.Create(connecitonstring);
var mongoClient = new MongoClient(mongoUrl);
_database = mongoClient.GetDatabase(databaseName);
}

public IMongoDatabase? Database => _database;
}