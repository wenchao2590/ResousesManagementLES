using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// SeqSectionBLL
    /// </summary>
    public class SeqSectionBLL
    {
        #region Common
        /// <summary>
        /// SeqSectionDAL
        /// </summary>
        SeqSectionDAL dal = new SeqSectionDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SeqSectionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SeqSectionInfo info)
        {
            int cnt = dal.GetCounts("[DEFINE_FID] = N'" + info.DefineFid.GetValueOrDefault() + "' and [SECTION_SEQ] = " + info.SectionSeq.GetValueOrDefault() + "");
            if (cnt > 0)
                throw new Exception("0x00000022");///该段序号规则已维护

            if (info.DataGenerateType.GetValueOrDefault() == (int)DataGenerateTypeConstants.GrowthValue)
            {
                cnt = dal.GetCounts("[DEFINE_FID] = N'" + info.DefineFid.GetValueOrDefault() + "' and [DATA_GENERATE_TYPE] = " + (int)DataGenerateTypeConstants.GrowthValue + "");
                if (cnt > 0)
                    throw new Exception("0x00000020");///序列号规则中只能有一段为自动增长方式
                if (info.IsSeedValue.GetValueOrDefault())
                    throw new Exception("0x00000019");///自增段不能作为自增种子
            }
            else
            {
                cnt = dal.GetCounts("[DEFINE_FID] = N'" + info.DefineFid.GetValueOrDefault() + "' and [DATA_GENERATE_TYPE] = " + (int)DataGenerateTypeConstants.GrowthValue + " and [SECTION_SEQ] < " + info.SectionSeq.GetValueOrDefault() + "");
                if (cnt > 0)
                    throw new Exception("0x00000018");///自动增长规则段只能是最后一段
            }
            ///
            info.SeqCode = new SeqDefineDAL().GetSeqCode(info.DefineFid.GetValueOrDefault());
            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string defineFid = CommonBLL.GetFieldValue(fields, "DEFINE_FID");
            string sectionSeq = CommonBLL.GetFieldValue(fields, "SECTION_SEQ");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [DEFINE_FID] = N'" + defineFid + "' and [SECTION_SEQ] = " + (string.IsNullOrEmpty(sectionSeq) ? "0" : sectionSeq) + "");
            if (cnt > 0)
                throw new Exception("0x00000022");///该段序号规则已维护

            string dataGenerateType = CommonBLL.GetFieldValue(fields, "DATA_GENERATE_TYPE");
            string isSeedValue = CommonBLL.GetFieldValue(fields, "IS_SEED_VALUE");
            if ((string.IsNullOrEmpty(dataGenerateType) ? 0 : int.Parse(dataGenerateType)) == (int)DataGenerateTypeConstants.GrowthValue)
            {
                cnt = dal.GetCounts("[ID] <> " + id + " and [DEFINE_FID] = N'" + defineFid + "' and [DATA_GENERATE_TYPE] = " + (int)DataGenerateTypeConstants.GrowthValue + "");
                if (cnt > 0)
                    throw new Exception("0x00000020");///序列号规则中只能有一段为自动增长方式
                if (isSeedValue == "1")
                    throw new Exception("0x00000019");///自增段不能作为自增种子
            }
            else
            {
                cnt = dal.GetCounts("[ID] <> " + id + " and [DEFINE_FID] = N'" + defineFid + "' and [DATA_GENERATE_TYPE] = " + (int)DataGenerateTypeConstants.GrowthValue + " and [SECTION_SEQ] < " + (string.IsNullOrEmpty(sectionSeq) ? "0" : sectionSeq) + "");
                if (cnt > 0)
                    throw new Exception("0x00000018");///自动增长规则段只能是最后一段
            }
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
        public SeqSectionInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

    }
}
