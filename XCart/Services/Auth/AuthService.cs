using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCart.Common.CustomExceptions;
using XCart.Helpers;
using XCart.Models.User;

namespace XCart.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly DatabaseHelper _databaseHelper;

        public AuthService(DatabaseHelper database,DatabaseHelper databaseHelper)
        {
            _database = database.GetDatabaseConnection();
            _databaseHelper = databaseHelper;
        }

        public async Task<Session> GetLoginSessionAsync()
        {
            return await _databaseHelper.GetSessionAsync();
        }

        public async Task<UserModel?> LoginAsync(string email, string password)
        {
            var user = await _database.Table<UserModel>().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                var session = new Session
                {
                    UserId = user.Id.ToString(),
                    IsLoggedIn = true,
                };
               await  _databaseHelper.SaveSessionAsync(session);
            }
            return user;
        }

        public async Task<bool> RegisterAsync(UserModel user)
        {
            var existingUser = await _database.Table<UserModel>().FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User with this email already exists.");
            }

            await _database.InsertAsync(user);
            return true;
        }

        public async Task<UserModel?> GetUserByIdAsync(string userId)
        {
            if (int.TryParse(userId, out int userIdInt))
            {
                return await _database.Table<UserModel>().FirstOrDefaultAsync(u => u.Id == userIdInt);
            }
            return null;
        }

        public async Task Logout()
        {
             await _databaseHelper.DeleteSessionAsync();
        }
    }
}
