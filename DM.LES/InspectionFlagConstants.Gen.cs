#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年6月8日
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
	/// 是否免检 
	/// </summary>
	public enum InspectionFlagConstants
    {
		[Description("检验")]		
		Inspect = 1 ,
		[Description("免检")]		
		Exemption = 0 ,
	}
}
