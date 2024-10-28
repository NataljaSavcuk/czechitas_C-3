namespace ToDoList.WebApi;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTO;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]

public class ToDoItemsController : ControllerBase
{

    private static readonly List<ToDoItem> toDoItems = [];

    [HttpPost]
    [Route("api/ToDoItems")]
    public IActionResult Create([FromBody] ToDoItemCreateRequestDto request)
    {
        var newToDoItem = request.ToDomain();

        try
        {
            newToDoItem.ToDoItemId = toDoItems.Count == 0 ? 1 : toDoItems.Max(o => o.ToDoItemId) + 1;
            toDoItems.Add(newToDoItem);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return CreatedAtAction(nameof(ReadById), new { toDoItemid = newToDoItem.ToDoItemId }, newToDoItem);
    }

    [HttpGet]
    [Route("api/ToDoItems")]
    public IActionResult Read()
    {
        var response = new List<ToDoItemGetResponseDto>();

        try
        {
            if (toDoItems == null || toDoItems.Count == 0)
            {
                return NotFound();
            }

            foreach (var toDoItem in toDoItems)
            {
                response.Add(ToDoItemGetResponseDto.FromDomain(toDoItem));
            }
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("api/ToDoItems/{toDoItemId}")]
    public IActionResult ReadById(int toDoItemId)
    {
        try
        {
            var toDoItem = toDoItems.Find(o => o.ToDoItemId == toDoItemId);
            if (toDoItem == null)
            {
                return NotFound();
            }
            var response = ToDoItemGetResponseDto.FromDomain(toDoItem);
            return Ok(response);
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
            var currentToDoItem = toDoItems.FirstOrDefault(o => o.ToDoItemId == toDoItemId);
            var indexOfOldInstance = toDoItems.FindIndex(o => o.ToDoItemId == toDoItemId);

            if (currentToDoItem == null || indexOfOldInstance == -1)
            {
                return NotFound();
            }

            var updatedToDoItem = request.ToDomain();
            updatedToDoItem.ToDoItemId = toDoItemId;
            toDoItems[indexOfOldInstance] = updatedToDoItem;

            return NoContent();
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
            var itemToDelete = toDoItems.Find(o => o.ToDoItemId == toDoItemId);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            toDoItems.Remove(itemToDelete);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

}



