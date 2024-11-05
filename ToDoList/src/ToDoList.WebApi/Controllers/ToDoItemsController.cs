namespace ToDoList.WebApi;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTO;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repository;

[ApiController]

public class ToDoItemsController(IRepository<ToDoItem> repository) : ControllerBase
{
    private readonly IRepository<ToDoItem> repository = repository;

    [HttpPost]
    [Route("api/ToDoItems")]
    public IActionResult Create([FromBody] ToDoItemCreateRequestDto request)
    {
        var newToDoItem = request.ToDomain();
        try
        {
            repository.Create(newToDoItem);
            return CreatedAtAction(nameof(ReadById), new { toDoItemId = newToDoItem.ToDoItemId }, newToDoItem);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Route("api/ToDoItems")]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        try
        {
            var toDoItems = repository.Read().ToList();
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
            var toDoItem = repository.ReadById(toDoItemId);

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
            var currentToDoItem = repository.ReadById(toDoItemId);
            if (currentToDoItem != null)
            {
                currentToDoItem.Name = request.Name ?? currentToDoItem.Name;
                currentToDoItem.Description = request.Description ?? currentToDoItem.Description;
                currentToDoItem.IsCompleted = request.IsCompleted;

                repository.UpdateById(currentToDoItem);
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
            var toDoItem = repository.ReadById(toDoItemId);
            if (toDoItem != null)
            {
                repository.DeleteById(toDoItemId);
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
