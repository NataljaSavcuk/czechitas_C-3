
namespace ToDoList.Domain.DTO;

using ToDoList.Domain.Models;
using ToDoList.Domain.Types;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted, string? Category, Priority TaskPriority)
{
    public ToDoItem ToDomain()
    {
        var newToDoItem = new ToDoItem
        {
            Name = Name,
            Description = Description,
            IsCompleted = IsCompleted,
            Category = Category,
            TaskPriority = TaskPriority
        };
        return newToDoItem;
    }
}
