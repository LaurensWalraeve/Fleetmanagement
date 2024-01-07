using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using FleetManagement.Common.Exceptions;

namespace FleetManagement.Common.Models
{
    public class User
    {
        private string _password;
        private int _userId;
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public User(string password, string username, Role role)
        {
            Password = password;
            Username = username;
            Role = role;
        }
        public User(string password, string username, Role role, Driver driver)
        {
            Password = password;
            Username = username;
            Role = role;
            Driver = driver;
        }

        public User() { }

        public int UserID
        {
            get => _userId;
            set
            {
                if (value == 0)
                    throw new ValidationException("User ID cannot be null");
                _userId = value;
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Password cannot be empty");

                // Regular expression to check for at least 8 characters and at least 1 digit
                var passwordPattern = @"^(?=.*\d).{8,}$";

                if (!Regex.IsMatch(value, passwordPattern))
                    throw new ValidationException("Password must have at least 8 characters and at least 1 digit.");

                _password = HashPassword(value);
            }
        }


        public string Username
        {
            get => _username;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ValidationException("Username cannot be empty");

                _username = value.ToLower(); // Convert the username to lowercase
            }
        }

        private string _username;
        public Role? Role { get; set; }
        public Driver? Driver { get; set; }

        public static string HashPassword(string password)
        {
            // Create an instance of the SHA-256 hash algorithm
            using (var sha256 = SHA256.Create())
            {
                // Hash the password bytes with SHA-256
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Use a StringBuilder to convert the hashed bytes to a hexadecimal string
                var builder = new StringBuilder();
                for (var i = 0; i < hashedBytes.Length; i++)
                    builder.Append(hashedBytes[i].ToString("x2"));

                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password)
        {
            return string.Equals(this.Password, HashPassword(password), StringComparison.OrdinalIgnoreCase);
        }
    }
}
