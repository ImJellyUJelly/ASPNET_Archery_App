using System;
using System.Runtime.Serialization;

namespace ArcheryApplication.Classes
{
    [Serializable]
    internal class WedstrijdException : Exception
    {
        public WedstrijdException()
        {
        }

        public WedstrijdException(string message) : base(message)
        {
        }

        public WedstrijdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WedstrijdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}