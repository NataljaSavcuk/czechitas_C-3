namespace ToDoList.Persistence.Repository;

public interface IRepository<T> where T : class
{
    void Create(T item);

    IEnumerable<T> Read();

    T ReadById(int id);

    void Update(T item);

    void DeleteById(int id);

}
