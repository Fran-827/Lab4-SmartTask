using SmartTaskClassLibrary.ViewModels;

namespace SmartTask
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
