using System;
using System.Runtime.Serialization;

namespace Redis2Go.Exceptions
{
    [Serializable]
    public class PortTakenException : Exception
    {
        public PortTakenException() { }
        public PortTakenException(string message) : base(message) { }
        public PortTakenException(string message, Exception inner) : base(message, inner) { }
        protected PortTakenException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
