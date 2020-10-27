#region Declaim
//---------------------------------------------------------------------------
// Name:		Type
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年7月9日
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
	/// WMM交易类型 
	/// </summary>
	public enum WmmTranTypeConstants
    {
		[Description("物料入库")]		
		Inbound = 10 ,
		[Description("物料出库")]		
		Outbound = 20 ,
		[Description("撤销入库")]		
		UndoInbound = 11 ,
		[Description("撤销出库")]		
		UndoOutbound = 21 ,
		[Description("物料冻结")]		
		MaterialFreezing = 30 ,
		[Description("状态冻结")]		
		StateFreezing = 40 ,
		[Description("物料解冻")]		
		MaterialThawing = 50 ,
		[Description("状态解冻")]		
		StateThawing = 60 ,
		[Description("不产生交易")]		
		None = 0 ,
		[Description("冻结移动")]		
		FrozenMovement = 70 ,
		[Description("物料移库")]		
		Movement = 80 ,
		[Description("无价值物料入库")]		
		WorthlessInbound = 90 ,
		[Description("冻结入库")]		
		FrozenInbound = 100 ,
		[Description("冻结出库")]		
		FrozenOutbound = 110 ,
	}
}
