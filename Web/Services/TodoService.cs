using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Todo.Models;
using Todo.Settings;

public class TodoService
{
    private MongoClient Client { get; set; }
    private IMongoDatabase Database { get; set; }
    private IMongoCollection<TodoItem> TodoCollection { get; set; }
    private const string CollectionName = "Items";

    public TodoService(IMongoDbSettings settings)
    {
        Client = new MongoClient(settings.ConnectionString);
        Database = Client.GetDatabase(settings.DatabaseName);
        TodoCollection = Database.GetCollection<TodoItem>(CollectionName);
    }

    public void DeleteDatabase()
    {
        Client.DropDatabase(Database.DatabaseNamespace.DatabaseName);
    }

    public bool DeleteById(string id)
    {
        var result = TodoCollection.DeleteOne(x => x.Id == id);
        //var result = TodoCollection.DeleteOne(Builders<TodoItem>.Filter.Where(x=>x.Id == id));
        return result.DeletedCount == 1;
    }

    public IList<TodoItem> FindByTitle(string title)
    {
        return TodoCollection.Find(x => x.Title.Contains(title)).ToList();
        //return TodoCollection.Find<TodoItem>(Builders<TodoItem>.Filter.Where(x => x.Title.Contains(title))).ToList();
    }

    public long GetNumberOfTodos()
    {
        return TodoCollection.CountDocuments(x => true);
        //return TodoCollection.CountDocuments(new FilterDefinitionBuilder<TodoItem>().Empty);
    }

    internal List<TodoItem> GetAll()
    {
        return TodoCollection.Find(x => true).ToList();
        //return TodoCollection.Find(new FilterDefinitionBuilder<TodoItem>().Empty).ToList();
    }

    public TodoItem GetById(string id)
    {
        return TodoCollection.Find(x => x.Id == id).FirstOrDefault();
        //return TodoCollection.Find<TodoItem>(Builders<TodoItem>.Filter.Eq(x => x.Id, id)).FirstOrDefault();
    }

    internal TodoItem Update(TodoItem item)
    {
        //No checks, just go with it
        if (item.Completed && item.CompletionTime == null)
        {
            item.CompletionTime = DateTime.Now;
        }
        if (!item.Completed)
        {
            item.CompletionTime = null;
        }
        TodoCollection.ReplaceOne(x => x.Id == item.Id, item);
        return item;
    }

    public void Save(TodoItem item)
    {
        TodoCollection.InsertOne(item);
    }
}