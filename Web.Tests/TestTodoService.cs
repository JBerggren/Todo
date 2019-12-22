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
        public TestTodoService()
        {
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

        public void Dispose()
        {
            var client = new MongoDB.Driver.MongoClient(ConnectionString);
            client.DropDatabase(Database);
            return;
        }
    }
}
