namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
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
    public void Put_ValidId_ReturnsNoContent()
    {
        // Arrange
        var toDoItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Jmeno",
            Description = "Popis",
            IsCompleted = false
        };

        // Set up mock to return the item for a valid ID
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

        // Verify the item was updated correctly
        Assert.Equal("Jine jmeno", toDoItem.Name);
        Assert.Equal("Jiny popis", toDoItem.Description);
        Assert.True(toDoItem.IsCompleted);

        // Ensure that the SaveChanges method was called to persist changes
        repositoryMock.Received(1).UpdateById(toDoItem);
    }

    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = -1;

        // Configure the mock to return null when an invalid ID is requested
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

        // Verify that Update was not called, as the item does not exist
        repositoryMock.DidNotReceive().UpdateById(Arg.Any<ToDoItem>());
    }
}
