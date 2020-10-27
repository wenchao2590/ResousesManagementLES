﻿#region

using System;
using System.Text.RegularExpressions;

#endregion

namespace Infrustructure.Net
{
	/// <summary>
	/// Represents a file or directory entry from an FTP listing
	/// </summary>
	/// <remarks>
	/// This class is used to parse the results from a detailed
	/// directory list from FTP. It supports most formats of
	/// </remarks>
	public class FtpFileInfo
	{
		#region "Properties"

		private readonly string _filename;
		private readonly DirectoryEntryTypes _fileType;
		private readonly string _path;
		private readonly string _permission;
		private readonly long _size;
		private DateTime _fileDateTime;

		public string FullName
		{
			get { return Path + Filename; }
		}

		public string Filename
		{
			get { return _filename; }
		}

		/// <summary>
		/// Path of file (always ending in /)
		/// </summary>
		/// <remarks>
		/// 1.1: Modifed to ensure always ends in / - with thanks to jfransella for pointing this out
		/// </remarks>
		public string Path
		{
			get { return _path + (_path.EndsWith("/") ? "" : "/"); }
		}

		public DirectoryEntryTypes FileType
		{
			get { return _fileType; }
		}

		public long Size
		{
			get { return _size; }
		}

		public DateTime FileDateTime
		{
			get { return _fileDateTime; }
			internal set { _fileDateTime = value; }
		}

		public string Permission
		{
			get { return _permission; }
		}

		public string Extension
		{
			get
			{
				int i = Filename.LastIndexOf(".");
				if (i >= 0 && i < (Filename.Length - 1))
					return Filename.Substring(i + 1);
				return "";
			}
		}

		public string NameOnly
		{
			get
			{
				int i = Filename.LastIndexOf(".");
				if (i > 0)
					return Filename.Substring(0, i);
				return Filename;
			}
		}

		#endregion

		#region DirectoryEntryTypes enum

		/// <summary>
		/// Identifies entry as either File or Directory
		/// </summary>
		public enum DirectoryEntryTypes
		{
			File,
			Directory
		}

		#endregion

		/// <summary>
		/// Constructor taking a directory listing line and path
		/// </summary>
		/// <param name="line">The line returned from the detailed directory list</param>
		/// <param name="path">Path of the directory</param>
		/// <remarks></remarks>
		public FtpFileInfo(string line, string path)
		{
			//parse line
			Match m = GetMatchingRegex(line);
			if (m == null)
				//failed
				throw (new ApplicationException("Unable to parse line: " + line));

			_filename = m.Groups["name"].Value;
			_path = path;

			Int64.TryParse(m.Groups["size"].Value, out _size);
			//_size = System.Convert.ToInt32(m.Groups["size"].Value);

			_permission = m.Groups["permission"].Value;
			string _dir = m.Groups["dir"].Value;
			if (_dir != "" && _dir != "-")
				_fileType = DirectoryEntryTypes.Directory;
			else
				_fileType = DirectoryEntryTypes.File;

			try
			{
				_fileDateTime = DateTime.Parse(m.Groups["timestamp"].Value);
			}
			catch (Exception)
			{
				_fileDateTime = Convert.ToDateTime(null);
			}
			
		}

		private static Match GetMatchingRegex(string line)
		{
			Regex rx;
			Match m;
			for (int i = 0; i <= _ParseFormats.Length - 1; i++)
			{
				rx = new Regex(_ParseFormats[i]);
				m = rx.Match(line);
				if (m.Success)
					return m;
			}
			return null;
		}

		#region "Regular expressions for parsing LIST results"

		/// <summary>
		/// List of REGEX formats for different FTP server listing formats
		/// </summary>
		/// <remarks>
		/// The first three are various UNIX/LINUX formats, fourth is for MS FTP
		/// in detailed mode and the last for MS FTP in 'DOS' mode.
		/// I wish VB.NET had support for Const arrays like C# but there you go
		/// </remarks>
        private static readonly string[] _ParseFormats = new string[]
		                                                 	{
		                                                 		"(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)"
		                                                 		,
		                                                 		"(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{4})\\s+(?<name>.+)"
		                                                 		,
		                                                 		"(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\d+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)"
		                                                 		,
		                                                 		"(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})\\s+\\d+\\s+\\w+\\s+\\w+\\s+(?<size>\\d+)\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{1,2}:\\d{2})\\s+(?<name>.+)"
		                                                 		,
		                                                 		"(?<dir>[\\-d])(?<permission>([\\-r][\\-w][\\-xs]){3})(\\s+)(?<size>(\\d+))(\\s+)(?<ctbit>(\\w+\\s\\w+))(\\s+)(?<size2>(\\d+))\\s+(?<timestamp>\\w+\\s+\\d+\\s+\\d{2}:\\d{2})\\s+(?<name>.+)"
		                                                 		,
		                                                 		"(?<timestamp>\\d{2}\\-\\d{2}\\-\\d{2}\\s+\\d{2}:\\d{2}[Aa|Pp][mM])\\s+(?<dir>\\<\\w+\\>){0,1}(?<size>\\d+){0,1}\\s+(?<name>.+)"
		                                                 	};

		#endregion
	}
}