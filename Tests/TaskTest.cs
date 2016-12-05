using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using ToDoList.Objects;

namespace ToDoList
{
  public class ToDoTest //: IDisposable
  {
    public ToDoTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo_test;Integrated Security=SSPI;";
    }
    // public void Dispose()
    // {
    //   Task.DeleteAll();
    // }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Task.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }
    // [Fact]
    // public void Test_Save_SavesToDatabase()
    // {
    //   //Arrange
    //   Task testTask = new Task("Mow the lawn");
    //
    //   //Act
    //   testTask.Save();
    //   List<Task> result = Task.GetAll();
    //   List<Task> testList = new List<Task>{testTask};
    //
    //   //Assert
    //   Assert.Equal(testList, result);
    // }
    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Task firstTask = new Task("Mow the lawn");
      Task secondTask = new Task("Mow the lawn");

      //Assert
      Assert.Equal(firstTask, secondTask);
    }
  }
}
