using System;
using System.Runtime.Serialization;

namespace Redis2Go.Exceptions
{
    [Serializable]
    public class RedisBinariesNotFoundException : Exception
    {
        public RedisBinariesNotFoundException() { }
        public RedisBinariesNotFoundException(string message) : base(message) { }
        public RedisBinariesNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected RedisBinariesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
