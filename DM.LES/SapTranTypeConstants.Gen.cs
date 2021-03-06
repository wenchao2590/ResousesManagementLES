#region Declaim
//---------------------------------------------------------------------------
// Name:		Type
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年6月5日
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
	/// SAP物料移动类型 
	/// </summary>
	public enum SapTranTypeConstants
    {
		[Description("GR 收货-整车")]		
		Inbound = 101 ,
		[Description("TF 厂内移储")]		
		Movement = 311 ,
		[Description("免费交货")]		
		RebateInbound = 511 ,
	}
}
