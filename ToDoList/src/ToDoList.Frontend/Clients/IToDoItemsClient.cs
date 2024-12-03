namespace ToDoList.Frontend.Clients;


using ToDoList.Frontend.Views;

public interface IToDoItemsClient
{
    public Task<List<ToDoItemView>> ReadItemsAsync();
    public Task CreateItemAsync(ToDoItemView itemView);
    public Task<ToDoItemView?> ReadItemByIdAsync(int itemId);
    public Task DeleteItemAsync(ToDoItemView itemView);
    public Task UpdateItemAsync(ToDoItemView itemView);
}


