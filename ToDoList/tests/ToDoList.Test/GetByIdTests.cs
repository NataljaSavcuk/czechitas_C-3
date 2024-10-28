namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTO;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Http;
using Xunit;

public class GetByIdTests
{
    [Fact]
    public void ReadById__SouldReturnsNotFound()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);

        // Act
        var result = controller.ReadById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void ReadById_SouldReturnsSomeData()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Test_Get_Item_by_Id",
            Description = "Test of get by id method",
            IsCompleted = true
        };
        mockToDoItems.Add(toDoItem);
        var controller = new ToDoItemsController(mockToDoItems);

        // Act
        var result = controller.ReadById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(result);

        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);

        var returnItem = okResult.Value as ToDoItemGetResponseDto;
        Assert.NotNull(returnItem);

        Assert.Equal(toDoItem.Name, returnItem.Name);
        Assert.Equal(toDoItem.Description, returnItem.Description);
        Assert.Equal(toDoItem.IsCompleted, returnItem.IsCompleted);
    }

    [Fact]
    public void ReadById_SouldReturnsInternalServerError()
    {
        // Arrange
        var controller = new ToDoItemsController();

        // Act
        var result = controller.ReadById(999);
        var resultObject = result.Result as ObjectResult;

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }

}
