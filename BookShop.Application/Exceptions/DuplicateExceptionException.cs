using System;

namespace BookShop.Application.Exceptions
{
    public class DuplicateExceptionException : Exception
    {
        public DuplicateExceptionException(string message) : base(message)
        {
            
        }
    }
}
