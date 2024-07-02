using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Models.User;

namespace XCart.Services.Auth
{
    public interface IAuthService
    {
        Task<UserModel?> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(UserModel user);
        Task<Session> GetLoginSessionAsync();
        Task<UserModel?> GetUserByIdAsync(string? userId);
        Task Logout();
    }
}
