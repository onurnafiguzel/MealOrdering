using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.Shared
{
    public class PasswordEncrypter
    {
        public static String Encrypt(String Password)
        {           
            var plainTextBytes = Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
