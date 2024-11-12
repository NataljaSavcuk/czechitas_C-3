namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTO;
using ToDoList.Persistence.Repository;
using ToDoList.WebApi;
using NSubstitute;
using Xunit;

public class GetTests
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public GetTests()
    {
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        var toDoItems = new List<ToDoItem> { toDoItem };
        repositoryMock.Read().Returns(toDoItems);

        // Act
        var result = controller.Read();
        var okResult = result.Result as OkObjectResult;
        var value = okResult?.Value as IEnumerable<ToDoItemGetResponseDto>;
        var firstItem = value?.First();

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
        Assert.NotNull(value);
        Assert.Equal(toDoItem.Name, firstItem?.Name);
        Assert.Equal(toDoItem.Description, firstItem?.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem?.IsCompleted);
    }

    [Fact]
    public void Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        repositoryMock.Read().Returns(new List<ToDoItem>());

        // Act
        var result = controller.Read();
        var notFoundResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
    }

    [Fact]
    public void Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        repositoryMock.When(r => r.Read())
                        .Do(call => throw new Exception("Chyba prihlaseni do databaze"));

        // Act
        var result = controller.Read();
        var errorResult = result.Result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(errorResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
    }
}
