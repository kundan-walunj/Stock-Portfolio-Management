using MongoDB.Driver;

namespace Stock_Portfolio_Management.Data
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            var mongodbconnectionstring = _configuration.GetConnectionString("DbConnection");  //get connection string
            var mongoUrl= MongoUrl.Create(mongodbconnectionstring);    //create connection
            var mongoClient=new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);  //get db name
        }

        public IMongoDatabase? Database=> _database;    
    }
}
