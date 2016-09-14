using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Charon.Engines.Common
{
    public class RequestExecutor
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private Thread _worker = null;
        private IRequestTask _requestTask = null;

        public RequestExecutor(IRequestTask requestTask, string threadName)
        {
            _requestTask = requestTask;
            _worker = new Thread(DoWork);
            _worker.Name = threadName;
            _worker.Start();
        }

        public void DoWork()
        {
            _logger.Log(LogLevel.Info, string.Format("{0} is executing request id: {1}", _worker.Name, _requestTask.Request.Id));
            _requestTask.Execute();
        }
    }
}
