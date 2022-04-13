using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMongoCollection<Payment> _mongo = new MongoClient(Environment.GetEnvironmentVariable("MONGO")).GetDatabase("demo").GetCollection<Payment>("payments");

        [Route("{clientId}")]
        [HttpGet]
        public IEnumerable<Payment> GetPayments(int clientId)
        {
            return _mongo.AsQueryable().Where(x => x.ClientId == clientId);
        }
    }

    public class Payment
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int ClientId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}