
export class TodoApp {
  constructor() {
    this.loadTodos();
  }

  addTodo(e) {
    let isEnter = e.keyCode == 13;
    if(!isEnter){
      return true;
    }
    var newTodo = {title: this.newTodoTitle};
    fetch("/todo",{
      method:"POST",
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(newTodo)
    }).then(x=>{
      this.newTodoTitle = "";
      this.loadTodos();
    });
    return false;
  }

  loadTodos(){
    fetch("/todo").then(x => {
      return x.json();
    }).then(x=> {
      this.items = x.items;
    });
  }
}
