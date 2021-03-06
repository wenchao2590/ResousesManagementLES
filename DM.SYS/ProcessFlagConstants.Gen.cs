#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018-07-23
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
	/// 处理标记 
	/// </summary>
	public enum ProcessFlagConstants
    {
		[Description("逆处理")]		
		ConverseProgress = 40 ,
		[Description("未处理")]		
		Untreated = 10 ,
		[Description("已处理")]		
		Processed = 20 ,
		[Description("挂起")]		
		Suspend = 30 ,
		[Description("取消")]		
		Cancel = 90 ,
		[Description("已重发")]		
		Resend = 50 ,
	}
}
