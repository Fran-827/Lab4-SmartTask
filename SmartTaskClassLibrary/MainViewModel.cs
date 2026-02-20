using System.Collections.ObjectModel;
using System.Text.Json;
using SmartTaskClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace SmartTaskClassLibrary.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        string filePath = @"D:\2TS2526\CPE106L\Lab4\SmartTask\smarttasks.json";

        public List<SmartTaskModel> allTasks = new();

        public ObservableCollection<SmartTaskModel> Tasks { get; set; } = new();

        public List<string> Categories { get; } = new List<string>
        {
            "All", "Work", "School", "Personal"
        };

        public List<string> Statuses { get; } = new List<string>
        {
            "All", "Pending", "Completed"
        };

        [ObservableProperty]
        private string newTaskName;

        [ObservableProperty]
        private string newTaskDescription;

        [ObservableProperty]
        private string selectedCategory;

        [ObservableProperty]
        private string selectedFilterCategory = "All";

        [ObservableProperty]
        private string selectedFilterStatus = "All";


        public MainViewModel()
        {
            LoadTasks();
            SortTasks();
        }

        [RelayCommand]
        private void SetStatusFilter(string status)
        {
            SelectedFilterStatus = status;
            ApplyFilter();
        }

        [RelayCommand]
        private void SetCategoryFilter(string category)
        {
            SelectedFilterCategory = category;
            ApplyFilter();
        }

        [RelayCommand]
        private void LoadTasks()
        {
            if (!File.Exists(filePath)) return;

            var json = File.ReadAllText(filePath);
            var savedTasks = JsonSerializer.Deserialize<List<SmartTaskModel>>(json);

            if (savedTasks != null)
                allTasks = savedTasks;

            ApplyFilter();
        }

        [RelayCommand]
        private void SaveTasks()
        {
            var json = JsonSerializer.Serialize(allTasks,
                new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        [RelayCommand]
        private void AddTask()
        {
            if (string.IsNullOrWhiteSpace(NewTaskName) ||
                string.IsNullOrWhiteSpace(SelectedCategory))
                return;

            var task = new SmartTaskModel
            {
                TaskName = NewTaskName,
                TaskDescription = NewTaskDescription,
                Category = SelectedCategory,
                Status = "Pending"
            };

            allTasks.Add(task);
            SaveTasks();

            NewTaskName = string.Empty;
            NewTaskDescription = string.Empty;
            SelectedCategory = null;

            ApplyFilter();
        }

        [RelayCommand]
        public void DeleteTask(SmartTaskModel task)
        {
            if (task == null) return;

            allTasks.Remove(task);
            SaveTasks();
            ApplyFilter();
        }

        [RelayCommand]
        private void CompleteTask(SmartTaskModel task)
        {
            if (task == null) return;

            task.Status = "Completed";
            SaveTasks();
            ApplyFilter();
        }

        [RelayCommand]
        public void ApplyFilter()
        {
            var filtered = allTasks.AsEnumerable();

            if (SelectedFilterCategory != "All")
                filtered = filtered.Where(t => t.Category == SelectedFilterCategory);

            if (SelectedFilterStatus != "All")
                filtered = filtered.Where(t => t.Status == SelectedFilterStatus);

            filtered = filtered.OrderBy(t => t.Status == "Completed");

            Tasks.Clear();
            foreach (var task in filtered)
            {
                Tasks.Add(task);
            }
        }

        private void SortTasks()
        {
            Tasks = new ObservableCollection<SmartTaskModel>(
                Tasks.OrderBy(t => t.Status == "Completed")
            );
        }

    }
}
