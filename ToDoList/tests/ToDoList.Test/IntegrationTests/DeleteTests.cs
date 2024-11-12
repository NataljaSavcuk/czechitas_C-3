namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.WebApi;
using ToDoList.Persistence.Repository;


public class DeleteTests
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public DeleteTests()
    {
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {
        // Arrange
        var toDoItemId = 1;
        var toDoItem = new ToDoItem
        {
            ToDoItemId = toDoItemId,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        repositoryMock.ReadById(toDoItemId).Returns(toDoItem);

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);

        repositoryMock.Received(1).DeleteById(toDoItemId);
    }

    [Fact]
    public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = -1;

        repositoryMock.ReadById(invalidId).Returns((ToDoItem)null);

        // Act
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        repositoryMock.Received(1).ReadById(invalidId);
        repositoryMock.Received(0).DeleteById(invalidId);
    }

    [Fact]
    public void Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var toDoItemId = -1;

        repositoryMock.When(r => r.ReadById(toDoItemId))
                        .Do(call => throw new Exception("Chyba prihlaseni do databaze"));

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        var errorResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(errorResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, errorResult.StatusCode);
        repositoryMock.Received(0).DeleteById(toDoItemId);
    }
}
