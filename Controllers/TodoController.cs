using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {


        private readonly ILogger<TodoController> _logger;
        private TodoService Service {get;set;}

        public TodoController(ILogger<TodoController> logger, TodoService todoService)
        {
            _logger = logger;
            Service = todoService;
        }

        [HttpGet]
        public string Get()
        {
            return "Number of items " + Service.GetNumberOfTodos();
        }
    }
}
