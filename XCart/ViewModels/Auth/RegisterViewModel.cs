using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Text.RegularExpressions;
using XCart.Common.CustomExceptions;
using XCart.Services.Auth;

namespace XCart.ViewModels.Auth
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string? firstName;

        [ObservableProperty]
        private string? lastName;

        [ObservableProperty]
        private string? phone;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private string? errorMessage;

        public RegisterViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        private async Task NavigateToLoginAsync()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task RegisterUserAsync()
        {
            if (!await IsValid())
                return;

            var user = new Models.User.UserModel()
            {
                FirstName = FirstName!.Trim(),
                LastName = LastName!.Trim(),
                Phone = Phone!.Trim(),
                Email = Email!.Trim(),
                Password = Password!.Trim()
            };

            try
            {
                await _authService.RegisterAsync(user);
                ErrorMessage = string.Empty;
                await Shell.Current.Navigation.PopAsync();
            }
            catch (UserAlreadyExistsException ex)
            {
                ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorMessage = "An unexpected error occurred. Please try again.";
                Console.WriteLine(ex);
            }
        }

        private async Task<bool> IsValid()
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                await ShowSnackbar("Please enter both first and last names.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Phone) || !IsValidIndianPhoneNumber(Phone))
            {
                await ShowSnackbar("Please enter a valid Indian phone number.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email))
            {
                await ShowSnackbar("Please enter a valid email address.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 6)
            {
                await ShowSnackbar("Password must be at least 6 characters long.");
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidIndianPhoneNumber(string phone)
        {
            string phonePattern = @"^(\+91[\-\s]?)?[0]?[789]\d{9}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        private async Task ShowSnackbar(string message)
        {
            await Snackbar.Make(message).Show();
        }
    }
}
