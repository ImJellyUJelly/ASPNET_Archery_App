using System;

namespace Model.Exceptions
{
    public class SchutterException : Exception
    {
        public SchutterException()
        {
            
        }
        public SchutterException(string message) : base(message)
        {

        }
    }
}
