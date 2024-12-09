namespace ToDoList.Frontend.Views;

using ToDoList.Domain.Types;
using System.ComponentModel.DataAnnotations;

public class ToDoItemView
{
    public int ToDoItemId { get; set; }

    [Required(ErrorMessage = "Name is mandatory.")]
    [Length(3, 50)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is mandatory.")]
    [StringLength(250)]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    public Priority TaskPriority { get; set; }
}
