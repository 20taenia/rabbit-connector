using System;
using System.Data.Entity.Infrastructure;

namespace Charon.Data.EntityFramework
{
    public class BaseDbException : Exception
    {
        private string _message = null;

        // This is done as context is likely to have been disposed by the time the message is read.
        // So we cache the message.
        public BaseDbException(Exception innerException) : base(null, innerException)
        {
            _message = GenerateMessage();
        }

        private string GenerateMessage()
        {

            var exception = GetBaseException();

            if (exception != null)
                return exception.Message;

            return base.Message;
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
