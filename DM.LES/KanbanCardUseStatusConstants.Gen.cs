#region Declaim
//---------------------------------------------------------------------------
// Name:		UseStatus
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年5月31日
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
	/// 看板卡使用状态 
	/// </summary>
	public enum KanbanCardUseStatusConstants
    {
		[Description("未使用")]		
		NotUsed = 10 ,
		[Description("已扫描")]		
		Scaned = 20 ,
		[Description("已出库")]		
		Outbound = 30 ,
		[Description("已回库")]		
		Reback = 50 ,
		[Description("使用中")]		
		Using = 40 ,
	}
}
