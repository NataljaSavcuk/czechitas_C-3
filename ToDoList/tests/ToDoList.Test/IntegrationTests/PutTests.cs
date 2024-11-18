namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTO;
using ToDoList.WebApi;
using ToDoList.Persistence.Repository;

public class PutTests
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public PutTests()
    {
        // Set up mock repository
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        repositoryMock.ReadById(toDoItem.ToDoItemId).Returns(toDoItem);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, request);

        // Assert
        Assert.IsType<NoContentResult>(result);

        Assert.Equal("Jine jmeno", toDoItem.Name);
        Assert.Equal("Jiny popis", toDoItem.Description);
        Assert.True(toDoItem.IsCompleted);

        repositoryMock.Received(1).Update(toDoItem);
    }

    [Fact]
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var invalidId = -1;

        repositoryMock.ReadById(invalidId).Returns((ToDoItem)null);

        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(invalidId, request);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        repositoryMock.DidNotReceive().Update(Arg.Any<ToDoItem>());
    }
    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = 1;
        var request = new ToDoItemUpdateRequestDto(
            Name: "Jine jmeno",
            Description: "Jiny popis",
            IsCompleted: true
        );
        repositoryMock.When(r => r.ReadById(toDoItemId))
                  .Do(call => throw new Exception("Chyba prihlaseni do databaze"));

        // Act
        var result = controller.UpdateById(toDoItemId, request);
        var errorResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(errorResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);

        repositoryMock.DidNotReceive().Update(Arg.Any<ToDoItem>());
    }
}
