using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class ButtonBLL
    {
        #region Common
        ButtonDAL dal = new ButtonDAL();
        public List<ButtonInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public ButtonInfo SelectInfo(string plant,string assemblyLine,string buttonId)
        {
            return dal.GetInfo(plant,assemblyLine,buttonId);
        }

        public bool InsertInfo(ButtonInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(string plant,string assemblyLine,string buttonId)
        {
            return dal.Delete(plant,assemblyLine,buttonId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, string plant,string assemblyLine,string buttonId)
        {
            return dal.UpdateInfo(fields, plant,assemblyLine,buttonId) > 0 ? true : false;
        }

        #endregion
    }
}

