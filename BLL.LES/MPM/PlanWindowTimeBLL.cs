using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PlanWindowTimeBLL
    {
        #region Common
        PlanWindowTimeDAL dal = new PlanWindowTimeDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PlanWindowTimeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlanWindowTimeInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PlanWindowTimeInfo info)
        {
            ///零件类代码②名称③，必选项，来自于计划拉动零件类TM_MPM_PLAN_PART_BOX，保存时需要同时保存零件类外键①
            PlanPartBoxInfo planPartBoxInfo = new PlanPartBoxDAL().GetInfo(info.PartBoxCode);
            if (planPartBoxInfo == null)
                throw new Exception("Err_:MC:0x00000738");///请选择有效的零件类代码
            info.PartBoxFid = planPartBoxInfo.Fid.GetValueOrDefault();
            ///工作日⑧默认为当天日期，在窗口时间⑩填写完成时自动根据零件类中对应的几个时间合计扣减得到发单时间⑨，发单时间⑨不可编辑
            info.WindowTime = DateTime.Parse(info.WorkDay.GetValueOrDefault().ToString("yyyy-MM-dd") + " " + info.WindowTime.GetValueOrDefault().ToString("HH:mm:ss"));
            int sumTime = planPartBoxInfo.PickUpTime.GetValueOrDefault() + planPartBoxInfo.DeliveryTime.GetValueOrDefault() + planPartBoxInfo.DelayTime.GetValueOrDefault();
            info.SendTime = info.WindowTime.GetValueOrDefault().AddMinutes(sumTime * -1);
            ///相同零件类代码②、工作日⑧、窗口时间⑩的数据不允许重复
            int cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "' and [WINDOW_TIME] = N'" + info.WindowTime + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000740");///相同零件类、工作日的窗口时间不能重复
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
            ///发单状态为未发单才允许被删除
            int cnt = dal.GetCounts("[ID] = " + id + " and [SEND_TIME_STATUS] = " + (int)SendTimeStatusConstants.NoSend + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000668");///发单状态为未发单才允许被删除
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
            ///发单状态⑪为10未发单才允许被修改
            string sendTimeStatus = CommonBLL.GetFieldValue(fields, "SEND_TIME_STATUS");
            if (Convert.ToInt32(sendTimeStatus) != (int)SendTimeStatusConstants.NoSend)
                throw new Exception("Err_:MC:0x00000741");///未发单状态窗口时间才允许修改
            string windowTime = CommonBLL.GetFieldValue(fields, "WINDOW_TIME");
            string workDay = CommonBLL.GetFieldValue(fields, "WORK_DAY");
            string partBoxCode = CommonBLL.GetFieldValue(fields, "PART_BOX_CODE");
            PlanPartBoxInfo planPartBoxInfo = new PlanPartBoxDAL().GetInfo(partBoxCode);
            if (planPartBoxInfo == null)
                throw new Exception("Err_:MC:0x00000738");///请选择有效的零件类代码

            ///工作日⑧默认为当天日期，在窗口时间⑩填写完成时自动根据零件类中对应的几个时间合计扣减得到发单时间⑨，发单时间⑨不可编辑
            DateTime windowDateTime = DateTime.Parse(DateTime.Parse(workDay).ToString("yyyy-MM-dd") + " " + DateTime.Parse(windowTime).ToString("HH:mm:ss"));
            int sumTime = planPartBoxInfo.PickUpTime.GetValueOrDefault() + planPartBoxInfo.DeliveryTime.GetValueOrDefault() + planPartBoxInfo.DelayTime.GetValueOrDefault();
            DateTime sendDateTime = windowDateTime.AddMinutes(sumTime * -1);
            fields = CommonBLL.SetFieldValue(fields, "WINDOW_TIME", windowDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            fields = CommonBLL.SetFieldValue(fields, "SEND_TIME", sendDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            ///相同零件类代码②、工作日⑧、窗口时间⑩的数据不允许重复
            int cnt = dal.GetCounts("[ID] <> " + id + " and [PART_BOX_CODE] = N'" + partBoxCode + "' and [WINDOW_TIME] = N'" + windowDateTime + "'");
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000740");///相同零件类、工作日的窗口时间不能重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 根据零件类外键
        /// 获取最近一个未发单的窗口时间
        /// </summary>
        /// <param name="partBoxFid"></param>
        /// <returns></returns>
        public PlanWindowTimeInfo GetLastNoSendTimeInfo(Guid partBoxFid)
        {
            return dal.GetLastNoSendTimeInfo(partBoxFid);
        }
        /// <summary>
        /// 根据零件类、当前窗口时间
        /// 获取下一窗口时间
        /// </summary>
        /// <param name="partBoxFid"></param>
        /// <param name="currentWindowTime"></param>
        /// <returns></returns>
        public DateTime GetNextWindowTime(Guid partBoxFid, DateTime currentWindowTime)
        {
            return dal.GetNextWindowTime(partBoxFid, currentWindowTime);
        }
        /// <summary>
        /// 根据工作日、零件类
        /// 获取窗口时间集合
        /// </summary>
        /// <param name="partBoxFid"></param>
        /// <param name="workDay"></param>
        /// <returns></returns>
        public List<DateTime> GetWindowTimesByWorkDay(Guid partBoxFid, DateTime workDay)
        {
            return dal.GetWindowTimesByWorkDay(workDay, partBoxFid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partBoxFid"></param>
        /// <param name="workDay"></param>
        /// <returns></returns>
        public List<string> GettimeZoneByWorkDay(Guid partBoxFid, DateTime workDay)
        {
            return dal.GettimeZoneByWorkDay(workDay, partBoxFid);
        }
        public List<PlanWindowTimeInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
    }
}

