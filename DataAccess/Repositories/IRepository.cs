namespace DataAccess.Repositories;

public interface IRepository<T>
{
    Task<List<T>> Get();
    Task<T> GetById(Guid id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task DeleteById(Guid id);
}