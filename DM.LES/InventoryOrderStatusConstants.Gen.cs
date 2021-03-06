#region Declaim
//---------------------------------------------------------------------------
// Name:		Status
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年6月26日
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
	/// 盘点单状态 
	/// </summary>
	public enum InventoryOrderStatusConstants
    {
		[Description("已创建")]		
		CREATED = 10 ,
		[Description("已发布")]		
		PUBLISHED = 20 ,
		[Description("已确认")]		
		CONFIRMED = 30 ,
		[Description("已完成")]		
		COMPLETED = 40 ,
		[Description("已作废")]		
		ABANDONED = 90 ,
	}
}
