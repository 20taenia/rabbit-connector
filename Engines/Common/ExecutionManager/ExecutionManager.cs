﻿using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Charon.Engines.Common
{
    public class ExecutionManager
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly string _asyncThreadName = "AsyncTaskExecutorThread";
        private readonly string _threadName = "TaskExecutorThread";

        private long _syncThreadIndex = 1;
        private List<RequestExecutorAsync> _asyncExecutorList = null;
        private ConcurrentQueue<IRequestTask> _requestTasks = null;
        private EventWaitHandle _waitHandle = null;
        private System.Timers.Timer _throttleTimer = null;

        public ExecutionManager(int asyncThreadCount = 50, int asyncProcessingInterval = 1)
        {
            _waitHandle = new AutoResetEvent(false);
            _requestTasks = new ConcurrentQueue<IRequestTask>();
            _asyncExecutorList = new List<RequestExecutorAsync>();

            //Create execution classes for async operations (each containing a worker thread) 
            _logger.Log(LogLevel.Info, "Initialising execution manager with " + asyncThreadCount + " threads");
            for (int i = 1; i <= asyncThreadCount; i++)
                _asyncExecutorList.Add(new RequestExecutorAsync(_requestTasks, _waitHandle, _asyncThreadName + i));

            //Initialis throttle timer
            _throttleTimer = new System.Timers.Timer();
            _throttleTimer.Elapsed += onThrottleTimerElapsed;
            _throttleTimer.Interval = asyncProcessingInterval;
            _throttleTimer.Start();
        }

        private void onThrottleTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ProcessAsyncTasks();
        }

        public void AddTask(IRequestTask requestTask)
        {
            //Enqueue async tasks for processing
            if (requestTask.IsAsyncRequest)
                _requestTasks.Enqueue(requestTask);
            else
            {
                //Create executor and execute syncronously
                var executor = new RequestExecutor(requestTask, _threadName + _syncThreadIndex);
                _syncThreadIndex++;
            }
        }

        public void ProcessAsyncTasks()
        {
            _logger.Log(LogLevel.Trace, string.Format("{0} asynchronous tasks waiting to be executed.", _requestTasks.Count));
            while (_requestTasks.Count > 0)
            {
                _waitHandle.Set();
            }
        }
    }
}
