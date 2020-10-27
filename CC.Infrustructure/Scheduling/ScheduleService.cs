#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using Infrustructure.Utilities;
using Infrustructure.Logging;

#endregion

namespace Infrustructure.Scheduling
{
	public class ScheduleService : IService
	{
		#region Variables
		private static readonly ScheduleService _instance;

		private readonly List<ScheduleTask> _tasks;

		#endregion

		#region Initialize

		static ScheduleService()
		{
			_instance = new ScheduleService();
		}

		private ScheduleService()
		{
			_tasks = new List<ScheduleTask>();
			Initialize();
		}


		public static ScheduleService Instance()
		{
			// TODO: by alex hu@20080617 need monitoring the file changes
			return _instance;
		}

		private void Initialize()
		{
			Logger.Instance.Trace(this, "initialized ScheduleService");
			// TODO: by alex hu@20080617 need factory parttern to allow varieties config data storages.
			InitByXml();
		}

		private void InitByXml()
		{
			string path = ConfigurationManager.AppSettings["schedulingconfig"];
            path = MiscUtil.ResolveFilePath(path);
			if (string.IsNullOrEmpty(path))
			{
				Logger.Instance.Info(this, "没有在config文件中的AppSettings节点配置[schedulingconfig]的值.");
				return;
			}
			if (!File.Exists(path))
			{
				Logger.Instance.Info(this, string.Format("[taskconfig]配置的文件路径'{0}'不存在.", path));
				return;
			}

			XmlDocument doc = new XmlDocument();
			doc.Load(path);
			InitConfigData(doc.SelectSingleNode("tasks"));
		}

		private void InitConfigData(XmlNode p1)
		{
			if (p1 == null)
			{
				Logger.Instance.Trace(this, "empty task config data");
				return;
			}
			_tasks.Clear();
			foreach (XmlNode xmlNode1 in p1.ChildNodes)
			{
				if (xmlNode1.Name.Equals("task", StringComparison.InvariantCultureIgnoreCase))
				{
					XmlAttribute xmlAttribute = xmlNode1.Attributes["type"];
					Type type = Type.GetType(xmlAttribute.Value);
					if (type != null)
					{
						ScheduleTask task = Activator.CreateInstance(type) as ScheduleTask;
						if (task != null)
						{
							task.InitConfig(xmlNode1);
							_tasks.Add(task);
						}
					}
				}
			}
			Logger.Instance.Trace(this, string.Format("initialized {0} tasks", _tasks.Count));
		}

		#endregion

		#region IService Members

		public void Start()
		{
			Logger.Instance.Trace(this, "start scheduling");

			List<ScheduleTask>.Enumerator enumerator = _tasks.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ScheduleTask task = enumerator.Current;
					task.Start();
				}
			}
			catch (Exception e)
			{
				Logger.Instance.Error(this, e);
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		public void Stop()
		{
			Logger.Instance.Trace(this, "stop scheduling");
			List<ScheduleTask>.Enumerator enumerator = _tasks.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					ScheduleTask taskThread = enumerator.Current;
					taskThread.Dispose();
				}
			}
			catch (Exception e)
			{
				Logger.Instance.Error(this, e);
			}
			finally
			{
				enumerator.Dispose();
			}
			_tasks.Clear();
		}

		#endregion

	}
}