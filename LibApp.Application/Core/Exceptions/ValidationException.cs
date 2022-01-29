using LibApp.Application.Core.Models;
using System;
using System.Collections.Generic;

namespace LibApp.Application.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public List<ValidationError> Errors { get; private set; }

        public ValidationException(List<ValidationError> errors, string message): base(message)
        {
            Errors = errors;
        }
    }
}
