#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年4月8日
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
	/// 基础数据状态 
	/// </summary>
	public enum DataStatusConstants
    {
		[Description("已创建")]		
		CREATED = 10 ,
		[Description("已启用")]		
		ENABLED = 20 ,
		[Description("已作废")]		
		INVALID = 30 ,
	}
}