using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApp.Models
{
   
    public class Todo
    {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id {get; set;} = string.Empty;
    public string Title {get; set;} = string.Empty;
    public string? Description {get; set;}
    public DateTime DueDate {get; set;} = DateTime.UtcNow;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;
    public bool IsCompleted {get; set;} = false;
    public bool IsDeleted {get; set;} = false;

}
}