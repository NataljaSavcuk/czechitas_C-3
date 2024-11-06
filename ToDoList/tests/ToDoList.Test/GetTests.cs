namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTO;
using ToDoList.WebApi;
using ToDoList.Persistence.Repository;
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
    public void Get_AllItems_ReturnsAllItems()
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
        var resultResult = result.Result as OkObjectResult;
        var value = resultResult?.Value as IEnumerable<ToDoItemGetResponseDto>;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First();

        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);
    }

    [Fact]
    public void Get_NoItems_ReturnsNotFound()
    {
        // Arrange
        repositoryMock.Read().Returns(new List<ToDoItem>());

        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}
