import { inject } from 'aurelia';
import { TodoService } from 'todo-service';
@inject(TodoService) 
export class TodoApp {
  constructor(todoService) {
    this.service = todoService;
    this.loadTodos();
  }

  addTodo(e) {
    let isEnter = e.keyCode == 13;
    if(!isEnter){
      return true;
    }

    var newTodo = {title: this.newTodoTitle};

    this.service.createNew(newTodo).then(x=>{
      this.newTodoTitle = "";
      this.loadTodos();
    });
    return false;
  }

  loadTodos(){
    this.service.getItems().then(x=> {
      this.items = x.items;
    });
  }
}
