using FileSystemDistributor.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor.Utils
{
    public class Logger : ILogger
    {
        private const string loggedKey = "Logged";
        private const string sectionSeparatorStart = "************************ Exception Information ****************************";
        private const string sectionSeparatorEnd = "***************************************************************************";

        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogException(Exception ex)
        {
            LogException(ex, "");
        }

        public void LogException(Exception ex, params Tuple<string, string>[] parameters)
        {
            LogException(ex, "", parameters);
        }

        public void LogException(Exception ex, string message = "", params Tuple<string, string>[] parameters)
        {
            bool shouldLog = ShouldLogException(ex);

            if (!shouldLog && string.IsNullOrEmpty(message) && parameters.Length == 0)
            {
                return;
            }

            Console.WriteLine("");
            Console.WriteLine(sectionSeparatorStart);
            if (shouldLog)
            {
                LogExceptionInformation(ex);
            }
            else
            {
                //This exception was already written out once. This attempt has a different message
                //and/or parameters to log and therefore the function didn't return early.
                Console.WriteLine("This exception was already logged.");
            }

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Message: " + message);
            }

            LogParameters(parameters);
            Console.WriteLine(sectionSeparatorEnd);
            Console.WriteLine("");
        }

        private void LogParameters(params Tuple<string, string>[] parameters)
        {
            if (parameters.Length > 0)
            {
                Console.WriteLine("Parameters:");
                parameters.ToList().ForEach(p => Console.WriteLine($"Name: {p.Item1}. Value: {p.Item2}"));
            }
        }

        private void LogExceptionInformation(Exception ex)
        {
            Console.WriteLine(ex);
            if (ex.Data.Keys.Count > 0)
            {
                Console.WriteLine("Exception Data:");
                foreach (var key in ex.Data.Keys)
                {
                    Console.WriteLine(ex.Data[key].ToString());
                }
            }

            ex.Data.Add(loggedKey, true);
        }

        /// <summary>
        /// Exceptions can be logged, thrown, logged again, thrown again, etc any number of times.
        /// This function will determine if the exception has not been logged before and should be.
        /// </summary>
        /// <param name="ex">Exception to analyze</param>
        /// <returns>True if the given should be logged</returns>
        private bool ShouldLogException(Exception ex)
        {
            if (ex == null)
            {
                return false;
            }

            //Would be weird to have false added as a value but check it anyways.
            bool value;
            bool shouldLog = (ex.Data[loggedKey] == null || (bool.TryParse(ex.Data[loggedKey].ToString(), out value) && !value));
            return shouldLog;
        }
    }
}
