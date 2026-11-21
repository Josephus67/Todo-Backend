namespace TodoApp.Models
{
    public string Id {get; set;} = string.Empty;
    public string Ttitle {get; set;} = string.Empty;
    public string? Description {get; set;}
    public DateTime DueDate {get; set;} = DateTime.UtcNow;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public Datetime UpdatedAt {get; set;} = DateTime.UtcNow;
    public bool IsCompleted {get; set;} = false;
    public bool IsDeleted {get; set;} = false;

}