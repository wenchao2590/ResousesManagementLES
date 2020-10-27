using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BLL.LES
{
    public partial class InboundTypeBLL
    {
        /// <summary>
        /// CodeItemDAL
        /// </summary>
        CodeItemDAL dal = new CodeItemDAL();
        /// <summary>
        /// 进仓类别
        /// INBOUND_TYPE
        /// SIOM = 67FA6B26-5C2D-4C26-B882-DFCB2B07FB6B(TODO：需要修改为统一的FID)
        /// BFDA = E71E90A7-C157-4FAD-9D17-AD9B210AA5AF
        /// </summary>
        public string codeFid = "E71E90A7-C157-4FAD-9D17-AD9B210AA5AF";
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<InboundTypeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            textWhere += " and [CODE_FID] = N'" + codeFid + "'";
            dataCount = dal.GetCounts(textWhere);
            List<CodeItemInfo> codeItemInfos = dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            List<InboundTypeInfo> inboundTypeInfos = new List<InboundTypeInfo>();
            foreach (var codeItemInfo in codeItemInfos)
            {
                InboundTypeInfo inboundTypeInfo = new InboundTypeInfo();
                inboundTypeInfo.Id = codeItemInfo.Id;
                inboundTypeInfo.ItemValue = codeItemInfo.ItemValue.GetValueOrDefault();
                inboundTypeInfo.ItemName = codeItemInfo.ItemName;
                inboundTypeInfo.Comments = codeItemInfo.Comments;
                inboundTypeInfo.ValidFlag = codeItemInfo.ValidFlag.GetValueOrDefault();
                inboundTypeInfo.CreateUser = codeItemInfo.CreateUser;
                inboundTypeInfo.CreateDate = codeItemInfo.CreateDate.GetValueOrDefault();
                inboundTypeInfos.Add(inboundTypeInfo);
            }
            return inboundTypeInfos;
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InsertInfo(InboundTypeInfo info)
        {
            int cnt = dal.GetCounts("[ITEM_NAME] = N'" + info.ItemName + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000119");///进仓类型不能重复            

            string itemValue = new SeqDefineDAL().GetCurrentCode("INBOUND_TYPE_VALUE");
            cnt = dal.GetCounts("[ITEM_VALUE] = " + itemValue + " and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0 || string.IsNullOrEmpty(itemValue))
                throw new Exception("MC:0x00000119");///进仓类型不能重复

            CodeItemInfo codeItemInfo = new CodeItemInfo();
            codeItemInfo.Fid = Guid.NewGuid();
            codeItemInfo.ItemValue = int.Parse(itemValue);
            codeItemInfo.ItemName = info.ItemName;
            codeItemInfo.Comments = info.Comments;
            codeItemInfo.CodeFid = Guid.Parse(codeFid);
            codeItemInfo.ValidFlag = true;
            codeItemInfo.CreateUser = info.CreateUser;
            codeItemInfo.CreateDate = info.CreateDate;
            return dal.Add(codeItemInfo) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string itemName = CommonBLL.GetFieldValue(fields, "ITEM_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ITEM_NAME] = N'" + itemName + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000119");///进仓类型不能重复

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InboundTypeInfo SelectInfo(long id)
        {
            CodeItemInfo codeItemInfo = dal.GetInfo(id);
            if (codeItemInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            InboundTypeInfo inboundTypeInfo = new InboundTypeInfo();
            inboundTypeInfo.Id = codeItemInfo.Id;
            inboundTypeInfo.ItemValue = codeItemInfo.ItemValue.GetValueOrDefault();
            inboundTypeInfo.ItemName = codeItemInfo.ItemName;
            inboundTypeInfo.Comments = codeItemInfo.Comments;
            inboundTypeInfo.ValidFlag = codeItemInfo.ValidFlag.GetValueOrDefault();
            inboundTypeInfo.CreateUser = codeItemInfo.CreateUser;
            inboundTypeInfo.CreateDate = codeItemInfo.CreateDate.GetValueOrDefault();
            return inboundTypeInfo;
        }

        /// <summary>
        /// 获取EXCEL所用选项
        /// </summary>
        /// <param name="idField"></param>
        /// <param name="textField"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetComboxItems(string idField, string textField)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            List<CodeItemInfo> codeItemInfos = dal.GetList("[CODE_FID] = N'" + codeFid + "'", "[DISPLAY_ORDER]");
            foreach (CodeItemInfo codeItemInfo in codeItemInfos)
            {
                PropertyInfo prop = codeItemInfo.GetType().GetProperty(idField);
                if (prop == null) continue;
                object objId = prop.GetValue(codeItemInfo, null);
                if (objId == null) continue;
                prop = codeItemInfo.GetType().GetProperty(textField);
                if (prop == null) continue;
                object objText = prop.GetValue(codeItemInfo, null);
                if (objText == null) continue;
                keyValuePairs.Add(objId.ToString(), objText.ToString());
            }
            return keyValuePairs;
        }
    }
}
