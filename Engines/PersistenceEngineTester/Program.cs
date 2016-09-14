using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Charon.Engines.PersistenceEngineTester
{
    public class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        static bool _exitSystem = false;

        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        private static EventHandler _handler;
        private static Tester _tester;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool ExitHandler(CtrlType sig)
        {
            _logger.Log(LogLevel.Info, "Exiting system due to external CTRL-C, or process kill, or shutdown");

            //do your cleanup here
            _tester.Stop();
            _logger.Log(LogLevel.Info, "Cleanup complete");

            //allow main to run off
            _exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        static void Main(string[] args)
        {
            // Some biolerplate to react to close window event, CTRL-C, kill, etc
            _handler += new EventHandler(ExitHandler);
            SetConsoleCtrlHandler(_handler, true);

            //start your multi threaded program here
            _tester = new Tester();
            _tester.Start();

            //We can exit if work is done (got to here)
            _exitSystem = true;

            //hold the console so it doesn’t run off the end
            while (!_exitSystem)
            {
                Thread.Sleep(500);
            }
        }

        public void Start()
        {
            // start a thread and start doing some processing
            _logger.Log(LogLevel.Info, "Thread started, processing..");
        }
    }
}