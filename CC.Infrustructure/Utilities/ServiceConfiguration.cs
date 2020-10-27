using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using Infrustructure;
using Infrustructure.Logging;

namespace Infrustructure.Utilities
{
    /// <summary>
    /// 检查配置文件是否存在
    /// </summary>
    public class ServiceConfiguration
    {
        public const string AppSetting_ServiceId = "ServiceId";
        public const string AppSetting_OperateFilePath = "OperateFilePath";
        public const string AppSetting_BakupFilePath = "BakupFilePath";
        public const string AppSetting_InvalidBakupFilePath = "InvalidBakupFilePath";
        public const string AppSetting_Plant = "Plant";

        public const string ConnectionSetting_String = "LES";

        /// <summary>
        /// 订单配置文件名
        /// </summary>
        protected const string InfasOrderImportServiceFileName = "YF.MES.InfasOrderImportService.exe.config";

        /// <summary>
        /// Termal配置文件名
        /// </summary>
        protected const string TGNTermalImportServiceFileName = "YF.MES.TGNTermalImportService.exe.config";

        /// <summary>
        /// 零件分解
        /// </summary>
        protected const string PartsResolveServiceFileName = "YF.MES.PartsResolveService.exe.config";

        /// <summary>
        /// Status配置文件名
        /// </summary>
        protected const string FISStatusImportServiceFileName = "YF.MES.FISStatusImportService.exe.config";

        /// <summary>
        /// FISRealTImeStatus配置文件名
        /// </summary>
        protected const string FISRealTimeStatusImportServiceFileName = "YF.MES.FISRealTimeStatusImportService.exe.config";

        /// <summary>
        /// SupplierSourceList配置文件名
        /// </summary>
        protected const string SupplierSourceListImportServiceFileName = "YF.MES.SupplierSourceListImportService.exe.config";

        /// <summary>
        /// SupplierQuota配置文件名
        /// </summary>
        protected const string SupplierQuotaImportServiceFileName = "YF.MES.SupplierQuotaImportService.exe.config";

        /// <summary>
        /// InhousePull配置文件名
        /// </summary>
        protected const string InhousePullServiceFileName = "YF.MES.InhousePullService.exe.config";

        /// <summary>
        /// InboundPull配置文件名
        /// </summary>
        protected const string InboundPullServiceFileName = "YF.MES.InboundPullService.exe.config";

        /// <summary>
        /// InhouseDiff配置文件名
        /// </summary>
        protected const string InhouseDiffServiceFileName = "YF.MES.InhouseDiffService.exe.config";

        /// <summary>
        /// InboundDif配置文件名
        /// </summary>
        protected const string InboundDiffServiceFileName = "YF.MES.InboundDiffService.exe.config";

        /// <summary>
        /// Besi配置文件名
        /// </summary>
        protected const string TWDBesiImportServiceFileName = "YF.MES.TWDBesiImportService.exe.config";

        /// <summary>
        /// Termal Diff
        /// </summary>
        protected const string TermalPBOMDiffServiceFileName = "YF.MES.TermalPBOMCombineService.exe.config";

        /// <summary>
        /// 
        /// </summary>
        protected const string SAPMRPImportServiceFileName = "YF.MES.SAPMRPImportService.exe.config";

        /// <summary>
        /// EPSSignalCollectionService
        /// </summary>
        protected const string EPSSignalCollectionServiceFileName = "YF.MES.EPSSignalCollectionService.exe.config";

        /// <summary>
        /// EPSCounter
        /// </summary>
        protected const string EPSCounterServiceFileName = "YF.MES.EPSCounterService.exe.config";

        /// <summary>
        /// JISGenReckoning
        /// </summary>
        protected const string JISGenReckoningServiceFileName = "YF.MES.JISGenReckoningService.exe.config";


        protected const string PbomImportServiceFileName = "YF.MES.PBOMImportService.exe.config";

        protected const string A100PartsResolveServiceFileName = "YF.MES.A100PartsResolveService.exe.config";

        protected static Dictionary<LESServiceType, LESService> LESCache;

        static ServiceConfiguration()
        {
            SetLESServiceCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        public static bool ServiceConfigurationChecking(LESServiceType serviceType)
        {
            //配置文件检查，如不存在写入日志.
            if (!ServiceConfigurationFileChecking(serviceType))
            {
                return false;
            }

            //检查 appsettings 节点配置
            bool result = true;
            if (!AppSettingsChecking(serviceType))
            {
                result = false;
            }

            //检查 数据库连接 配置是否正常
            if (!ConnectionStringsChecking(serviceType))
            {
                result = false;
            }

            //检查日志是否正常
            if (!LogConfigurationChecking(serviceType))
            {
                result = false;
            }

            Log.Flush();

            return result;
        }

        /// <summary>
        /// 检查Infas订单配置文件是否存在
        /// </summary>
        /// <returns></returns>
        protected static bool ServiceConfigurationFileChecking(LESServiceType serviceType)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LESCache[serviceType].ConfigurationFileName);

