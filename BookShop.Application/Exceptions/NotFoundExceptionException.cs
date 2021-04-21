using System;

namespace BookShop.Application.Exceptions
{
    public class NotFoundExceptionException : Exception
    {
        public NotFoundExceptionException(string message) : base(message)
        {
            
        }
    }
}
