#region Declaim
//---------------------------------------------------------------------------
// Name:		SourceDataType
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年7月16日
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
	/// TWD计数器更新数据来源 
	/// </summary>
	public enum TwdCounterSourceDataTypeConstants
    {
		[Description("状态点")]		
		StatePoint = 10 ,
		[Description("库存")]		
		Inventory = 20 ,
		[Description("计数器")]		
		Calculator = 30 ,
		[Description("手工")]		
		Manual = 40 ,
		[Description("组单")]		
		CreateRunsheet = 50 ,
        [Description("提前拉动")]
        AdvancePull = 60,
    }
}