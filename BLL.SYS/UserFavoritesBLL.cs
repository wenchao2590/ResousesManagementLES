namespace BLL.SYS
{
    using DAL.SYS;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// UserFavoritesBLL
    /// </summary>
    public partial class UserFavoritesBLL
    {
        #region Common
        /// <summary>
        /// UserFavoritesDAL
        /// </summary>
        UserFavoritesDAL dal = new UserFavoritesDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<UserFavoritesInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<UserFavoritesInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(UserFavoritesInfo info)
        {
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserFavoritesInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="menuFid"></param>
        /// <returns></returns>
        public bool DelFavorite(Guid userFid, Guid roleFid, Guid menuFid)
        {
            return dal.LogicDelete(userFid, roleFid, menuFid);
        }
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="userFid"></param>
        /// <param name="roleFid"></param>
        /// <param name="menuFid"></param>
        /// <returns></returns>
        public bool AddFavorite(Guid userFid, Guid roleFid, Guid menuFid)
        {
            int cnt = dal.GetCounts("[USER_FID] = N'" + userFid + "' and [ROLE_FID] = N'" + roleFid + "' and [MENU_FID] = N'" + menuFid + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000015");///TODO:该页面已添加收藏

            UserFavoritesInfo info = new UserFavoritesInfo();
            info.Fid = Guid.NewGuid();
            info.MenuFid = menuFid;
            info.UserFid = userFid;
            info.RoleFid = roleFid;
            info.CreateDate = DateTime.Now;
            info.CreateUser = string.Empty;
            info.ValidFlag = true;
            if (dal.Add(info) == 0)
                throw new Exception("MC:1x00000015");///TODO:添加收藏失败

            return true;
        }
    }
}
