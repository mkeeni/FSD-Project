﻿namespace Mavericks_Bank.Exceptions
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(string? message) : base(message)
        {

        }
    }
}
