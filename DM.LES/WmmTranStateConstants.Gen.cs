#region Declaim
//---------------------------------------------------------------------------
// Name:		State
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年5月1日
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
	/// 仓储交易记录状态 
	/// </summary>
	public enum WmmTranStateConstants
    {
		[Description("未处理")]		
		Created = 10 ,
		[Description("已处理")]		
		Done = 20 ,
	}
}
