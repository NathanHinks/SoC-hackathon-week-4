using System.Collections.Generic;
using Dapper;
using System;
public class ToDoItemRepository : BaseRepository
{
    public IEnumerable<ToDoItem> GetAll()
    {
        using var connection = CreateConnection();
        return connection.Query<ToDoItem>("SELECT * FROM ToDoItems ORDER BY Id;");
    }

    public void Delete(long id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM ToDoItems WHERE Id = @Id", new { Id = id });
    }

    public ToDoItem GetOne(long id)
    {
        using var connection = CreateConnection();
        return connection.QuerySingle<ToDoItem>("SELECT * FROM ToDoItems WHERE Id=@Id", new { Id = id });
    }

    public ToDoItem Update(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        return connection.QuerySingle<ToDoItem>("UPDATE ToDoItems SET title = @Title, priority = @Priority, isComplete = @IsComplete WHERE Id=@Id; SELECT * FROM ToDoItems WHERE Id= @Id", toDoItem);
    }

    public ToDoItem Insert(ToDoItem toDoItem)
    {
        using var connection = CreateConnection();
        return connection.QuerySingle<ToDoItem>("INSERT INTO ToDoItems (title, priority, isComplete) VALUES (@Title, @Priority, @IsComplete); SELECT * FROM ToDoItems WHERE title= @Title", toDoItem);
    }

}