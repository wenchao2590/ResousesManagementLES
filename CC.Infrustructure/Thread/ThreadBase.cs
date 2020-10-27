using Infrustructure.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Infrustructure.Thread
{
    abstract public class ThreadBase
    {
        #region Properties
        /// <summary>
        /// 主工作线程是否活动标识
        /// </summary>   
        protected bool isWorkThreadActive;
        /// <summary>
        /// 主工作线程每次运行的间隔时间
        /// </summary>
        protected int waitDuration;
        /// <summary>
        /// 主工作线程
        /// </summary>
        private System.Threading.Thread _workthread;

        /// <summary>
        /// 监控现场等待时间
        /// </summary>
        private int _monitorWaitTime = 5000;

        /// <summary>
        /// 监控线程，响应前台的控制命令
        /// </summary>
        private System.Threading.Thread _monitorthread;

        /// <summary>
        /// 监控线程是否活动标识
        /// </summary>
        protected bool isMonitorThreadActive;

        /// <summary>
        /// 系统的ID标识，每一个后台进程有一个ID，在数据库表TS_BAS_ProcessSchedule中配置有此系统ID对应的记录
        /// </summary>
        private Guid _serviceFid;

        /// <summary>
        /// 事件对象，当工作线程在工作间隙时，用户可能需要停止工作线程，为了避免用户等待过久时间，通过此事件对象停止线程活动
        /// </summary>
        private AutoResetEvent[] _eventArray;

        private bool _startbytime = false;
        private string _starttime;
        private int _startbytimemode = 0;
        private int _startParameter;
        private DateTime _lastRunTime = DateTime.MinValue;

        /// <summary>
        /// 系统运行状态枚举
        /// </summary>
        private const int ThreadStop = 40;
        private const int ThreadActive = 20;
        private const int ThreadPause = 30;
        private const int ThreadStep = 50;
        #endregion

        #region Constructor
        /// <summary>
        /// class constructor
        /// </summary>
        /// <param name="waitInterval"></param>
        public ThreadBase(Guid serviceFid)
        {
            this._serviceFid = serviceFid;
        }
        #endregion

        #region methods
        /// <summary>
        /// 启动系统服务,当WindowsService启动时被调用
        /// </summary>
        public virtual void Start()
        {
            Logger.Instance.Trace(this, "Entry Start");

            // 0.WindowsService刚启动时工作线程不活动，待监控线程将其启动
            this.isWorkThreadActive = false;

            // 1.创建工作线程的事件同步对象
            this._eventArray = new AutoResetEvent[] { new AutoResetEvent(false) };

            // 3.启动监控线程
            this.isMonitorThreadActive = true;
            this._monitorthread = new System.Threading.Thread(new ThreadStart(SystemMonitor));
            this._monitorthread.Start();

        }

        private void InitialWithConfig(ProcessScheduleInfo SystemInfo)
        {
            if (SystemInfo != null)
            {
                _monitorWaitTime = SystemInfo.Check_interval * 1000;
                this.waitDuration = SystemInfo.Run_interval;

                // 是否定时启动模式,
                // 1. 参数3 为HH:mm格式
                // 2. 参数4 为1(按天),2(按周),3(按月)之一
                // 3. 参数5 为整数
                string param3 = SystemInfo.System_parameter3;
                string param4 = SystemInfo.System_parameter4;
                string param5 = SystemInfo.System_parameter5;
                if (!string.IsNullOrEmpty(param3) &&
                    !string.IsNullOrEmpty(param4) &&
                    !string.IsNullOrEmpty(param5) &&
                    Regex.IsMatch(param3, "([0-1][0-9]|2[0-3]):([0-5][0-9])") &&
                    int.TryParse(param4, out _startbytimemode) &&
                    _startbytimemode > 0 && _startbytimemode < 4 &&
                    int.TryParse(param5, out _startParameter))
                {
                    _startbytime = true;
                    _starttime = param3;
                }

            }
        }

        /// <summary>
        /// 结束系统服务,当WindowsService结束时被调用
        /// </summary>
        public void Stop()
        {
            this.isMonitorThreadActive = false;
            Logger.Instance.Trace(this, "Entry Stop");
        }

        /// <summary>
        /// 监控线程，响应用户界面对工作线程的停止、启动操作
        /// </summary>
        protected void SystemMonitor()
        {
            Logger.Instance.Trace(this, "Entry SystemMonitor");
            //int index = 0;
            //
            // 系统监控线程将一直运行直到WindowsService被结束
            //
            while (isMonitorThreadActive)
            {
                //Infrustructure.Logger.Instance.Trace(this, string.Format("SystemMonitor loop {0}", (++index)));

                try
                {
                    //读取系统记录
                    ProcessScheduleInfo SystemInfo = ProcessScheduleDAL.GetInfo(_serviceFid);
                    InitialWithConfig(SystemInfo);
                    WorkInDuration(SystemInfo);
                }
                catch (Exception exception)
                {
                    Logger.Instance.Error(this, exception);
                    Logger.Instance.Info(this, "由于错误停止了工作线程");
                    #region Modify: Andy-Liu 2008-11-06 执行错误时，停止工作线程
                    //停止工作线程
                    if (isWorkThreadActive)
                        StopWorkThread();

                    #endregion
                }
                System.Threading.Thread.Sleep(_monitorWaitTime);
            }

            //如果监控线程被结束，那么工作线程也要结束
            if (isWorkThreadActive)
                StopWorkThread();
            Logger.Instance.Trace(this, "end SystemMonitor");
        }

        private void WorkInDuration(ProcessScheduleInfo SystemInfo)
        {
            if (SystemInfo != null)
            {
                ///正在运行过程中用户停止了线程
                if (SystemInfo.Last_run_status == ThreadStop && isWorkThreadActive)
                {
                    Logger.Instance.Trace(this, "should StopWorkThread");

                    //停止工作线程
                    StopWorkThread();
                }
                ///线程已经停止，用户启动线程
                if (SystemInfo.Last_run_status == ThreadActive && !isWorkThreadActive)
                {
                    Logger.Instance.Trace(this, "should StartWorkThread");

                    //启动工作线程
                    StartWorkThread();
                }
            }
        }

        /// <summary>
        /// 启动工作线程
        /// </summary>
        protected virtual void StartWorkThread()
        {
            //Console.WriteLine("StartWorkThread");
            this.isWorkThreadActive = true;
            this._workthread = new System.Threading.Thread(new ThreadStart(Run));
            this._workthread.SetApartmentState(ApartmentState.STA);
            this._workthread.Start();
            this._eventArray[0].Reset();

            Logger.Instance.Trace(this, "StartWorkThread completed");
        }

        /// <summary>
        /// 停止工作线程，在终止线程以前等待60秒
        /// </summary>
        protected virtual void StopWorkThread()
        {
            //Console.WriteLine("StopWorkThread");
            //
            // Signal the stop event.
            //
            this.isWorkThreadActive = false;
            this._eventArray[0].Set();

            //
            // Wait up to 6 seconds for the thread.
            // 
            int waitTime = 6000;

            while (waitTime > 0)
            {
                if (this._workthread.ThreadState == System.Threading.ThreadState.Stopped)
                    break;

                System.Threading.Thread.Sleep(100);
                waitTime -= 100;
            }

            //
            // If the thread is still not finished, terminate it.
            //
            if (waitTime <= 0)
            {
                this._workthread.Abort();
            }
            _lastRunTime = DateTime.MinValue;

            Logger.Instance.Trace(this, "StopWorkThread completed");
        }

        /// <summary>
        /// The derived class should implement Process() function to define  
        /// the actual useful code.
        /// </summary>
        /// <returns></returns>
        abstract protected bool Process();


        private void LogRunTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                ProcessScheduleDAL.UpdateRunTime(_serviceFid, startTime, endTime);
            }
            catch (Exception exception)
            {
                Logger.Instance.Error(this, exception);
            }

        }

        /// <summary>
        /// Entry function of service thread, which keeps calling Process()
        /// until the stop flat is set.
        /// </summary>
        [STAThreadAttribute()]
        protected void Run()
        {
            Logger.Instance.Trace(this, "Entry Run");
            this.isWorkThreadActive = true;

            int index = 0;
            //
            // Repeat until isExiting is set to true.
            //
            while (this.isWorkThreadActive)
            {
                Logger.Instance.Trace(this, string.Format("Run loop {0}, lastruntime {1}, startbytime {2}", (++index), _lastRunTime, _startbytime));
                if (_lastRunTime != DateTime.MinValue || !_startbytime)
                {
                    try
                    {
                        RunSub();
                    }
                    catch (System.Exception ex)
                    {
                        Logger.Instance.Error(this, ex);
                    }
                }
                //int duration = this.waitDuration * 1000;
                int duration = GetWaitDuration();
                Logger.Instance.Trace(this, duration.ToString());
                //
                // Wait for waitDuration seconds 
                //
                WaitHandle.WaitAny(this._eventArray, duration, false);
                Logger.Instance.Trace(this, "wait");

            }
            Logger.Instance.Trace(this, "Exit Run" + this.isWorkThreadActive.ToString());
        }

        private int GetWaitDuration()
        {
            if (_lastRunTime == DateTime.MinValue)
                _lastRunTime = DateTime.Now;

            int returnvalue;
            if (_startbytime)
            {
                DateTime durtime = DateTime.Parse(DateTime.Today.ToShortDateString() + " " + _starttime);

                switch (_startbytimemode)
                {
                    case 1:     // by day
                        while (durtime <= _lastRunTime)
                        {
                            durtime = durtime.AddDays(1);
                        }
                        break;
                    case 2:     // by week
                        while (durtime <= _lastRunTime || (int)durtime.DayOfWeek != _startParameter)
                        {
                            durtime = durtime.AddDays(1);
                        }
                        break;
                    case 3:     // by month
                        while (durtime <= _lastRunTime || durtime.Day != _startParameter)
                        {
                            durtime = durtime.AddDays(1);
                        }
                        break;
                    default:
                        return this.waitDuration * 1000;
                }
                TimeSpan t = (durtime - DateTime.Now);
                returnvalue = Convert.ToInt32(t.TotalMilliseconds);
                Logger.Instance.Trace(this, string.Format("GetWaitDuration: duration[{0}],durtime[{1}]", returnvalue, durtime));
            }
            else
                returnvalue = this.waitDuration * 1000;


            if (returnvalue < 0)
            {
                returnvalue = 3000;
            }

            return returnvalue;
        }

        private void RunSub()
        {
            Logger.Instance.Trace(this, "Entry RunSub");
            bool result = false;
            DateTime startTime, endTime;

            startTime = DateTime.Now;

            try
            {
                //Infrustructure.Logger.Instance.Trace(this, "Process开始");
                result = Process();
                //Infrustructure.Logger.Instance.Trace(this, "Process结束");
            }
            catch (System.Exception ex)
            {
                Logger.Instance.Error(this, ex);
                result = false;
            }
            finally
            {
                _lastRunTime = DateTime.Now;
            }

            //
            // Force the garbage collector to collect garbage now.
            //
            //Infrustructure.Logger.Instance.Trace(this, "GC.Collect开始");
            GC.Collect();
            //Infrustructure.Logger.Instance.Trace(this, "GC.Collect结束");
            if (result)
            {
                Logger.Instance.Trace(this, "Process succeeded");
            }
            else
            {
                Logger.Instance.Info(this, "Process failed");
            }

            // 记录本轮运行的开始时间和结束时间
            endTime = DateTime.Now;
            LogRunTime(startTime, endTime);
        }

        #endregion
    }


}