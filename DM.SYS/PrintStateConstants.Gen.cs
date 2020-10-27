#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年4月10日
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
	/// 打印状态 
	/// </summary>
	public enum PrintStateConstants
    {
		[Description("未打印")]		
		NOT_TO_PRINT = 10 ,
		[Description("已打印")]		
		HAVE_TO_PRINT = 20 ,
		[Description("已关闭")]		
		CLOSED = 30 ,
	}
}
