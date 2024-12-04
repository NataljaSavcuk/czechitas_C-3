
namespace ToDoList.Domain.DTO;

using ToDoList.Domain.Models;
using ToDoList.Domain.Types;

public record ToDoItemGetResponseDto(int ToDoItemId, string Name, string Description, bool IsCompleted, string? Category, Priority TaskPriority)
{
    public static ToDoItemGetResponseDto FromDomain(ToDoItem item) =>
    new(item.ToDoItemId, item.Name, item.Description, item.IsCompleted, item?.Category, item.TaskPriority);

}
