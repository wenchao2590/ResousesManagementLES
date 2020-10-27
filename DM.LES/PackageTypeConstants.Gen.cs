#region Declaim
//---------------------------------------------------------------------------
// Name:		
// Function: 	Expose data in table TS_SYS_CODE,TS_SYS_CODE_ITEM from database as business object to system.
// Tool:		T4
// CreateDate:	2018年6月15日
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
	/// 器具类型 
	/// </summary>
	public enum PackageTypeConstants
    {
		[Description("标准塑料箱")]		
		StandardPlastic = 10 ,
		[Description("金属笼")]		
		MetalCage = 100 ,
		[Description("塑料托盘")]		
		PlasticTray = 110 ,
		[Description("循环金属桶")]		
		CirculatingMetalBucket = 120 ,
		[Description("塑料桶")]		
		PlasticBucket = 130 ,
		[Description("木质托盘")]		
		WoodenPallet = 140 ,
		[Description("一次性金属桶")]		
		DisposableMetalBucket = 150 ,
		[Description("一次性支管")]		
		OnetimeBranchPipe = 160 ,
		[Description("纸箱")]		
		Carton = 20 ,
		[Description("标准金属箱")]		
		StandardMetal = 30 ,
		[Description("金属料架")]		
		MetalFrame = 40 ,
		[Description("标准围板箱")]		
		StandardBox = 50 ,
		[Description("标准卡板箱")]		
		StandardBoard = 60 ,
		[Description("非标金属箱")]		
		NonstandardMetal = 70 ,
		[Description("非标围板箱")]		
		NonStandardBox = 80 ,
		[Description("非标卡板箱")]		
		NonStandardBoard = 90 ,
	}
}