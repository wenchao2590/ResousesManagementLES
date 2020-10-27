using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Infrustructure.Print
{
    /// <summary>
    /// 根据JIS配置的打印参数，返回对应字体大小
    /// </summary>
    public class PrintHelper
    {
        const string DEFAULT_FONTSIZE = "20pt";

        static List<JISPrintConfigInfo> _JisPlantConfigList = null;

        public static string GetFontSize(int rowNumber, int totalRecords)
        {
            int pageSize = 10;

            try
            {
                if (!EventLog.SourceExists("Foton LES ReportService Error"))
                    EventLog.CreateEventSource("Foton LES ReportService Error", "Application");

                EventLog.WriteEntry("Foton LES ReportService Error", "1111");

                int pageRecords = 0;

                int totalPages = (totalRecords / pageSize + 1);

                int currentPage = ((rowNumber - 1) / pageSize + 1);

                if (currentPage < totalPages)
                    pageRecords = pageSize;
                else
                    pageRecords = totalRecords - (currentPage - 1) * pageSize;

                var dal = new JISPrintConfigDAL();

                EventLog.WriteEntry("Foton LES ReportService Error", "2222");

                if (_JisPlantConfigList == null || _JisPlantConfigList.Count == 0)
                    _JisPlantConfigList = dal.GetList();

                foreach (JISPrintConfigInfo pci in _JisPlantConfigList)
                {
                    if (pageRecords >= pci.StartRow && pageRecords <= pci.EndRow)
                    {
                        EventLog.WriteEntry("Foton LES ReportService Error", pci.FontSize);
                        return pci.FontSize;
                    }
                }
                EventLog.WriteEntry("Foton LES ReportService Error", "3333");
                
                return DEFAULT_FONTSIZE;
            }
            catch (System.Exception ex)
            {
                EventLog.WriteEntry("Foton LES ReportService Error", ex.InnerException.Message);

                throw new System.Exception("异常信息:" + ex.InnerException.Message + "..." + ex.Message);

            }
        }
    }
}
