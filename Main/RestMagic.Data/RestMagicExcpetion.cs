using System;
using System.Runtime.Serialization;

namespace RestMagic.Lib
{
    [Serializable]
    public class RestMagicExcpetion : Exception
    {
        public RestMagicExcpetion()
        {
        }

        public RestMagicExcpetion(string message) : base(message)
        {
        }

        public RestMagicExcpetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RestMagicExcpetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}