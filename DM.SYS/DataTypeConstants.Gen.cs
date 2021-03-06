#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年4月3日
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
	/// 数据类型 
	/// </summary>
	public enum DataTypeConstants
    {
		[Description("string")]		
		STRING = 10 ,
		[Description("int")]		
		INT = 20 ,
		[Description("bool")]		
		BOOL = 30 ,
		[Description("datetime")]		
		DATETIME = 40 ,
		[Description("date")]		
		DATE = 50 ,
		[Description("decimal")]		
		DECIMAL = 60 ,
	}
}
