
namespace ToDoList.Domain.DTO;

using ToDoList.Domain.Models;

public record ToDoItemCreateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain()
    {
        var newToDoItem = new ToDoItem
        {
            Name = Name,
            Description = Description,
            IsCompleted = IsCompleted
        };
        return newToDoItem;
    }
}
