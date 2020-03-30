using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Exceptions
{
    public class ConflictDataException : Exception
    {
        public Dictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

        public ConflictDataException() : base("One or more data conflicts have occurred.")
        {
        }
        
        public ConflictDataException(string message) : base(message)
        {
        }

        public ConflictDataException(string fieldName, string message) : this()
        {
            Failures.Add(fieldName, Enumerable.Repeat(message, 1 ).ToArray());
        }
        
        public ConflictDataException(Dictionary<string, string[]> failures) : this()
        {
            Failures = failures;
        }
    }
}