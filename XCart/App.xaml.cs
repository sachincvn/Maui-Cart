using Microsoft.Extensions.DependencyInjection;
using XCart.Helpers;
using XCart.Services.Auth;
using XCart.Views.Auth;
using XCart.Views.Home;

namespace XCart
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            MainPage = new AppShell();
            InitializeMainPageAsync();  
        }

        private async void InitializeMainPageAsync()
        {
            await Task.Delay(2000);
            var authService = _serviceProvider.GetRequiredService<IAuthService>();
            var session = await authService.GetLoginSessionAsync();

            if (session != null && session.IsLoggedIn)
            {
                await Shell.Current.GoToAsync("//HomeView");
            }
            else
            {
                await Shell.Current.GoToAsync("LoginView");
            }
        }
    }
}
