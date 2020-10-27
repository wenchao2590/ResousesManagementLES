using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// CodeItemBLL
    /// </summary>
    public class CodeItemBLL
    {
        #region Common
        /// <summary>
        /// CodeItemDAL
        /// </summary>
        CodeItemDAL dal = new CodeItemDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<CodeItemInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// 批量获取代码名称对应的枚举项
        /// </summary>
        /// <param name="codeNames"></param>
        /// <returns></returns>
        public List<CodeItemInfo> GetListByCodeNames(List<string> codeNames)
        {
            if (codeNames.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<CodeInfo> codeInfos = new CodeDAL().GetList("[CODE_NAME] in ('" + string.Join("','", codeNames.ToArray()) + "')", string.Empty);
            if (codeInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            List<CodeItemInfo> codeItemInfos = dal.GetList("[CODE_FID] in ('" + string.Join("','", codeInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            foreach (var codeItemInfo in codeItemInfos)
            {
                CodeInfo codeInfo = codeInfos.FirstOrDefault(d => d.Fid.GetValueOrDefault() == codeItemInfo.CodeFid.GetValueOrDefault());
                if (codeInfo == null) continue;
                codeItemInfo.CodeName = codeInfo.CodeName;
            }
            return codeItemInfos;
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(CodeItemInfo info)
        {
            int cnt = dal.GetCounts("[CODE_FID] = N'" + info.CodeFid.GetValueOrDefault() + "' and [ITEM_VALUE] = " + info.ItemValue.GetValueOrDefault() + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000009");///不允许相同代码出现在同一系统代码集中

            cnt = dal.GetCounts("[CODE_FID] = N'" + info.CodeFid.GetValueOrDefault() + "' and [ITEM_NAME] = N'" + info.ItemName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000009");///不允许相同代码出现在同一系统代码集中

            cnt = dal.GetCounts("[CODE_FID] = N'" + info.CodeFid.GetValueOrDefault() + "' and [ITEM_NAME_EN] = N'" + info.ItemNameEn + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000009");///不允许相同代码出现在同一系统代码集中

            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string codeFid = CommonBLL.GetFieldValue(fields, "CODE_FID");
            string itemValue = CommonBLL.GetFieldValue(fields, "ITEM_VALUE");
            if (string.IsNullOrEmpty(itemValue))
                throw new Exception("MC:0x00000004");///系统代码项的值不允许为空
            int cnt = dal.GetCounts("[ID] <> " + id + " and [CODE_FID] = N'" + codeFid + "' and [ITEM_VALUE] = " + int.Parse(itemValue) + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000009");///不允许相同代码出现在同一系统代码集中

            string itemName = CommonBLL.GetFieldValue(fields, "ITEM_NAME");
            cnt = dal.GetCounts("[ID] <> " + id + " and [CODE_FID] = N'" + codeFid + "' and [ITEM_NAME] = N'" + itemName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000009");///不允许相同代码出现在同一系统代码集中

            string itemNameEn = CommonBLL.GetFieldValue(fields, "ITEM_NAME_EN");
            cnt = dal.GetCounts("[ID] <> " + id + " and [CODE_FID] = N'" + codeFid + "' and [ITEM_NAME_EN] = N'" + itemNameEn + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000009");///不允许相同代码出现在同一系统代码集中

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CodeItemInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// 根据CodeName和ItemName获取对应的值
        /// </summary>
        /// <param name="codeName"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public int GetValueByCodeItemName(string codeName, string itemName)
        {
            return dal.GetValueByCodeItemName(codeName, itemName);
        }
    }
}
