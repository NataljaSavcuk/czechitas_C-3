namespace ToDoList.Test;

using ToDoList.Domain.Models;
using ToDoList.WebApi;
using Microsoft.AspNetCore.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc;


public class PutTests
{
    [Fact]
    public void UpdateById_ShouldReturnNoContentResult()
    {
        // Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);

        var newToDoItem = new ToDoItem
        {
            ToDoItemId = 999999,
            Name = "Update_Test",
            Description = "Test of update method",
            IsCompleted = false
        };

        var updatedToDoItem = new Domain.DTO.ToDoItemUpdateRequestDto(
            Name: "Test_Pass",
            Description: "Test of update method is passed",
            IsCompleted: true
        );

        controller.toDoItems.Add(newToDoItem);

        // Act
        var result = controller.UpdateById(newToDoItem.ToDoItemId, updatedToDoItem);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Equal("Test_Pass", controller.toDoItems[0].Name);
        Assert.Equal("Test of update method is passed", controller.toDoItems[0].Description);
        Assert.True(controller.toDoItems[0].IsCompleted);
    }
    [Fact]
    public void UpdateById_SouldReturnsNotFound()
    {
        //Arrange
        var mockToDoItems = new List<ToDoItem>();
        var controller = new ToDoItemsController(mockToDoItems);

        var updatedToDoItem = new Domain.DTO.ToDoItemUpdateRequestDto(
        Name: "Test_Pass",
        Description: "Test of update method is passed",
        IsCompleted: true
        );

        //Act
        var result = controller.UpdateById(999999, updatedToDoItem);

        //Assert
        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public void UpdateById_SouldReturnsInternalServerError()
    {
        //Arrange
        var controller = new ToDoItemsController();

        var updatedToDoItem = new Domain.DTO.ToDoItemUpdateRequestDto(
        Name: "Test_Pass",
        Description: "Test of update method is passed",
        IsCompleted: true
        );

        //Act
        var result = controller.UpdateById(999999, updatedToDoItem) as ObjectResult;

        //Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
    }


}
