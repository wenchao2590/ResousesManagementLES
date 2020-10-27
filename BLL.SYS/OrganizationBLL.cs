using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class OrganizationBLL
    {
        #region Common
        OrganizationDAL dal = new OrganizationDAL();
        public List<OrganizationInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
    , out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public List<OrganizationInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        public long InsertInfo(OrganizationInfo info)
        {
            info.ValidFlag = true;
            return dal.Add(info);
        }
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        public OrganizationInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion


        /// <summary>
        /// 根据父组织外键获取旗下所有子组织的外键
        /// </summary>
        /// <param name="parentFid"></param>
        /// <returns></returns>
        public List<Guid> GetOrganizationFidsByParentFid(Guid parentFid)
        {
            ///根据父组织外键获取旗下所有子组织的外键
            List<Guid> organizationFids = dal.GetOrganizationFidsByParentFid(parentFid);
            ///
            List<Guid> resultFids = new List<Guid>();
            foreach (var organizationFid in organizationFids)
            {
                ///递归子组织，获取子组织下的子组织
                resultFids.AddRange(GetOrganizationFidsByParentFid(organizationFid));
            }
            resultFids.Add(parentFid);
            return resultFids;
        }
        /// <summary>
        /// 根据父组织外键获取旗下所有子组织的外键
        /// </summary>
        /// <param name="parentFids"></param>
        /// <returns></returns>
        public List<Guid> GetOrganizationFidsByParentFids(List<Guid> parentFids)
        {
            List<Guid> resultFids = new List<Guid>();
            foreach (var parentFid in parentFids)
            {
                resultFids.AddRange(GetOrganizationFidsByParentFid(parentFid));
                resultFids.Add(parentFid);
            }
            return resultFids;
        }

        public List<string> GetCodesByFids(List<Guid> fids)
        {
            return dal.GetCodesByFids(fids);
        }
        /// <summary>
        /// 获取CODE-NAME的组织描述信息
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string GetNameByFid(Guid fid)
        {
            OrganizationInfo info = dal.GetInfo(fid);
            if (info == null) return string.Empty;
            return info.Code + "-" + info.Name;
        }
    }
}
