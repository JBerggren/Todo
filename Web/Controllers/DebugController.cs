using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebugController : ControllerBase
    {
        private TodoService Service { get; set; }
        public DebugController(TodoService service)
        {
            Service = service;
        }

        [HttpGet("dropdb")]
        public string EmptyDatabase()
        {
            Service.DeleteDatabase();
            return "ok";
        }
    }
}