using MongoDB.Bson.Serialization.Attributes;

namespace Todo.Models
{
    public class TodoItem
    {
        public TodoItem(){
        }
        public TodoItem(string title){
            Title = title;
        }
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public bool Done { get; set; }
    }
}