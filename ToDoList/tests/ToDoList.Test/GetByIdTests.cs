namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
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
    public void GetById_ValidId_ReturnsItem()
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
    public void GetById_InvalidId_ReturnsNotFound()
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
}
