namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc;

public class PostTests
{
    [Fact]
    public void Create_SouldReturnsCreated()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);
        var request = new Domain.DTO.ToDoItemCreateRequestDto
        (
        "Create_Test",
        "Test of create method",
        true
        );

        // Act
        var result = controller.Create(request) as CreatedAtActionResult;

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal("Create_Test", controller.toDoItems[0].Name);
        Assert.Equal("Test of create method", controller.toDoItems[0].Description);
        Assert.True(controller.toDoItems[0].IsCompleted);
    }

    [Fact]
    public void Create_SouldReturnsInternalServerError()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var request = new Domain.DTO.ToDoItemCreateRequestDto
        (
        "Create_Test",
        "Test of create method",
        true
        );

        // Act
        var result = controller.Create(request) as ObjectResult;

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
    }
}

