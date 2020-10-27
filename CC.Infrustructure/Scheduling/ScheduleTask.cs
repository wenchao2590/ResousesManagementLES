#region

using Infrustructure.Logging;
using System;
using System.Collections;
using System.Threading;
using System.Xml;

#endregion

namespace Infrustructure.Scheduling
{
	public abstract class ScheduleTask : IScheduleTask, IDisposable
	{
		#region Variables
		private bool _disposed;
		private bool _isRunning;
		private DateTime _lastEnd;
		private DateTime _lastStarted;
		private DateTime _lastSuccess;
		private int _seconds;
		private Timer _timer;
		private Hashtable _otherAttributes = new Hashtable();
		#endregion

		internal ScheduleTask()
		{
			_seconds = 3600;
		}

		#region Properties

		public int Interval
		{
			get { return _seconds*1000; }
		}

		public bool IsRunning
		{
			get { return _isRunning; }
		}

		public DateTime LastEnd
		{
			get { return _lastEnd; }
		}

		public DateTime LastStarted
		{
			get { return _lastStarted; }
		}

		public DateTime LastSuccess
		{
			get { return _lastSuccess; }
		}

		public Hashtable OtherAttributes
		{
			get { return _otherAttributes; }
		}

		#endregion

		#region IDisposable Members

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
		{
			Logger.Instance.Trace(this, "stop task");
			if ((_timer != null) && !_disposed)
				lock (this)
				{
					_timer.Dispose();
					_timer = null;
					_disposed = true;
				}
		}

		#endregion

		#region IScheduleTask Members

		public void Execute()
		{
			if (_isRunning)
			{
				Logger.Instance.Trace(this, "still last running");
				return;
			}

			_isRunning = true;
			_lastStarted = DateTime.Now;
			Logger.Instance.Trace(this, "execute task");
			try
			{
				DoTask();

				DateTime dateTime = DateTime.Now;
				_lastSuccess = dateTime;
				_lastEnd = dateTime;
			}
			catch (Exception e)
			{
				_lastEnd = DateTime.Now;
				Logger.Instance.Error(this, e);
			}
			Logger.Instance.Trace(this, "completed task");
			_isRunning = false;
		}

		#endregion

		internal void InitConfig(XmlNode node)
		{
			_isRunning = false;

			foreach(XmlAttribute attr in node.Attributes)
			{
				switch(attr.Name.ToLower())
				{
					case "type":
						break;
					case "seconds":
						if (!Int32.TryParse(attr.Value, out _seconds))
							_seconds = 3600;
						break;
					default:
						_otherAttributes[attr.Name] = attr.Value;
						break;
				}
			}
		}

		private void timer_callback(object p1)
		{
			_timer.Change(-1, -1);
			Execute();
			_timer.Change(Interval, Interval);
		}

		internal void Start()
		{
			Logger.Instance.Trace(this, "start task");
			if (_timer == null)
				_timer = new Timer(timer_callback, null, 0, Interval);
		}

		public abstract void DoTask();
	}
}