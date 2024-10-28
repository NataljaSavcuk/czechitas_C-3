namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTO;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Http;
using Xunit;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ShouldReturnsNotFound()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);


        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.True(resultResult is NotFoundResult);
    }

    [Fact]
    public void Get_AllItems_SouldReturnsSomeData()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);
        var request = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Test_Get_AllItems",
            Description = "Test of get all items method",
            IsCompleted = false
        };
        mockToDoItems.Add(request);

        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        var okResult = resultResult as OkObjectResult;
        var returnItems = (okResult.Value as IEnumerable<ToDoItemGetResponseDto>).ToList();
        Assert.Single(returnItems);
        Assert.Equal(request.Name, returnItems[0].Name);
    }

    [Fact]
    public void Get_AllItems_SouldReturnsInternalServerError()
    {
        // Arrange
        var controller = new ToDoItemsController();

        // Act
        var result = controller.Read();
        var resultObject = result.Result as ObjectResult;

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, resultObject.StatusCode);
    }


}
