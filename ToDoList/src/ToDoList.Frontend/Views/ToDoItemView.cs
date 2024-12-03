namespace ToDoList.Frontend.Views;

using ToDoList.Domain.Types;

public class ToDoItemView
{
    public int ToDoItemId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Category { get; set; }
    public Priority TaskPriority { get; set; }
}
