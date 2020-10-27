using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class ImageResourceBLL
    {
        #region Common
        ImageResourceDAL dal = new ImageResourceDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<ImageResourceInfo></returns>
        public List<ImageResourceInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ImageResourceInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ImageResourceInfo info)
        {
            ///图片名称不允许重复
            int cnt = dal.GetCounts("[IMAGE_NAME] = N'" + info.ImageName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000007"); ///图片名称不允许重复

            ///相同图片类型的图片描述不允许重复
            cnt = dal.GetCounts("[IMAGE_NAME_CN] = N'" + info.ImageNameCn + "' and [IMAGE_TYPE] = " + info.ImageType.GetValueOrDefault() + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000010"); ///相同图片类型的图片描述不允许重复
            cnt = dal.GetCounts("[IMAGE_NAME_EN] = N'" + info.ImageNameEn + "' and [IMAGE_TYPE] = " + info.ImageType.GetValueOrDefault() + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000010"); ///相同图片类型的图片描述不允许重复

            ///样式名称不允许重复
            cnt = dal.GetCounts("[CSS_TAG_NAME] = N'" + info.CssTagName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000012"); ///样式名称不允许重复
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
            ///已被使用？
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
            string imageNameCn = CommonBLL.GetFieldValue(fields, "IMAGE_NAME_CN");
            string imageType = CommonBLL.GetFieldValue(fields, "IMAGE_TYPE");
            ///相同图片类型的图片描述不允许重复
            int cnt = dal.GetCounts("[ID] <> " + id + " and [IMAGE_NAME_CN] = N'" + imageNameCn + "' and [IMAGE_TYPE] = " + imageType + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000010"); ///相同图片类型的图片描述不允许重复
            string imageNameEn = CommonBLL.GetFieldValue(fields, "IMAGE_NAME_EN");
            cnt = dal.GetCounts("[ID] <> " + id + " and [IMAGE_NAME_EN] = N'" + imageNameEn + "' and [IMAGE_TYPE] = " + imageType + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000010"); ///相同图片类型的图片描述不允许重复
            string cssTagName = CommonBLL.GetFieldValue(fields, "CSS_TAG_NAME");
            ///样式名称不允许重复
            cnt = dal.GetCounts("[ID] <> " + id + " and [CSS_TAG_NAME] = N'" + cssTagName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000012"); ///样式名称不允许重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageContent"></param>
        /// <param name="extensionName"></param>
        /// <param name="imageColumnName"></param>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool UpdateImage(byte[] imageContent, string extensionName, string id, string loginUser)
        {
            string[] imageFileTypes = new string[] { "PNG", "ICO", "JPG", "GIF" };
            if (!imageFileTypes.Contains(extensionName.ToUpper()))
                throw new Exception("MC:0x00000013"); ///系统不支持当前上传的图片格式，请联系管理员

            ///默认ICO，(int)ImageFileTypeConstants.Ico
            int extensionCode = 10;
            switch (extensionName.ToLower())
            {
                case "png": extensionCode = (int)ImageFileTypeConstants.Png; break;
                case "jpg": extensionCode = (int)ImageFileTypeConstants.Jpg; break;
                case "gif": extensionCode = (int)ImageFileTypeConstants.Gif; break;
            }
            int cnt = dal.GetCounts("[IMAGE_FILE_TYPE] = " + extensionCode + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000014"); ///文件格式不符
            return dal.UpdateImage(long.Parse(id), imageContent, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 读取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public byte[] ReadImage(string id)
        {
            return dal.ReadImage(long.Parse(id));
        }
        #endregion
    }
}