            if (!File.Exists(filePath))
            {
                try
                {
                    Log.WriteInfo(string.Format("【{0}】配置文件不存在【{1}】", LESCache[serviceType].ServiceName, LESCache[serviceType].ConfigurationFileName));
                    Log.Flush();
                }
                catch
                {
                }

                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取所有服务基本信息: InfasOrder,TGNTermal
        /// </summary>
        /// <returns></returns>
        protected static void SetLESServiceCache()
        {
            if (LESCache == null)
            {
                LESCache = new Dictionary<LESServiceType, LESService>();

                LESCache.Add(LESServiceType.InfasOrder, new LESService()
                {
                    ServiceType = LESServiceType.InfasOrder,
                    ServiceName = "Infas订单",
                    ConfigurationFileName = InfasOrderImportServiceFileName
                });
                LESCache.Add(LESServiceType.TGNTermal, new LESService()
                {
                    ServiceType = LESServiceType.TGNTermal,
                    ServiceName = "TGNTermal",
                    ConfigurationFileName = TGNTermalImportServiceFileName
                });
                LESCache.Add(LESServiceType.PartsResolve, new LESService()
                {
                    ServiceType = LESServiceType.PartsResolve,
                    ServiceName = "PartsResolve",
                    ConfigurationFileName = PartsResolveServiceFileName
                });
                LESCache.Add(LESServiceType.FISStatus, new LESService()
                {
                    ServiceType = LESServiceType.FISStatus,
                    ServiceName = "FISStatus",
                    ConfigurationFileName = FISStatusImportServiceFileName
                });
                LESCache.Add(LESServiceType.FISRealTimeStatus, new LESService()
                {
                    ServiceType = LESServiceType.FISRealTimeStatus,
                    ServiceName = "FISRealTimeStatus",
                    ConfigurationFileName = FISRealTimeStatusImportServiceFileName
                });
                LESCache.Add(LESServiceType.SupplierSourceList, new LESService()
                {
                    ServiceType = LESServiceType.SupplierSourceList,
                    ServiceName = "SupplierSourceList",
                    ConfigurationFileName = SupplierSourceListImportServiceFileName
                });
                LESCache.Add(LESServiceType.SupplierQuota, new LESService()
                {
                    ServiceType = LESServiceType.SupplierQuota,
                    ServiceName = "SupplierQuota",
                    ConfigurationFileName = SupplierQuotaImportServiceFileName
                });

                LESCache.Add(LESServiceType.InhousePullCombine, new LESService()
                {
                    ServiceType = LESServiceType.InhousePullCombine,
                    ServiceName = "InhousePullCombine",
                    ConfigurationFileName = InhousePullServiceFileName
                });

                LESCache.Add(LESServiceType.InboundPullCombine, new LESService()
                {
                    ServiceType = LESServiceType.InboundPullCombine,
                    ServiceName = "InboundPullCombine",
                    ConfigurationFileName = InboundPullServiceFileName
                });

                LESCache.Add(LESServiceType.TWDBesi, new LESService()
                {
                    ServiceType = LESServiceType.TWDBesi,
                    ServiceName = "TWDBesiImport",
                    ConfigurationFileName = TWDBesiImportServiceFileName
                });

                LESCache.Add(LESServiceType.InhouseDiff, new LESService()
                {
                    ServiceType = LESServiceType.InhouseDiff,
                    ServiceName = "InhouseDiff",
                    ConfigurationFileName = InhouseDiffServiceFileName
                });

                LESCache.Add(LESServiceType.InboundDiff, new LESService()
                {
                    ServiceType = LESServiceType.InboundDiff,
                    ServiceName = "InboundDiff",
                    ConfigurationFileName = InboundDiffServiceFileName
                });

                LESCache.Add(LESServiceType.TermalPBOMDiff, new LESService()
                {
                    ServiceType = LESServiceType.TermalPBOMDiff,
                    ServiceName = "TermalPBOMDiff",
                    ConfigurationFileName = TermalPBOMDiffServiceFileName
                });

                LESCache.Add(LESServiceType.Delfor, new LESService()
                {
                    ServiceType = LESServiceType.Delfor,
                    ServiceName = "Delfor",
                    ConfigurationFileName = SAPMRPImportServiceFileName
                });

                LESCache.Add(LESServiceType.EPSSignalCollection, new LESService()
                {
                    ServiceType = LESServiceType.EPSSignalCollection,
                    ServiceName = "EPSSignalCollection",
                    ConfigurationFileName = EPSSignalCollectionServiceFileName
                });

                LESCache.Add(LESServiceType.EPSCounter, new LESService()
                {
                    ServiceType = LESServiceType.EPSCounter,
                    ServiceName = "EPSCounter",
                    ConfigurationFileName = EPSCounterServiceFileName
                });

                LESCache.Add(LESServiceType.JISGenReckoning, new LESService()
                {
                    ServiceType = LESServiceType.JISGenReckoning,
                    ServiceName = "JISGenReckoning",
                    ConfigurationFileName = JISGenReckoningServiceFileName
                });

                LESCache.Add(LESServiceType.PBOM, new LESService()
                {
                    ServiceType = LESServiceType.PBOM,
                    ServiceName = "PbomImport",
                    ConfigurationFileName = PbomImportServiceFileName
                });

                LESCache.Add(LESServiceType.A100PartsResolve, new LESService()
                {
                    ServiceType = LESServiceType.A100PartsResolve,
                    ServiceName = "A100PartsResolve",
                    ConfigurationFileName = A100PartsResolveServiceFileName
                });
            }
        }

        /// <summary>
        /// 日志配置检查
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        protected static bool LogConfigurationChecking(LESServiceType serviceType)
        {
            return true;
        }

        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        protected static bool ConnectionStringsChecking(LESServiceType serviceType)
        {
            bool result = true;
            string connectionString = ConfigurationManager.ConnectionStrings["LES"].ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                Log.WriteInfo(string.Format("【{0}】服务数据库连接字浮串为空.", LESCache[serviceType].ServiceName));
                result = false;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }
            }
            catch (SqlException ex)
            {
                Log.WriteInfo(string.Format("【{0}】服务数据库连接字浮串无效,数据库连接字段【{1}】.", LESCache[serviceType].ServiceName, connectionString));
                Log.WriteInfo(ex.Message);

                return false;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        protected static bool AppSettingsChecking(LESServiceType serviceType)
        {
            bool result = BaseAppSettingChecking(serviceType);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static bool BaseAppSettingChecking(LESServiceType serviceType)
        {
            string serviceId = ConfigurationManager.AppSettings["ServiceId"];
            string operateFilePath = ConfigurationManager.AppSettings["OperateFilePath"];
            string bakupFilePath = ConfigurationManager.AppSettings["BakupFilePath"];
            string invalidBakupFilePath = ConfigurationManager.AppSettings["InvalidBakupFilePath"];

            bool result = true;

            string serviceName = LESCache[serviceType].ServiceName;

            #region 基本配置项检查 serviceType,operateFilePath,bakupFilePath

            //基本配置项serviceId检查 
            if (string.IsNullOrEmpty(serviceId))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'ServiceId'字段内容为空.", serviceName));

                result = false;
            }
            else
            {
                Regex regex = new Regex(@"^\d*$");
                if (!regex.IsMatch(serviceId.Trim()))
                {
                    Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'ServiceId'字段内容不为数字.", serviceName));
                    result = false;
                }
            }

            if (serviceType == LESServiceType.PartsResolve
                || serviceType == LESServiceType.InhousePullCombine
                || serviceType == LESServiceType.InboundPullCombine
                || serviceType == LESServiceType.InboundDiff
                || serviceType == LESServiceType.InhouseDiff
                || serviceType == LESServiceType.EPSSignalCollection
                || serviceType == LESServiceType.EPSCounter
                || serviceType == LESServiceType.JISGenReckoning
                || serviceType ==  LESServiceType.A100PartsResolve)
            {
                //零件分解时，不检查文件及文件备份目录

                return result;
            }

            //检查配置项OperateFilePath
            if (string.IsNullOrEmpty(operateFilePath))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'OperateFilePath'字段内容为空.", serviceName));

                result = false;
            }
            else if (!Directory.Exists(operateFilePath))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'OperateFilePath'目录无法访问.", serviceName));

                result = false;
            }

