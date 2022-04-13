// See https://aka.ms/new-console-template for more information
using System.Net.Http.Json;
using MongoDB.Driver;

var apiClient = new HttpClient();
var apiEndpoint = Environment.GetEnvironmentVariable("API_ENDPOINT");
var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGO"));
var collection = mongoClient.GetDatabase("demo").GetCollection<Payment>("payments");
while(true)
{
    try{
        var payment = await apiClient.GetFromJsonAsync<Payment>(apiEndpoint);
        await collection.InsertOneAsync(payment);
    }
    catch(Exception ex){
        Console.WriteLine(ex.Message);
    }
    Thread.Sleep(5000);
}

internal class Payment
{
    public int ClientId { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; }
}