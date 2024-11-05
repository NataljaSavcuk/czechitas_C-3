namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Domain.DTO;
using ToDoList.WebApi;
using ToDoList.Persistence.Repository;
public class PostTests
{
    private readonly IRepository<ToDoItem> repositoryMock;
    private readonly ToDoItemsController controller;

    public PostTests()
    {
        // Set up the mock repository
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1, // Assume an ID is assigned upon creation
            Name = request.Name,
            Description = request.Description,
            IsCompleted = request.IsCompleted
        };

        // Configure the mock to simulate adding a new item and returning it
        repositoryMock.When(repo => repo.Create(Arg.Any<ToDoItem>()))
                      .Do(callInfo => callInfo.Arg<ToDoItem>().ToDoItemId = toDoItem.ToDoItemId);

        repositoryMock.ReadById(toDoItem.ToDoItemId).Returns(toDoItem);

        // Act
        var result = controller.Create(request);
        var createdAtResult = result as CreatedAtActionResult;
        var value = createdAtResult?.Value as ToDoItem;

        // Assert
        Assert.IsType<CreatedAtActionResult>(createdAtResult);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(toDoItem.ToDoItemId, value.ToDoItemId);

        // Verify that Create was called on the repository
        repositoryMock.Received(1).Create(Arg.Is<ToDoItem>(item =>
            item.Name == request.Name &&
            item.Description == request.Description &&
            item.IsCompleted == request.IsCompleted
        ));
    }
}
