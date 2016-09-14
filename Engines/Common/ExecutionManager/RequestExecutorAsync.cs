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
    public class RequestExecutorAsync
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private Thread _worker = null;
        private ConcurrentQueue<IRequestTask> _requestTasks = null;
        private EventWaitHandle _waitHandle = null;

        public RequestExecutorAsync(ConcurrentQueue<IRequestTask> requestTasks, EventWaitHandle waitHandle, string threadName)
        {
            _waitHandle = waitHandle;
            _requestTasks = requestTasks;
            _worker = new Thread(DoWork);
            _worker.Name = threadName;
            _worker.Start();
        }

        public void DoWork()
        {
            while (true)
            {
                if (_requestTasks == null)
                    break;

                IRequestTask requestTask = null;
                if (_requestTasks.TryDequeue(out requestTask))
                {
                    _logger.Log(LogLevel.Info, string.Format("{0} is asyncronously executing request id: {1}", _worker.Name, requestTask.Request.Id));
                    requestTask.Execute();
                }

                _waitHandle.WaitOne();
            }
        }
    }
}
