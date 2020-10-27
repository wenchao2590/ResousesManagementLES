using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{

    /// <summary>
    /// 物料主数据
    /// </summary>
    [XmlRoot("PlantStructure")]
    public class BFDASapPlantStructureInfo
    {
        public string WERKS;///工厂
		public string NAME1;///工厂描述
		public string ZBM;///部门
		public string ZBMMS;///部门描述
		public string ZCJ;///车间
		public string ZCJMS;///车间描述
		public string LINE_NO;///生产线
		public string LINE_NOMS;///生产线描述
		public string VLSCH;///工位
		public string TXT;///工位描述
		public string ZSX;///顺序
    }
}