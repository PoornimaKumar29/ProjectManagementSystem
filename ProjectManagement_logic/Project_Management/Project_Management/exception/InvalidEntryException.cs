using System;

namespace Project_Management.exception
{
    // Custom exception class for invalid entries
    public class InvalidEntryException : Exception
    {
        // Default constructor
        public InvalidEntryException()
            : base("An invalid entry was detected.") { }

        // Constructor that accepts a custom message
        public InvalidEntryException(string message)
            : base(message) { }

        // Constructor that accepts a custom message and an inner exception
        public InvalidEntryException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
