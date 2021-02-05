using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task Delete(long id);
    Task DeleteAll();
    Task<T> GetOne(long id);
    Task<T> Update(T item);
    Task<T> Insert(T item);
}