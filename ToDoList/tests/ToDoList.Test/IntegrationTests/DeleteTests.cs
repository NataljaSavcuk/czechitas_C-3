namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
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
        // Set up mock repository
        repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        controller = new ToDoItemsController(repositoryMock);
    }

    [Fact]
    public void Delete_ValidId_ReturnsNoContent()
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

        // Set up mock to return the item for the valid ID
        repositoryMock.ReadById(toDoItemId).Returns(toDoItem);

        // Act
        var result = controller.DeleteById(toDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);

        // Verify that DeleteById was called with the correct ID
        repositoryMock.Received(1).DeleteById(toDoItemId);
    }

    [Fact]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = -1;

        // Configure the mock to return null for an invalid ID
        repositoryMock.ReadById(invalidId).Returns((ToDoItem)null);

        // Act
        var result = controller.DeleteById(invalidId);

        // Assert
        Assert.IsType<NotFoundResult>(result);

        // Verify that Delete was not called since the item does not exist
        repositoryMock.Received(1).ReadById(invalidId);
        repositoryMock.Received(0).DeleteById(invalidId);
    }
}
