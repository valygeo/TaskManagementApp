using System;
using System.Collections.Generic;
using System.Text;

namespace UserServices.Service
{
    public interface IUserAuthorizationService
    {
        string GetToken(User user);
        bool ValidateToken(string tokenString);
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
