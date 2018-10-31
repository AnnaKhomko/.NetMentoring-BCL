using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor.Utils.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="message">Message to log</param>
        void Log(string message);

        /// <summary>
        /// Log exception details
        /// </summary>
        /// <param name="ex">Exception to log</param>
        void LogException(Exception ex);

        /// <summary>
        /// Log exception details and the given message. Parameters passed in to the function logging the exception will be logged as well.
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="parameters">Parameter values passed in to any function from which the given exception was raised. Tuple is variable name and value</name></param>
        void LogException(Exception ex, params Tuple<string, string>[] parameters);

        /// <summary>
        /// Log exception details and the given message. Parameters passed in to the function logging the exception will be logged as well.
        /// </summary>
        /// <param name="ex">Exception to log</param>
        /// <param name="message">Message about the exception</param>
        /// <param name="parameters">Parameter values passed in to any function from which the given exception was raised. Tuple is variable name and value</name></param>
        void LogException(Exception ex, string message, params Tuple<string, string>[] parameters);
    }
}
