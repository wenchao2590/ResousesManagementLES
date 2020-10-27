#region

using System;
using System.Collections.Generic;

#endregion

namespace Infrustructure.Net
{
	/// <summary>
	/// Stores a list of files and directories from an FTP result
	/// </summary>
	/// <remarks></remarks>
	public class FtpDirectoryInfo : List<FtpFileInfo>
	{
		private const char slash = '/';

		public FtpDirectoryInfo()
		{
			//creates a blank directory listing
		}

		/// <summary>
		/// Constructor: create list from a (detailed) directory string
		/// </summary>
		/// <param name="dir">directory listing string</param>
		/// <param name="path"></param>
		/// <remarks></remarks>
		public FtpDirectoryInfo(string dir, string path)
		{
			foreach (string line in dir.Replace("\n", "").Split(Convert.ToChar('\r')))
			{
				//parse
				if (line != "")
					Add(new FtpFileInfo(line, path));
			}
		}

		/// <summary>
		/// Filter out only files from directory listing
		/// </summary>
		/// <param name="ext">optional file extension filter</param>
		/// <returns>FTPdirectory listing</returns>
		public FtpDirectoryInfo GetFiles(string ext)
		{
			return GetFileOrDir(FtpFileInfo.DirectoryEntryTypes.File, ext);
		}

		/// <summary>
		/// Returns a list of only subdirectories
		/// </summary>
		/// <returns>FTPDirectory list</returns>
		/// <remarks></remarks>
		public FtpDirectoryInfo GetDirectories()
		{
			return GetFileOrDir(FtpFileInfo.DirectoryEntryTypes.Directory, "");
		}

		//internal: share use function for GetDirectories/Files
		private FtpDirectoryInfo GetFileOrDir(FtpFileInfo.DirectoryEntryTypes type, string ext)
		{
			FtpDirectoryInfo result = new FtpDirectoryInfo();
			foreach (FtpFileInfo fi in this)
			{
				if (fi.FileType == type)
					if (ext == "")
						result.Add(fi);
					else if (ext == fi.Extension)
						result.Add(fi);
			}
			return result;
		}

		public bool FileExists(string filename)
		{
			foreach (FtpFileInfo ftpfile in this)
			{
				if (ftpfile.Filename == filename)
					return true;
			}
			return false;
		}

		public static string GetParentDirectory(string dir)
		{
			string tmp = dir.TrimEnd(slash);
			int i = tmp.LastIndexOf(slash);
			if (i > 0)
				return tmp.Substring(0, i - 1);
			throw (new ApplicationException("No parent for root"));
		}
	}
}