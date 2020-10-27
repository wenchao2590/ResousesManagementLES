using System;
using System.IO;
using Infrustructure.Utilities;
using Infrustructure.Logging;

namespace Infrustructure.Scheduling
{
	public class TmpFilesCleanupTask : ScheduleTask
	{
        public override void DoTask()
		{
			string s1 = OtherAttributes["folder"] as string;
			if(string.IsNullOrEmpty(s1))
			{
				Logger.Instance.Info(this, string.Format("没有定义folder的值, 它应该是用','隔开的一系列目录路径，本次操作取消执行"));
				return;
			}
        	string s2 = OtherAttributes["expiredays"] as string;
			int expireDays = 7;
			if(!string.IsNullOrEmpty(s2))
			{
				if(!int.TryParse(s2, out expireDays))
				{
					expireDays = 7;
					Logger.Instance.Info(this, string.Format("无法将过期天数expiredays所定义的值 [{0}] 解析为整数, 本操作将使用默认7",s2));
				}
			}
			else
				Logger.Instance.Info(this, string.Format("没有设置过期天数expiredays, 本操作将使用默认7"));

			foreach(string folder in s1.Split(';'))
			{
				if (string.IsNullOrEmpty(folder))
					continue;
                string folder1 = MiscUtil.ResolveFolderPath(folder);
				if(string.IsNullOrEmpty(folder1))
				{
					Logger.Instance.Info(this, string.Format("目录路径[{0}]无效", folder));
					continue;
				}
				string[] files = Directory.GetFiles(folder1);
				
				foreach(string file in files)
				{
					DateTime d = File.GetLastAccessTime(file);
					if ((DateTime.Now - d).Days >= expireDays)
					{
						try
						{
							File.Delete(file);
						}
						catch
						{
							// just eating the error and waiting the next time trying.
						}
					}
				}
			}
			
		}
	}
}
