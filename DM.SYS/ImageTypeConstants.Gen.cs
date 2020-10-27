#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年4月13日
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
	/// 图片资源类型 
	/// </summary>
	public enum ImageTypeConstants
    {
		[Description("页面菜单图标")]		
		MenuIcon = 10 ,
		[Description("页面按钮图标")]		
		ActionIcon = 20 ,
		[Description("首页功能图标")]		
		PageIcon = 30 ,
		[Description("图片原始文件")]		
		ResourceImageFile = 40 ,
	}
}