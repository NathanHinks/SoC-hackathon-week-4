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
    public ToDoItem UpdateToDoItem(int id, [FromBody] ToDoItem ToDoItem)
    {
        ToDoItem.Id = id;
        return _ToDoItemRepository.Update(ToDoItem);
    }

    [HttpPost]
    public ToDoItem CreateToDoItem([FromBody] ToDoItem ToDoItem)
    {
        return _ToDoItemRepository.Insert(ToDoItem);
    }

    [HttpDelete("{id}")]
    public void DeleteToDoItem(int id)
    {
        _ToDoItemRepository.Delete(id);
    }


}

