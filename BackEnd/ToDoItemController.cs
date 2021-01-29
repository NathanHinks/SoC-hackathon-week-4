using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]s")]
public class ToDoItemController : ControllerBase
{
    private readonly ToDoItemRepository _ToDoItemRepository;

    public ToDoItemController()
    {
        _ToDoItemRepository = new ToDoItemRepository();
    }

    [HttpGet]
    public IEnumerable<ToDoItem> GetAllToDoItems(int id)
    {
        return _ToDoItemRepository.GetAll();
    }

    [HttpGet("{id}")]
    public ToDoItem GetToDoItem(int id)
    {
        return _ToDoItemRepository.GetOne(id);
    }

    [HttpPut("{id}")]
    public ToDoItem UpdateToDoItem(int id, [FromBody] ToDoItem toDoItem)
    {
        toDoItem.Id = id;
        return _ToDoItemRepository.Update(toDoItem);
    }

    [HttpPost]
    public ToDoItem CreateToDoItem([FromBody] ToDoItem toDoItem)
    {
        return _ToDoItemRepository.Insert(toDoItem);
    }

    [HttpDelete("{id}")]
    public void DeleteToDoItem(int id)
    {
        _ToDoItemRepository.Delete(id);
    }

    [HttpDelete]
    public void DeleteAllToDoItems()
    {
        _ToDoItemRepository.DeleteAll();
    }



}

