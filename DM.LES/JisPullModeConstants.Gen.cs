#region Declaim
//---------------------------------------------------------------------------
// Name:		Mode
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年7月10日
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
	/// 排序拉动模式 
	/// </summary>
	public enum JisPullModeConstants
    {
		[Description("排序拉动")]		
		JisPull = 10 ,
		[Description("物料成套拉动")]		
		CompletePull = 20 ,
	}
}
