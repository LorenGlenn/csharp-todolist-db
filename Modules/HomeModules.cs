using Nancy;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
        {
          Get["/"] = _ => {
              return View["index.cshtml"];
          };
          Get["/tasks/new"] = _ => {
            return View["task_form.cshtml"];
          };
          Post["/tasks/new"] = _ => {
            Task newTask = new Task(Request.Form["task-description"]);
            newTask.Save();
            return View["success.cshtml"];
          };
          Get["/categories"] = _ => {
            var allCategories = Category.GetAll();
            return View["categories.cshtml", allCategories];
          };
          Get["/tasks"] =_=> {
            List<Task> allTasks = Task.GetAll();
            return View["tasks.cshtml", allTasks];
          };
          Get["/categories/new"] = _ => {
            return View["category_form.cshtml"];
          };
          Post["/categories/new"] = _ => {
            Category newCategory = new Category(Request.Form["category-name"]);
            newCategory.Save();
            return View["success.cshtml"];
          };
          Get["tasks/{id}"] = parameters => {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Task SelectedTask = Task.Find(parameters.id);
            List<Category> TaskCategories = SelectedTask.GetCategories();
            List<Category> AllCategories = Category.GetAll();
            model.Add("task", SelectedTask);
            model.Add("taskCategories", TaskCategories);
            model.Add("allCategories", AllCategories);
            return View["task.cshtml", model];
          };
          Get["categories/{id}"] = parameters => {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category SelectedCategory = Category.Find(parameters.id);
            List<Task> CategoryTasks = SelectedCategory.GetTasks();
            List<Task> AllTasks = Task.GetAll();
            model.Add("category", SelectedCategory);
            model.Add("categoryTasks", CategoryTasks);
            model.Add("allTasks", AllTasks);
            return View["category.cshtml", model];
          };
          Post["/categories"] = _ => {
            var newCategory = new Category(Request.Form["category-name"]);
            newCategory.Save();
            var allCategories = Category.GetAll();
            return View["categories.cshtml", allCategories];
          };
          Post["/tasks"] = _ => {
            Category selectedCategory = Category.Find(Request.Form["category-id"]);
            int selectedCategoryId = selectedCategory.GetId();
            string taskDescription = Request.Form["task-description"];
            Task newTask = new Task(taskDescription);
            newTask.Save();
            List<Task> categoryTasks = selectedCategory.GetTasks();
            return View["category.cshtml", selectedCategory];
          };
          Post["task/add_category"] = _ => {
            Category category = Category.Find(Request.Form["category-id"]);
            Task task = Task.Find(Request.Form["task-id"]);
            task.AddCategory(category);
            return View["success.cshtml"];
          };
          Post["category/add_task"] = _ => {
            Category category = Category.Find(Request.Form["category-id"]);
            Task task = Task.Find(Request.Form["task-id"]);
            category.AddTask(task);
            return View["success.cshtml"];
          };
        }
      }
    }
