using MongoDB.Driver;
using System;
using Todo.Models;
using Todo.Settings;

public class TodoService{
    private MongoClient Client {get;set;}
    private IMongoDatabase Database {get;set;}
    private const string CollectionName = "Items";

    public TodoService(IMongoDbSettings settings){
        Client = new MongoClient(settings.ConnectionString);
        Database =  Client.GetDatabase(settings.DatabaseName);
    }

    public long GetNumberOfTodos(){
        return Database.GetCollection<TodoItem>(CollectionName).CountDocuments(new FilterDefinitionBuilder<TodoItem>().Empty);
    }

    public void Save(TodoItem item)
    {
        Database.GetCollection<TodoItem>(CollectionName).InsertOne(item);
    }

    public TodoItem GetById(string id)
    {
        return Database.GetCollection<TodoItem>(CollectionName).Find<TodoItem>(Builders<TodoItem>.Filter.Eq(x => x.Id, id)).FirstOrDefault();
    }
}