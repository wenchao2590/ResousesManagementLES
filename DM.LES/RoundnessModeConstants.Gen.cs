#region Declaim
//---------------------------------------------------------------------------
// Name:		
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
	/// 圆整方式 
	/// </summary>
	public enum RoundnessModeConstants
    {
		[Description("按需")]		
		Ondemand = 10 ,
		[Description("向上")]		
		Upward = 20 ,
		[Description("向下")]		
		Downward = 30 ,
	}
}
