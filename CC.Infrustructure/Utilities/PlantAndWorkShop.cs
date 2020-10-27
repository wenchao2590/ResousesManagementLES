using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Infrustructure.Logging;

namespace Infrustructure.Utilities
{
    public class PlantAndWorkShop
    {
        //获取工作时间合法性配置文件的工厂编号
        public static string plantNo()
        {
            try
            {
                return AppSettings.ReadPlantXml(AppConst.directoryAppConfig, AppConst.PlantAndWorkshop, "plant", "id");
            }
            catch (System.Exception ex)
            {
                Logger.LogError("PlantAndWorkShop", "plantNo", "PlantAndWorkShop", ex.ToString(), ex);
                throw;
            }
        }
        //获取工作时间合法性配置文件的数据库连接字符串
        public static string PCMConnStr()
        {
            try
            {
                return AppSettings.GetDatabaseConnString("PMC").ToString();
            }
            catch (System.Exception ex)
            {
                Logger.LogError("PlantAndWorkShop", "PCMConnStr", "PlantAndWorkShop", ex.ToString(), ex);
                throw;
            }

        }
        //获取工作时间合法性配置文件的车间ID
        public static string getPMCZoneID(string WorkshopID)
        {
            try
            {
                Hashtable hs = new Hashtable();
                hs = AppSettings.ReadXml(AppConst.directoryAppConfig, AppConst.PlantAndWorkshop, "workshop");
                return hs[WorkshopID].ToString();
            }
            catch (System.Exception ex)
            {
                Logger.LogError("PlantAndWorkShop", "getPMCZoneID", "PlantAndWorkShop", ex.ToString(), ex);
                throw;
            }
        }

        //获取工作时间合法性配置文件的车间ID
        public static string getPMCZoneID(DataTable dt, string Column, string filterValue, string WorkshopID)
        {
            try
            {
                WorkshopID = WorkshopID.ToLower();
                Hashtable hs = new Hashtable();
                hs = AppSettings.GetHashtableFromDatatable(dt, Column, filterValue);
                if (hs[WorkshopID] != null)
                    return hs[WorkshopID].ToString();
                else
                    return "";
            }
            catch (System.Exception ex)
            {
                Logger.LogError("PlantAndWorkShop", "getPMCZoneID", "PlantAndWorkShop", ex.ToString(), ex);
                throw;
            }
        }

        public static void SaveConnStr(string directoryName, string fileName, DataGridView dgv, string ElementName, out string returnInfo)
        {
            AppSettings.SaveConnStr(directoryName, fileName, dgv, ElementName, out returnInfo);
        }
    }
}
