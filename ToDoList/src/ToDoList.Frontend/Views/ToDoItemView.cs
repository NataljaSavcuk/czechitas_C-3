namespace ToDoList.Frontend.Views;

using ToDoList.Domain.Types;
using System.ComponentModel.DataAnnotations;

public class ToDoItemView
{
    public int ToDoItemId { get; set; }

    [Required(ErrorMessage = "Name is mandatory.")]
    [Length(3, 50)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is mandatory.")]
    [StringLength(250)]
    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public string Category { get; set; }

    public Priority TaskPriority { get; set; }
}
