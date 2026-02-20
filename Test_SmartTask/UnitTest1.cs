using SmartTaskClassLibrary.Models;
using System.Threading.Tasks;

namespace Test_SmartTask
{
    public class TestMainViewModel
    {
        [Fact]
        public void TestAddTask()
        {
            var viewModel = new SmartTaskClassLibrary.ViewModels.MainViewModel();
            viewModel.NewTaskName = "Test Task add";
            viewModel.NewTaskDescription = "This is a test task.";
            viewModel.SelectedCategory = "Work";

            viewModel.AddTaskCommand.Execute(null);

            Assert.Contains(viewModel.Tasks, t => t.TaskName == "Test Task add" && t.TaskDescription == "This is a test task." && t.Category == "Work");
        }

        [Fact]
        public void TestDeleteTask()
        {
            var viewModel = new SmartTaskClassLibrary.ViewModels.MainViewModel();
            var task = new SmartTaskModel
            {
                TaskName = "Test Task",
                TaskDescription = "This is a test task.",
                Category = "Work"
            };
            viewModel.allTasks.Add(task);
            viewModel.ApplyFilter();
            viewModel.DeleteTaskCommand.Execute(task);

            Assert.DoesNotContain(viewModel.Tasks, t => t.TaskName == "Test Task" );
        }

        [Fact]
        public void TestSetStatusFilter()
        {
            var viewModel = new SmartTaskClassLibrary.ViewModels.MainViewModel();
            var task1 = new SmartTaskClassLibrary.Models.SmartTaskModel
            {
                TaskName = "Task 1",
                TaskDescription = "This is task 1.",
                Category = "Work",
                Status = "Pending"
            };
            var task2 = new SmartTaskClassLibrary.Models.SmartTaskModel
            {
                TaskName = "Task 2",
                TaskDescription = "This is task 2.",
                Category = "Work",
                Status = "Completed"
            };
            viewModel.allTasks.Add(task1);
            viewModel.allTasks.Add(task2);
            viewModel.ApplyFilter();


            viewModel.SetStatusFilterCommand.Execute("Pending");

            Assert.Contains(viewModel.Tasks, t => t.TaskName == "Task 1" && t.Status == "Pending");
            Assert.DoesNotContain(viewModel.Tasks, t => t.TaskName == "Task 2" && t.Status == "Completed");
        }

        [Fact]
        public void TestSetCategoryFilter()
        {

            var viewModel = new SmartTaskClassLibrary.ViewModels.MainViewModel();
            var task1 = new SmartTaskClassLibrary.Models.SmartTaskModel
            {
                TaskName = "Task 1",
                TaskDescription = "This is task 1.",
                Category = "Work"
            };
            var task2 = new SmartTaskClassLibrary.Models.SmartTaskModel
            {
                TaskName = "Task 2",
                TaskDescription = "This is task 2.",
                Category = "Personal"
            };
            viewModel.allTasks.Add(task1);
            viewModel.allTasks.Add(task2);
            viewModel.ApplyFilter();

            viewModel.SetCategoryFilterCommand.Execute("Work");

            Assert.Contains(viewModel.Tasks, t => t.TaskName == "Task 1" && t.Category == "Work");
            Assert.DoesNotContain(viewModel.Tasks, t => t.TaskName == "Task 2" && t.Category == "Personal");
        }

        [Fact]
        public void TestSaveAndLoadTasks()
        {

            var viewModel = new SmartTaskClassLibrary.ViewModels.MainViewModel();
            var task = new SmartTaskModel
            {
                TaskName = "Test Save and Load",
                TaskDescription = "This is a test task.",
                Category = "Work"
            };
            viewModel.allTasks.Add(task);

            viewModel.SaveTasksCommand.Execute(null);
            viewModel.Tasks.Clear();
            viewModel.LoadTasksCommand.Execute(null);

            Assert.Contains(viewModel.Tasks, t => t.TaskName == "Test Save and Load" && t.TaskDescription == "This is a test task." && t.Category == "Work");
        }
    }
}