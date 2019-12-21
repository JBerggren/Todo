using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {


        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var mongo = new MongoClient("mongodb://todoadmin:getthingsdone@mongo:27017");
            var db = mongo.GetDatabase("TodoDb");
            var items = db.GetCollection<TodoItem>("items");
            items.InsertOne(new TodoItem(){Title = DateTime.Now.ToString("mm:ss")});
            return "Number of items " + items.CountDocuments(new FilterDefinitionBuilder<TodoItem>().Empty);
        }
    }

    public class TodoItem{
        public string Title{get;set;}
        public bool Done {get;set;}
    }
}
