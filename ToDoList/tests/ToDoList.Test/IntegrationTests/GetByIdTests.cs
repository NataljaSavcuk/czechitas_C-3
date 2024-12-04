namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTO;
using ToDoList.Persistence.Repository;
using ToDoList.WebApi;
using NSubstitute;
using Xunit;

public class GetByIdTests
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public GetByIdTests()
    {
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }
    [Fact]
    public void Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {
        // Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false,
            Category = "test",
            TaskPriority = Domain.Types.Priority.Low
        };

        repositoryMock.ReadById(toDoItem.ToDoItemId).Returns(toDoItem);

        // Act
        var result = controller.ReadById(toDoItem.ToDoItemId);
        var okResult = result.Result as OkObjectResult;
        var value = okResult?.Value as ToDoItemGetResponseDto;

        // Assert
        Assert.IsType<OkObjectResult>(okResult);
        Assert.NotNull(value);

        Assert.Equal(toDoItem.Description, value.Description);
        Assert.Equal(toDoItem.IsCompleted, value.IsCompleted);
        Assert.Equal(toDoItem.Name, value.Name);
    }

    [Fact]
    public void Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var invalidId = -1;
        repositoryMock.ReadById(invalidId).Returns((ToDoItem)null);

        // Act
        var result = controller.ReadById(invalidId);
        var notFoundResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(notFoundResult);
    }
    [Fact]
    public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        repositoryMock.When(r => r.ReadById(Arg.Any<int>()))
                        .Do(call => throw new Exception("Chyba prihlaseni do databaze"));

        // Act
        var result = controller.ReadById(1);
        var errorResult = result.Result as ObjectResult;

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
    }

}
