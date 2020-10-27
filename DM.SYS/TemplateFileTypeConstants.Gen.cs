#region Declaim
//---------------------------------------------------------------------------
// Name:		Type
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年5月2日
// <auto-generated>
//     This code was generated by a tool. 
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion 
using System.ComponentModel;

namespace DM.SYS 
{
	/// <summary>
	/// 打印模板文件类型 
	/// </summary>
	public enum TemplateFileTypeConstants
    {
		[Description("xlsx")]		
		xlsx = 10 ,
		[Description("xls")]		
		xls = 20 ,
		[Description("html")]		
		html = 30 ,
	}
}
