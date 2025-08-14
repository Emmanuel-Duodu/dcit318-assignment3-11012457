using System;

namespace SchoolGradingSystem
{
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }
}
