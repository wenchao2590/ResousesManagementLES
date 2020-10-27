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
    public partial class PlaceOfOriginBLL
    {
        /// <summary>
        /// CodeItemDAL
        /// </summary>
        CodeItemDAL dal = new CodeItemDAL();
        /// <summary>
        /// PLACE_OF_ORIGIN
        /// 产地
        /// </summary>
        public string codeFid = "7848F682-E2FA-43E2-9B38-9DD02938F29F";
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PlaceOfOriginInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            textWhere += " and [CODE_FID] = N'" + codeFid + "'";
            dataCount = dal.GetCounts(textWhere);
            List<CodeItemInfo> codeItemInfos = dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            List<PlaceOfOriginInfo> placeOfOriginInfos = new List<PlaceOfOriginInfo>();
            foreach (var codeItemInfo in codeItemInfos)
            {
                PlaceOfOriginInfo placeOfOriginInfo = new PlaceOfOriginInfo();
                placeOfOriginInfo.Id = codeItemInfo.Id;
                placeOfOriginInfo.ItemNameEn = codeItemInfo.ItemNameEn;
                placeOfOriginInfo.ItemName = codeItemInfo.ItemName;
                placeOfOriginInfo.Comments = codeItemInfo.Comments;
                placeOfOriginInfo.ValidFlag = codeItemInfo.ValidFlag.GetValueOrDefault();
                placeOfOriginInfo.CreateUser = codeItemInfo.CreateUser;
                placeOfOriginInfo.CreateDate = codeItemInfo.CreateDate.GetValueOrDefault();
                placeOfOriginInfos.Add(placeOfOriginInfo);
            }
            return placeOfOriginInfos;
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool InsertInfo(PlaceOfOriginInfo info)
        {
            int cnt = dal.GetCounts("[ITEM_NAME] = N'" + info.ItemName + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000085");///名称不能重复
            cnt = dal.GetCounts("[ITEM_NAME_EN] = N'" + info.ItemName + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000086");///代码不能重复
            CodeItemInfo codeItemInfo = new CodeItemInfo();
            codeItemInfo.Fid = Guid.NewGuid();
            codeItemInfo.ItemNameEn = info.ItemNameEn;
            codeItemInfo.ItemName = info.ItemName;
            codeItemInfo.Comments = info.Comments;
            codeItemInfo.CodeFid = Guid.Parse(codeFid);
            codeItemInfo.ValidFlag = true;
            codeItemInfo.CreateUser = info.CreateUser;
            codeItemInfo.CreateDate = info.CreateDate;
            return dal.Add(codeItemInfo) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string itemName = CommonBLL.GetFieldValue(fields, "ITEM_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ITEM_NAME] = N'" + itemName + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000085");///名称不能重复

            string itemNameEn = CommonBLL.GetFieldValue(fields, "ITEM_NAME_EN");
            cnt = dal.GetCounts("[ID] <> " + id + " and [ITEM_NAME_EN] = N'" + itemNameEn + "' and [CODE_FID] = N'" + codeFid + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000086");///代码不能重复

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            int cnt = new MaintainPartsDAL().GetCounts("[ORIGIN_PLACE] in (select [ITEM_NAME_EN] from dbo.[TS_SYS_CODE_ITEM] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000125");///产地已在材料档案中出现，不能删除

            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlaceOfOriginInfo SelectInfo(long id)
        {
            CodeItemInfo codeItemInfo = dal.GetInfo(id);
            if (codeItemInfo == null)
                throw new Exception("MC:0x00000084");///数据错误
            PlaceOfOriginInfo placeOfOriginInfo = new PlaceOfOriginInfo();
            placeOfOriginInfo.Id = codeItemInfo.Id;
            placeOfOriginInfo.ItemNameEn = codeItemInfo.ItemNameEn;
            placeOfOriginInfo.ItemName = codeItemInfo.ItemName;
            placeOfOriginInfo.Comments = codeItemInfo.Comments;
            placeOfOriginInfo.ValidFlag = codeItemInfo.ValidFlag.GetValueOrDefault();
            placeOfOriginInfo.CreateUser = codeItemInfo.CreateUser;
            placeOfOriginInfo.CreateDate = codeItemInfo.CreateDate.GetValueOrDefault();
            return placeOfOriginInfo;
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
