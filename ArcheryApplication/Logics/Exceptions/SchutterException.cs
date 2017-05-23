using System;

namespace ArcheryApplication.Classes
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
