using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class WorkScheduleBLL
    {


        #region Common
        WorkScheduleDAL dal = new WorkScheduleDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<WorkScheduleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkScheduleInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WorkScheduleInfo Collection </returns>
		public List<WorkScheduleInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(WorkScheduleInfo info)
        {
            ///��������١��������ڡ����ߴ���ۡ����ڢܡ���΢������Ҫ����Ψһ��У��
            int cnt = dal.GetCounts(string.Format(@"and [PLANT] = N'{0}' and [WORKSHOP] = N'{1}' and [ASSEMBLY_LINE] = N'{2}' and [DATE] = N'{3}' and [SHIFT] = N'{4}' ", info.Plant, info.Workshop, info.AssemblyLine, info.Date, info.Shift));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000196");
            ///�򹤳�����١��������ڡ����ߴ����Ϊ����������ϵ��������ҪУ�鸸���Ӧ�Ƿ��Ѵ�������Ϊ�յ����ݣ�
            ///���繤�������1000+��������ZZ01��ά����2018-03-17���������
            ///����ά�����������1000+��������ZZ01+���ߴ����T01��2018-03-17�������
            cnt = dal.GetCounts(string.Format(@"and [PLANT] = N'{0}' and [WORKSHOP] = N'{1}' and [ASSEMBLY_LINE] <> N'{2}' and [DATE] = N'{3}' and [SHIFT] = N'{4}' ", info.Plant, info.Workshop, info.AssemblyLine, info.Date, info.Shift));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000197");
            ///
            cnt = dal.GetCounts(string.Format(@"and [PLANT] = N'{0}' and [WORKSHOP] <> N'{1}' and [DATE] = N'{2}' and [SHIFT] = N'{3}' ", info.Plant, info.AssemblyLine, info.Date, info.Shift));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000198");
            ///
            info.BeginTime = DateTime.Parse(info.Date.GetValueOrDefault().ToString("yyyy-MM-dd") + " " + info.BeginTime.GetValueOrDefault().ToString("HH:mm"));
            info.EndTime = DateTime.Parse(info.Date.GetValueOrDefault().ToString("yyyy-MM-dd") + " " + info.EndTime.GetValueOrDefault().ToString("HH:mm"));
            //����У����ͬά���¿�ʼʱ��+����ʱ��Ϊʱ�䷶Χ����������֮��ʱ�䷶Χ���ܽ��棬
            //������ͬ��������١��������ڡ����ߴ��������ά����2018-03-17 08:00:00�� 2018-03-17 16:00:00�����ݣ�
            //��ά��2018-03-17 15:00:00�� 2018-03-18 06:00:00�����ݱ���ʱ��Ҫ����
            if (info.BeginTime >= info.EndTime)
                info.EndTime = info.EndTime.GetValueOrDefault().AddDays(1);
            cnt = dal.GetCounts(string.Format(@"and [PLANT] = N'{0}' and [WORKSHOP] = N'{1}' and [ASSEMBLY_LINE] = N'{2}' "
+ "and (([BEGIN_TIME] < N'{3}' and [END_TIME] > N'{3}') or ([END_TIME] > N'{4}' and [BEGIN_TIME] < N'{4}') or ([BEGIN_TIME] > N'{3}' and [BEGIN_TIME] < N'{4}') or ([END_TIME] > N'{3}' and [END_TIME] < N'{4}')) "
, info.Plant, info.Workshop, info.AssemblyLine, info.BeginTime, info.EndTime));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000199");
            return dal.Add(info);
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
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string plant = CommonBLL.GetFieldValue(fields, "PLANT");
            string workshop = CommonBLL.GetFieldValue(fields, "WORKSHOP");
            string assemblyLine = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE");
            DateTime date = DateTime.Parse(CommonBLL.GetFieldValue(fields, "DATE"));
            DateTime beginTime = DateTime.Parse(CommonBLL.GetFieldValue(fields, "BEGIN_TIME"));
            DateTime endTime = DateTime.Parse(CommonBLL.GetFieldValue(fields, "END_TIME"));
            ///
            beginTime = DateTime.Parse(date.ToString("yyyy-MM-dd") + " " + beginTime.ToString("HH:mm"));
            endTime = DateTime.Parse(date.ToString("yyyy-MM-dd") + " " + endTime.ToString("HH:mm"));
            //����У����ͬά���¿�ʼʱ��+����ʱ��Ϊʱ�䷶Χ����������֮��ʱ�䷶Χ���ܽ��棬
            //������ͬ��������١��������ڡ����ߴ��������ά����2018-03-17 08:00:00�� 2018-03-17 16:00:00�����ݣ�
            //��ά��2018-03-17 15:00:00�� 2018-03-18 06:00:00�����ݱ���ʱ��Ҫ����
            if (beginTime >= endTime)
                endTime = endTime.AddDays(1);
            int cnt = dal.GetCounts(string.Format(@"and [PLANT] = N'{0}' and [WORKSHOP] = N'{1}' and [ASSEMBLY_LINE] = N'{2}' "
+ "and (([BEGIN_TIME]< N'{3}' and [END_TIME] > N'{3}') or ([END_TIME] > N'{4}' and [BEGIN_TIME] < N'{4}') or ([BEGIN_TIME] > N'{3}' and [BEGIN_TIME] < N'{4}') or ([END_TIME] > N'{3}' and [END_TIME] < N'{4}')) and [ID] <>{5}"
, plant, workshop, assemblyLine, beginTime, endTime, id));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000199");
            ///
            fields = CommonBLL.SetFieldValue(fields, "BEGIN_TIME", beginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            fields = CommonBLL.SetFieldValue(fields, "END_TIME", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        #region privete
        /// <summary>
        /// ��ȡͬ������ıȽϼ���
        /// </summary>
        /// <param name="partNos"></param>
        /// <returns></returns>
        public List<WorkScheduleInfo> GetListForInterfaceDataSync(List<DateTime> date)
        {
            if (date.Count == 0) return new List<WorkScheduleInfo>();
            return dal.GetListForInterfaceDataSync(date);
        }
        #endregion
    }
}

