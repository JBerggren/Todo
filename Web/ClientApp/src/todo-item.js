import { bindable, inject } from 'aurelia';
import {TodoService} from 'todo-service';

@inject(TodoService)
export class TodoItem {
  @bindable item;

  constructor(todoService) {
    this.service = todoService;
    this.completionTime = null;
  }

  afterBind(){
    if(this.item.completionTime){ 
      var d = new Date(this.item.completionTime);
      this.completionTime =  `${d.getDate()}/${d.getMonth()} ${d.getHours()}:${d.getMinutes()}`;
    }
  }

  changeCompleted(){
    this.service.updateItem(this.item);
  }
}
