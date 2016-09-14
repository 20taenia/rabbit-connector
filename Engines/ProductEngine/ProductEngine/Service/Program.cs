using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Charon.Engines.ProductEngine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
       {
            if (Environment.UserInteractive)
            {
                ServiceWrapper serviceWrapper = new ServiceWrapper(args);
                serviceWrapper.TestStart(args);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new ServiceWrapper(args) };
                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
