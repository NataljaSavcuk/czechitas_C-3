
namespace ToDoList.Domain.DTO;

using ToDoList.Domain.Models;

public record ToDoItemGetResponseDto(string Name, string Description, bool IsCompleted)
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) => new(item.Name, item.Description, item.IsCompleted);

}
