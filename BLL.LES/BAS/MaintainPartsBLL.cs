using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// MaintainPartsBLL
    /// </summary>
    public class MaintainPartsBLL
    {
        #region Common
        /// <summary>
        /// MaintainPartsDAL
        /// </summary>
        MaintainPartsDAL dal = new MaintainPartsDAL();
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<MaintainPartsInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<MaintainPartsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MaintainPartsInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// ��֤-���
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(MaintainPartsInfo info)
        {
            ///���ϺŢ�ȫ��Χ�������ظ�
            int cnt = dal.GetCounts("[PART_NO] = N'" + info.PartNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000726"); ///���ϺŲ������ظ� 
            return dal.Add(info);
        }
        /// <summary>
        /// ��֤��Ч���ϲֿ������������Ϣ-����ɾ��
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///������Ч���ϲִ���Ϣ������������Ϣ���������ݲ��������ɾ������
            int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts("[PART_NO] in (select [PART_NO] from [LES].[TM_BAS_MAINTAIN_PARTS] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000727");///������Ч����������Ϣ,�޷�ɾ��

            cnt = new PartsStockDAL().GetCounts("[PART_NO] in (select [PART_NO] from [LES].[TM_BAS_MAINTAIN_PARTS] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000728");///������Ч����������Ϣ,�޷�ɾ��
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// ��֤-�޸�
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// ��ȡ�ӿ�����ͬ������Ҫ�ıȽϼ���
        /// </summary>
        /// <returns></returns>
        public List<MaintainPartsInfo> GetListForInterfaceDataSync(List<string> partNos)
        {
            if (partNos.Count == 0)
                return new List<MaintainPartsInfo>();
            return dal.GetListForInterfaceDataSync(partNos);
        }
        /// <summary>
        /// ִ�е���EXCEL����
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<MaintainPartsInfo> maintainPartsExcelInfos = CommonDAL.DatatableConvertToList<MaintainPartsInfo>(dataTable).ToList();
            if (maintainPartsExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

            ///��ȡҵ�����Ҫ��������ݼ���,׼���Ա�
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsDAL().GetListForInterfaceDataSync(maintainPartsExcelInfos.Select(d => d.PartNo).ToList());
            ///ִ�е�SQL���
            string sql = string.Empty;
            ///��ȡ������Ϣ
            List<PlantInfo> plantInfos = new PlantDAL().GetListForInterfaceDataSync();

            List<string> fields = new List<string>(fieldNames.Keys);
            ///���������м������
            foreach (var maintainPartsExcelInfo in maintainPartsExcelInfos)
            {
                ///����ʱ��Ҫ��дLES�Ĺ������
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.Plant == maintainPartsExcelInfo.Plant);
                if (plantInfo == null)
                    throw new Exception("MC:0x00000215");///����������ϵͳ�в�����

                ///��ǰҵ�����ݱ��д˹����ĸ�������Ϣʱ��Ҫ����
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == maintainPartsExcelInfo.PartNo && d.Plant == maintainPartsExcelInfo.Plant);
                if (maintainPartsInfo == null)
                {
                    ///���ϺŢ١������������Ƣ�Ϊ������
                    if (string.IsNullOrEmpty(maintainPartsExcelInfo.PartCname) || string.IsNullOrEmpty(maintainPartsExcelInfo.PartNo))
                        throw new Exception("MC:3x00000020");///���Ϻš�������������Ϊ������

                    ///�ֶ�
                    string insertFieldString = string.Empty;
                    ///ֵ
                    string insertValueString = string.Empty;
                    for (int i = 0; i < fields.Count; i++)
                    {
                        string valueStr = CommonDAL.GetFieldValueForSql<MaintainPartsInfo>(maintainPartsExcelInfo, fields[i]);
                        if (string.IsNullOrEmpty(valueStr))
                            throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶
                        insertFieldString += "[" + fieldNames[fields[i]] + "],";
                        insertValueString += valueStr + ",";
                    }

                    sql += "if not exists (select * from LES.TM_BAS_MAINTAIN_PARTS with(nolock) where [PART_NO] = N'" + maintainPartsExcelInfo.PartNo + "' and [PLANT] = N'" + maintainPartsExcelInfo.Plant + "' and [VALID_FLAG] = 1) "
                        + "insert into [LES].[TM_BAS_MAINTAIN_PARTS] ("
                        + "[FID],"
                        + insertFieldString
                        + "[CREATE_USER],"
                        + "[CREATE_DATE],"
                        + "[VALID_FLAG]"
                        + ") values ("
                        + "NEWID(),"///FID
                        + insertValueString
                        + "N'" + loginUser + "',"///CREATE_USER
                        + "GETDATE(),"///CREATE_DATE
                        + "1"///VALID_FLAG
                        + ");";
                    continue;
                }
                ///�����������Ƣ�Ϊ������
                if (string.IsNullOrEmpty(maintainPartsExcelInfo.PartCname))
                    throw new Exception("MC:3x00000020");///���Ϻš�������������Ϊ������

                ///ֵ
                string valueString = string.Empty;
                for (int i = 0; i < fields.Count; i++)
                {
                    string valueStr = CommonDAL.GetFieldValueForSql<MaintainPartsInfo>(maintainPartsExcelInfo, fields[i]);
                    if (string.IsNullOrEmpty(valueStr))
                        throw new Exception("MC:1x00000043");///���ݸ�ʽ�����ϵ���淶

                    valueString += "[" + fieldNames[fields[i]] + "] = " + valueStr + ",";
                }
                sql += "update [LES].[TM_BAS_MAINTAIN_PARTS] set "
                    + valueString
                    + "[MODIFY_USER] = N'" + loginUser + "',"
                    + "[MODIFY_DATE] = GETDATE() "
                    + "where [ID] = " + maintainPartsInfo.Id + ";";
            }
            ///
            if (string.IsNullOrEmpty(sql)) return false;

            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
    }
}

