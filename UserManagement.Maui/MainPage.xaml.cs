using UserManagement.Common.Models;
using UserManagement.Maui.ViewModels;

namespace UserManagement.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;


        }

        private async void OnToggleChanged(object sender, ToggledEventArgs e)
        {
            if (sender is Switch sw && sw.BindingContext is User user)
            {
                var vm = BindingContext as MainViewModel;
                await vm?.ToggleActiveCommand.ExecuteAsync(user);
            }
        }
    }
}
