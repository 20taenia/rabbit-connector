using System;
using System.Data.Entity.Validation;
using System.Text;

namespace Charon.Data.EntityFramework
{
    public class FormattedDbEntityValidationException : Exception
    {
        private string _message = null;

        public FormattedDbEntityValidationException(DbEntityValidationException innerException) : base(null, innerException)
        {
            // This is done as context is likely to have been disposed by the time the message is read.
            // So we cache the message.
            _message = GenerateMessage();
        }

        private string GenerateMessage()
        {
            var innerException = InnerException as DbEntityValidationException;
            if (innerException != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine();
                sb.AppendLine();
                foreach (var eve in innerException.EntityValidationErrors)
                {
                    sb.AppendLine(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage));
                    }
                }
                sb.AppendLine();

                return sb.ToString();
            }

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
