using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
public class ToDoItemRepository : BaseRepository, IRepository<ToDoItem>
{
    public ToDoItemRepository(IConfiguration configuration) : base(configuration) { }
    public async Task<IEnumerable<ToDoItem>> GetAll()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<ToDoItem>("SELECT * FROM ToDoItems ORDER BY CASE WHEN priority = 'high' THEN 1 WHEN priority = 'medium' THEN 2 WHEN priority = 'low' THEN 3 END;");
    }

    public async Task Delete(long id)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM ToDoItems WHERE Id = @Id", new { Id = id });
    }

    public async Task DeleteAll()
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync("DELETE FROM ToDoItems WHERE isComplete=true");
    }

    public async Task<ToDoItem> GetOne(long id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<ToDoItem>("SELECT * FROM ToDoItems WHERE Id=@Id", new { Id = id });
    }

    public async Task<ToDoItem> Update(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<ToDoItem>("UPDATE ToDoItems SET title = @Title, priority = @Priority, isComplete = @IsComplete WHERE Id=@Id; SELECT * FROM ToDoItems WHERE Id= @Id", toDoItem);
    }

    public async Task<ToDoItem> Insert(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<ToDoItem>("INSERT INTO ToDoItems (title, priority, isComplete) VALUES (@Title, @Priority, @IsComplete); SELECT * FROM ToDoItems WHERE title= @Title", toDoItem);
    }

}