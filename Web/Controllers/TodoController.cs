﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Models;

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
        public TodoListResponse Get()
        {
            return new TodoListResponse { Items = Service.GetAll()};
        }

        [HttpPost]
        public TodoItem Create(TodoCreateRequest param){
            if(string.IsNullOrWhiteSpace(param.Title)){
                throw new ArgumentException("Title empty");
            }
            var item = new TodoItem(param.Title);
            Service.Save(item);
            return item;
        }
    }

    public class TodoCreateRequest{
        public string Title{get;set;}
    }

    public class TodoListResponse{
        public IList<TodoItem> Items{get;set;}
    }
}
