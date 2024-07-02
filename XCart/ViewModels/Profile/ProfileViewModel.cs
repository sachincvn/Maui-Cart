using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Services.Auth;
using XCart.ViewModels.Abstract;
using XCart.ViewModels.Auth;
using XCart.Views.Auth;

namespace XCart.ViewModels.Profile
{
    public partial class ProfileViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string? fullName;

        [ObservableProperty]
        private string? email;

        private readonly IAuthService _authService;
        private readonly IServiceProvider _serviceProvider;


        public ProfileViewModel(IAuthService authService, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _authService = authService;
        }

        public override async Task InitAsync(object? parameter)
        {
            var session = await _authService.GetLoginSessionAsync();
            if (session != null)
            {
                var loggedInUser = await _authService.GetUserByIdAsync(session.UserId);
                if (loggedInUser!=null)
                {
                    FullName = $"{loggedInUser.FirstName} {loggedInUser.LastName}";
                    Email = loggedInUser.Email;
                }
            }
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            await _authService.Logout();
            var loginViewModel = _serviceProvider.GetRequiredService<LoginViewModel>();
            Application.Current!.MainPage = new LoginView(loginViewModel);
        }
    }
}
