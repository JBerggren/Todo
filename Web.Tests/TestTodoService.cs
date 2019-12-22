using MongoDB.Driver;
using System;
using Todo.Settings;
using Xunit;

namespace Web.Tests
{
    public class TestTodoService : IDisposable
    {
        TodoService Service;
        const string ConnectionString = "mongodb://todoadmin:getthingsdone@localhost:27017";
        const string Database = "Testing";
        MongoClient client; 
        public TestTodoService()
        {
            client  = new MongoClient(ConnectionString);
            Service = new TodoService(new MongoDbSettings() { ConnectionString = ConnectionString, DatabaseName = Database });
        }

        [Fact]
        public void CreatedTodoGetsId()
        {
            var todoItem = new Todo.Models.TodoItem("Testing");
            Service.Save(todoItem);
            Assert.False(string.IsNullOrWhiteSpace(todoItem.Id));
        }

        [Fact]
        public void TodosAreActuallyStored()
        {
            CreatedTodoGetsId();
            Assert.NotEqual(0, Service.GetNumberOfTodos());
        }

        [Fact]
        public void CanRetrieveTodoById()
        {
            var title = "TestID";
            var todoItem = new Todo.Models.TodoItem(title);
            Service.Save(todoItem);
            var retrievedTodo = Service.GetById(todoItem.Id);
            Assert.NotNull(retrievedTodo);
            Assert.Equal(retrievedTodo.Title, todoItem.Title);
        }

        [Fact]
        public void CanSearchForTitle()
        {
            client.DropDatabase(Database);
           
            var prefix = "TestTitle";
            var todoItem = new Todo.Models.TodoItem(prefix + "1");
            Service.Save(todoItem);
            todoItem = new Todo.Models.TodoItem(prefix + "2");
            Service.Save(todoItem);
            todoItem = new Todo.Models.TodoItem("AAA");
            Service.Save(todoItem);

            var items = Service.FindByTitle(prefix);
            Assert.Equal(2, items.Count);
        }



        public void Dispose()
        {
            client.DropDatabase(Database);
            return;
        }
    }
}
