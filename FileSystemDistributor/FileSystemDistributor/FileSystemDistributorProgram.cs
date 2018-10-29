using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDistributor
{
    class FileSystemDistributorProgram
    {
        private static bool isclosing = false;

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

            Console.WriteLine("CTRL+C,CTRL+BREAK or suppress the application to exit");

            while (!isclosing) ;
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                    isclosing = true;
                    Console.WriteLine("CTRL+C received!");
                    break;
                case CtrlTypes.CTRL_BREAK_EVENT:
                    isclosing = true;
                    Console.WriteLine("CTRL+BREAK received!");
                    break;
            }
            return true;
        }

        #region unmanaged

        // Declare the SetConsoleCtrlHandler function
        // as external and receiving a delegate.
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine
        // for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages
        // sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT
        }

        #endregion
    }
}
