
namespace ToDoList.Domain.DTO;

using ToDoList.Domain.Models;

public record ToDoItemUpdateRequestDto(string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain()
    {
        var updatedToDoItem = new ToDoItem
        {
            Name = Name,
            Description = Description,
            IsCompleted = IsCompleted
        };
        return updatedToDoItem;
    }

}
