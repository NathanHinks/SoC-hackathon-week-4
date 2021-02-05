using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]s")]
public class ToDoItemController : ControllerBase
{
    private readonly IRepository<ToDoItem> _toDoItemRepository;

    public ToDoItemController(IRepository<ToDoItem> toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllToDoItems()
    {
        try
        {
            var toDoItems = await _toDoItemRepository.GetAll();
            
            return Ok(toDoItems);
        }
        catch
        {
            return NotFound("Cannot find to do items");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetToDoItem(int id)
    {
        try {
            var toDoItem = await _toDoItemRepository.GetOne(id);

            return Ok(toDoItem);
        }
        catch {

            return NotFound($"Cannot find book {id}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateToDoItem(int id, [FromBody] ToDoItem toDoItem)
    {
        try {
            toDoItem.Id = id;
            var updatedToDo = await _toDoItemRepository.Update(toDoItem);

            return Ok(updatedToDo);
        }
        catch {

            return NotFound($"Could not find book {id}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDoItem([FromBody] ToDoItem toDoItem)
    {
        try {
            var newToDo = await _toDoItemRepository.Insert(toDoItem);

            return Created($"new book with id {toDoItem.Id} created", newToDo);
        }
        catch {
            return BadRequest("Could not create new To Do");
        }
    }

    [HttpDelete("{id}")]
    public async Task DeleteToDoItem(int id)
    {
        try
        {
            await _toDoItemRepository.Delete(id);
        }
        catch {
            NotFound($"book {id} not found");
        }
    }

    [HttpDelete]
    public async Task DeleteAllToDoItems()
    {
        try
        {
            await _toDoItemRepository.DeleteAll();
        }
        catch {
            NotFound("Could not find books to delete");
        }
    }
}

