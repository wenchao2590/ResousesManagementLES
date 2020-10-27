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
        /// �������߳��Ƿ���ʶ
        /// </summary>   
        protected bool isWorkThreadActive;
        /// <summary>
        /// �������߳�ÿ�����еļ��ʱ��
        /// </summary>
        protected int waitDuration;
        /// <summary>
        /// �������߳�
        /// </summary>
        private System.Threading.Thread _workthread;

        /// <summary>
        /// ����ֳ��ȴ�ʱ��
        /// </summary>
        private int _monitorWaitTime = 5000;

        /// <summary>
        /// ����̣߳���Ӧǰ̨�Ŀ�������
        /// </summary>
        private System.Threading.Thread _monitorthread;

        /// <summary>
        /// ����߳��Ƿ���ʶ
        /// </summary>
        protected bool isMonitorThreadActive;

        /// <summary>
        /// ϵͳ��ID��ʶ��ÿһ����̨������һ��ID�������ݿ��TS_BAS_ProcessSchedule�������д�ϵͳID��Ӧ�ļ�¼
        /// </summary>
        private Guid _serviceFid;

        /// <summary>
        /// �¼����󣬵������߳��ڹ�����϶ʱ���û�������Ҫֹͣ�����̣߳�Ϊ�˱����û��ȴ�����ʱ�䣬ͨ�����¼�����ֹͣ�̻߳
        /// </summary>
        private AutoResetEvent[] _eventArray;

        private bool _startbytime = false;
        private string _starttime;
        private int _startbytimemode = 0;
        private int _startParameter;
        private DateTime _lastRunTime = DateTime.MinValue;

        /// <summary>
        /// ϵͳ����״̬ö��
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
        /// ����ϵͳ����,��WindowsService����ʱ������
        /// </summary>
        public virtual void Start()
        {
            Logger.Instance.Trace(this, "Entry Start");

            // 0.WindowsService������ʱ�����̲߳����������߳̽�������
            this.isWorkThreadActive = false;

            // 1.���������̵߳��¼�ͬ������
            this._eventArray = new AutoResetEvent[] { new AutoResetEvent(false) };

            // 3.��������߳�
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

                // �Ƿ�ʱ����ģʽ,
                // 1. ����3 ΪHH:mm��ʽ
                // 2. ����4 Ϊ1(����),2(����),3(����)֮һ
                // 3. ����5 Ϊ����
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
        /// ����ϵͳ����,��WindowsService����ʱ������
        /// </summary>
        public void Stop()
        {
            this.isMonitorThreadActive = false;
            Logger.Instance.Trace(this, "Entry Stop");
        }

        /// <summary>
        /// ����̣߳���Ӧ�û�����Թ����̵߳�ֹͣ����������
        /// </summary>
        protected void SystemMonitor()
        {
            Logger.Instance.Trace(this, "Entry SystemMonitor");
            //int index = 0;
            //
            // ϵͳ����߳̽�һֱ����ֱ��WindowsService������
            //
            while (isMonitorThreadActive)
            {
                //Infrustructure.Logger.Instance.Trace(this, string.Format("SystemMonitor loop {0}", (++index)));

                try
                {
                    //��ȡϵͳ��¼
                    ProcessScheduleInfo SystemInfo = ProcessScheduleDAL.GetInfo(_serviceFid);
                    InitialWithConfig(SystemInfo);
                    WorkInDuration(SystemInfo);
                }
                catch (Exception exception)
                {
                    Logger.Instance.Error(this, exception);
                    Logger.Instance.Info(this, "���ڴ���ֹͣ�˹����߳�");
                    #region Modify: Andy-Liu 2008-11-06 ִ�д���ʱ��ֹͣ�����߳�
                    //ֹͣ�����߳�
                    if (isWorkThreadActive)
                        StopWorkThread();

                    #endregion
                }
                System.Threading.Thread.Sleep(_monitorWaitTime);
            }

            //�������̱߳���������ô�����߳�ҲҪ����
            if (isWorkThreadActive)
                StopWorkThread();
            Logger.Instance.Trace(this, "end SystemMonitor");
        }

        private void WorkInDuration(ProcessScheduleInfo SystemInfo)
        {
            if (SystemInfo != null)
            {
                ///�������й������û�ֹͣ���߳�
                if (SystemInfo.Last_run_status == ThreadStop && isWorkThreadActive)
                {
                    Logger.Instance.Trace(this, "should StopWorkThread");

                    //ֹͣ�����߳�
                    StopWorkThread();
                }
                ///�߳��Ѿ�ֹͣ���û������߳�
                if (SystemInfo.Last_run_status == ThreadActive && !isWorkThreadActive)
                {
                    Logger.Instance.Trace(this, "should StartWorkThread");

                    //���������߳�
                    StartWorkThread();
                }
            }
        }

        /// <summary>
        /// ���������߳�
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
        /// ֹͣ�����̣߳�����ֹ�߳���ǰ�ȴ�60��
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
                //Infrustructure.Logger.Instance.Trace(this, "Process��ʼ");
                result = Process();
                //Infrustructure.Logger.Instance.Trace(this, "Process����");
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
            //Infrustructure.Logger.Instance.Trace(this, "GC.Collect��ʼ");
            GC.Collect();
            //Infrustructure.Logger.Instance.Trace(this, "GC.Collect����");
            if (result)
            {
                Logger.Instance.Trace(this, "Process succeeded");
            }
            else
            {
                Logger.Instance.Info(this, "Process failed");
            }

            // ��¼�������еĿ�ʼʱ��ͽ���ʱ��
            endTime = DateTime.Now;
            LogRunTime(startTime, endTime);
        }

        #endregion
    }


}