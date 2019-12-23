export class TodoService {
    constructor() {
        this.baseUrl = "http://localhost:5000";
    }

    createNew(newTodo) {
        return fetch(this.baseUrl + "/todo", {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newTodo)
        });
    }

    getItems() {
        return fetch(this.baseUrl + "/todo").then(x => {
            return x.json();
        });
    }

    updateItem(item){
        return fetch(this.baseUrl + "/todo/" + item.id, {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        }).then(x=> x.json()).then(updatedItem=>{
            item.completed = updatedItem.completed;
            item.completionTime = updatedItem.completionTime;
        });
    }
}