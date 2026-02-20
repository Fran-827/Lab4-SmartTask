using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTaskClassLibrary.Models
{
    public partial class SmartTaskModel : ObservableObject
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Category { get; set; }

        [ObservableProperty]
        public string status;

        public bool IsCompleted => Status == "Completed";
        public bool IsNotCompleted => !IsCompleted;

        public string StatusColor =>
            Status == "Completed"
            ? "Green"
            : "Orange";

        public string categoryColor =>
            Category switch
            {
                "Work" => "Blue",
                "School" => "Purple",
                "Personal" => "Teal",
                _ => "Gray"
            };


        partial void OnStatusChanged(string value)
        {
            OnPropertyChanged(nameof(IsCompleted));
            OnPropertyChanged(nameof(IsNotCompleted));
            OnPropertyChanged(nameof(StatusColor));
            OnPropertyChanged(nameof(categoryColor));
        }
    }
}
