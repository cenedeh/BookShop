using System;

namespace BookShop.Application.Exceptions
{
    public class UserAuthenticationException : Exception
    {
        public UserAuthenticationException(string message) : base(message)
        {
            
        }
    }
}
