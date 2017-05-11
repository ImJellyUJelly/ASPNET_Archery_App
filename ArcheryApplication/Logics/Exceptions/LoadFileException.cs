using System;
using System.Runtime.Serialization;

namespace ArcheryApplication.Classes
{
    [Serializable]
    internal class LoadFileException : Exception
    {
        public LoadFileException()
        {
        }

        public LoadFileException(string message) : base(message)
        {
        }

        public LoadFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoadFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}