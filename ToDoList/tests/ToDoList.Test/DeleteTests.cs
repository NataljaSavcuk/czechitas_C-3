namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc;

public class DeleteTests
{
    [Fact]
    public void DeleteById_SouldReturnsNoContent()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);
        var newToDoItem = new ToDoItem
        {
            ToDoItemId = 999999,
            Name = "Delete_Test",
            Description = "Test of delete method",
            IsCompleted = false
        };

        controller.toDoItems.Add(newToDoItem);
        //Act
        var result = controller.DeleteById(999999);

        //Assert
        Assert.IsType<NoContentResult>(result);
    }


    [Fact]
    public void DeleteById_SouldReturnsNotFound()
    {
        //Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);

        //Act
        var result = controller.DeleteById(999999);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteById_SouldReturnsInternalServerError()
    {
        //Arrange
        var controller = new ToDoItemsController();

        //Act
        var result = controller.DeleteById(999999) as ObjectResult;
        ;

        //Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
    }
}


