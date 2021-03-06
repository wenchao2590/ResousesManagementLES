#region Declaim
//---------------------------------------------------------------------------
// Name:		State
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年6月4日
// <auto-generated>
//     This code was generated by a tool. 
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion 
using System.ComponentModel;

namespace DM.LES 
{
	/// <summary>
	/// 发送数据返回状态 
	/// </summary>
	public enum InboundReturnStateConstants
    {
		[Description("传输成功")]		
		SUCCEED = 1 ,
		[Description("传输失败")]		
		FAILURE = 0 ,
	}
}
