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
    public partial class OutboundTypeBLL
    {
        /// <summary>
        /// CodeItemDAL
        /// </summary>
        CodeItemDAL dal = new CodeItemDAL();
        /// <summary>
        /// 出仓类别
        /// OUTBOUND_TYPE
        /// </summary>
        public string codeFid = "D3A126DC-E622-4760-8517-43C286E959BC";
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<OutboundTypeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            textWhere += " and [CODE_FID] = N'" + codeFid + "'";
            dataCount = dal.GetCounts(textWhere);
            List<CodeItemInfo> codeItemInfos = dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            List<OutboundTypeInfo> outboundTypeInfos = new List<OutboundTypeInfo>();
            foreach (var codeItemInfo in codeItemInfos)
            {
                OutboundTypeInfo outboundTypeInfo = new OutboundTypeInfo();
                outboundTypeInfo.Id = codeItemInfo.Id;
                outboundTypeInfo.ItemValue = codeItemInfo.ItemValue.GetValueOrDefault();
                outboundTypeInfo.ItemName = codeItemInfo.ItemName;
                outboundTypeInfo.Comments = codeItemInfo.Comments;
                outboundTypeInfo.ValidFlag = codeItemInfo.ValidFlag.GetValueOrDefault();
                outboundTypeInfo.CreateUser = codeItemInfo.CreateUser;
                outboundTypeInfo.CreateDate = codeItemInfo.CreateDate.GetValueOrDefault();
                outboundTypeInfos.Add(outboundTypeInfo);
            }
            return outboundTypeInfos;
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InsertInfo(OutboundTypeInfo info)
        {
            int cnt = dal.GetCounts("[ITEM_NAME] = N'" + info.ItemName + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000120");///出仓类型不能重复

            string itemValue = new SeqDefineDAL().GetCurrentCode("OUTBOUND_TYPE_VALUE");
            cnt = dal.GetCounts("[ITEM_VALUE] = " + itemValue + " and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0 || string.IsNullOrEmpty(itemValue))
                throw new Exception("MC:0x00000120");///出仓类型不能重复

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
                throw new Exception("MC:0x00000120");///出仓类型不能重复

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
        public OutboundTypeInfo SelectInfo(long id)
        {
            CodeItemInfo codeItemInfo = dal.GetInfo(id);
            if (codeItemInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            OutboundTypeInfo outboundTypeInfo = new OutboundTypeInfo();
            outboundTypeInfo.Id = codeItemInfo.Id;
            outboundTypeInfo.ItemValue = codeItemInfo.ItemValue.GetValueOrDefault();
            outboundTypeInfo.ItemName = codeItemInfo.ItemName;
            outboundTypeInfo.Comments = codeItemInfo.Comments;
            outboundTypeInfo.ValidFlag = codeItemInfo.ValidFlag.GetValueOrDefault();
            outboundTypeInfo.CreateUser = codeItemInfo.CreateUser;
            outboundTypeInfo.CreateDate = codeItemInfo.CreateDate.GetValueOrDefault();
            return outboundTypeInfo;
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
