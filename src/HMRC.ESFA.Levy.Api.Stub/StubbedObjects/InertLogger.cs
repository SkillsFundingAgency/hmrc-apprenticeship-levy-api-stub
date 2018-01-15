using SFA.DAS.NLog.Logger;
using System;
using System.Collections.Generic;

namespace HMRC.ESFA.Levy.Api.Stub.StubbedObjects
{
    public class InertLogger : ILog
    {
        public void Debug(string message)
        {
        }

        public void Debug(string message, IDictionary<string, object> properties)
        {
        }

        public void Debug(string message, ILogEntry logEntry)
        {
        }

        public void Error(Exception ex, string message)
        {
        }

        public void Error(Exception ex, string message, IDictionary<string, object> properties)
        {
        }

        public void Error(Exception ex, string message, ILogEntry logEntry)
        {
        }

        public void Fatal(Exception ex, string message)
        {
        }

        public void Fatal(Exception ex, string message, IDictionary<string, object> properties)
        {
        }

        public void Fatal(Exception ex, string message, ILogEntry logEntry)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, IDictionary<string, object> properties)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, ILogEntry logEntry)
        {
        }

        public void Trace(string message)
        {
        }

        public void Trace(string message, IDictionary<string, object> properties)
        {
        }

        public void Trace(string message, ILogEntry logEntry)
        {
        }

        public void Warn(string message)
        {
        }

        public void Warn(string message, IDictionary<string, object> properties)
        {
        }

        public void Warn(string message, ILogEntry logEntry)
        {
        }

        public void Warn(Exception ex, string message)
        {
        }

        public void Warn(Exception ex, string message, IDictionary<string, object> properties)
        {
        }

        public void Warn(Exception ex, string message, ILogEntry logEntry)
        {
        }
    }
}