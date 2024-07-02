using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCart.Models.User
{
    public class Session
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string? UserId { get; set; }

        public bool IsLoggedIn { get; set; }
    }
}
