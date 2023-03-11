﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MealOrdering.Shared.CustomExceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public ApiException(string message) : base(message)
        {

        }
    }
}
