using System;

namespace SchoolGradingSystem
{
    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }
}
