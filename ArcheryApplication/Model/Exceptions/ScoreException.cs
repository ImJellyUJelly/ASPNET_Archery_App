using System;
using System.Runtime.Serialization;

namespace Model.Exceptions
{
    [Serializable]
    public class ScoreException : Exception
    {
        public ScoreException()
        {
        }

        public ScoreException(string message) : base(message)
        {
        }

        public ScoreException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ScoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}