            //检查配置项BakupFilePath
            if (string.IsNullOrEmpty(bakupFilePath))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'BakupFilePath'字段内容为空.", serviceName));

                result = false;
            }
            else if (!Directory.Exists(bakupFilePath))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'BakupFilePath'目录无法访问.", serviceName));

                result = false;
            }

            //检查配置项InvalidBakupFilePath
            if (string.IsNullOrEmpty(bakupFilePath))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'InvalidBakupFilePath'字段内容为空.", serviceName));

                result = false;
            }
            else if (!Directory.Exists(invalidBakupFilePath))
            {
                Logger.Instance.Info(typeof(ServiceConfiguration), string.Format("【{0}】服务 'InvalidBakupFilePath'目录无法访问.", serviceName));

                result = false;
            }
            #endregion

            return result;
        }
    }

    public class LESService
    {
        public LESServiceType ServiceType;
        public string ServiceName;
        public string ConfigurationFileName;
    }

    public enum LESServiceType
    {
        InfasOrder,
        TGNTermal,
        PartsResolve,
        FISStatus,
        FISRealTimeStatus,
        SupplierSourceList,
        SupplierQuota,
        InhousePullCombine,
        InboundPullCombine,
        TWDBesi,
        InhouseDiff,
        InboundDiff,
        TermalPBOMDiff,
        Delfor,
        EPSSignalCollection,
        EPSCounter,
        JISGenReckoning,
        PBOM,
        A100PartsResolve
    }
}
