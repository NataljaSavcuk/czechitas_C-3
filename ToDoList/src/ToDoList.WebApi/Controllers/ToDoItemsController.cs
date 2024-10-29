namespace ToDoList.WebApi;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTO;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]

public class ToDoItemsController(ToDoItemsContext context) : ControllerBase
{
    public readonly List<ToDoItem> toDoItems;
    private readonly ToDoItemsContext context = context;

    [HttpPost]
    [Route("api/ToDoItems")]
    public IActionResult Create([FromBody] ToDoItemCreateRequestDto request)
    {
        var newToDoItem = request.ToDomain();

        try
        {
            context.Add(newToDoItem);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return CreatedAtAction(nameof(ReadById), new { toDoItemid = newToDoItem.ToDoItemId }, newToDoItem);
    }

    [HttpGet]
    [Route("api/ToDoItems")]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        var toDoItems = context.ToDoItems.ToList();

        try
        {
            if (toDoItems.Count == 0)
            {
                return NotFound();
            }

            var response = toDoItems.Select(ToDoItemGetResponseDto.FromDomain).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("api/ToDoItems/{toDoItemId}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        try
        {
            var toDoItem = context.ToDoItems.Find(toDoItemId);

            if (toDoItem != null)
            {
                var response = ToDoItemGetResponseDto.FromDomain(toDoItem);
                return Ok(response);
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    [Route("api/ToDoItems/{toDoItemId}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var currentToDoItem = context.ToDoItems.Find(toDoItemId);

            if (currentToDoItem != null)
            {
                currentToDoItem.Name = request.Name ?? currentToDoItem.Name;
                currentToDoItem.Description = request.Description ?? currentToDoItem.Description;
                currentToDoItem.IsCompleted = request.IsCompleted;

                context.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    [Route("api/ToDoItems/{toDoItemId}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = context.ToDoItems.Find(toDoItemId);

            if (itemToDelete != null)
            {
                context.Remove(itemToDelete);
                context.SaveChanges();

                return NoContent();
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
