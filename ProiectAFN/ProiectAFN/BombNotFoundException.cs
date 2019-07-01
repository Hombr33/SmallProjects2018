using System;
using System.Runtime.Serialization;

namespace ProiectAFN
{
    [Serializable]
    internal class BombNotFoundException : Exception
    {
        public BombNotFoundException()
        {
        }

        public BombNotFoundException(string message) : base(message)
        {
        }

        public BombNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BombNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}