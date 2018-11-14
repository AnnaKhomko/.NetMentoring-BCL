using FileSystemDistributor.Resources;
using FileSystemDistributor.Utils.Interfaces;
using System;

namespace FileSystemDistributor.Utils
{
	public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogException(Exception ex)
        {
            LogException(ex, "");
        }
        public void LogException(Exception ex, string message = "")
        {
            Console.WriteLine(Strings.LoggerStartSeparator);
            Console.WriteLine(ex);
            if (ex.Data.Keys.Count > 0)
            {
                Console.WriteLine(Strings.ExceptionDataMessage);
                foreach (var key in ex.Data.Keys)
                {
                    Console.WriteLine(String.Format(Strings.ExceptionData, key, ex.Data[key].ToString()));
                }
            }
            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(String.Format(Strings.ExceptionMessage,message));
            }

            Console.WriteLine(Strings.LoggerEndSeparator);
        }
    }
}
