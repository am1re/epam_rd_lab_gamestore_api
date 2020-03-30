using System;
using System.Collections.Generic;

namespace Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public Dictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

        public BadRequestException() : base("One or more validation failures have occurred.")
        {
        }
        
        public BadRequestException(string message) : base(message)
        {
        }
        
        public BadRequestException(Dictionary<string, string[]> failures) : this()
        {
            Failures = failures;
        }
    }
}