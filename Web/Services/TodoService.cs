using MongoDB.Driver;
using Todo.Models;
using Todo.Settings;

public class TodoService{
    private MongoClient Client {get;set;}
    private IMongoDatabase Database {get;set;}
    public TodoService(IMongoDbSettings settings){
        Client = new MongoClient(settings.ConnectionString);
        Database =  Client.GetDatabase(settings.DatabaseName);
    }

    public long GetNumberOfTodos(){
        return Database.GetCollection<TodoItem>("Items").CountDocuments(new FilterDefinitionBuilder<TodoItem>().Empty);
    }

    public void Save(TodoItem item)
    {
        Database.GetCollection<TodoItem>("Items").InsertOne(item);
    }
}