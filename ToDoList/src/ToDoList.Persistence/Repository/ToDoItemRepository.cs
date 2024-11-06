namespace ToDoList.Persistence.Repository;

using System;
using ToDoList.Persistence.Repository;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTO;

public class ToDoItemRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;
    public ToDoItemRepository(ToDoItemsContext context)
    {
        this.context = context;
    }
    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }
    public IEnumerable<ToDoItem> Read() => context.ToDoItems.ToList();

    public ToDoItem ReadById(int id) => context.ToDoItems.Find(id);

    public void UpdateById(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var itemToDelete = context.ToDoItems.Find(id);
        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
    }
}
