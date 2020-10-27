using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP 库存盘点计划 (报文表)SAP-LES-011
    /// TI_IFM_SAP_inventory_CHEK_PLAN
    /// </summary>
    public class BFDASapInventoryCheckPlanInfo
    {

        public string IBLNR; ///CHAR(10)      盘点凭证号 		
        public string WERKS; ///CHAR(4)       工厂 			
        public string MATNR; ///CHAR(18)      物料 			
        public string MENGE; ///QUAN(13)      数量 			
        public string LGORT; ///CHAR(4)       库存地点 		
        public string GIDAT; ///DATS(8)       计划盘点日期 	

    }
}