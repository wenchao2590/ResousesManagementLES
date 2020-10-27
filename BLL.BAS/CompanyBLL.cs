using DAL.BAS;
using DAL.SYS;
using DM.BAS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.BAS
{
    public partial class CompanyBLL
    {
        CompanyDAL dal = new CompanyDAL();
        public List<CompanyInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            ///对于树形结构来说，datacount是冗余值
            dataCount = int.MaxValue;
            ///对应字段需要转换
            textWhere = textWhere.Replace("PARENT_OID", "PARENT_FID");
            ///首先确定要返回的是COMPANY结合，但是建立树形结构数据的基础是ORGANIZATION，所以我们需要先获取
            List<OrganizationInfo> organlist = new OrganizationDAL().GetList("and [VALID_FLAG] <> 0" + textWhere, textOrder);
            if (organlist.Count == 0)
                return new List<CompanyInfo>();
            ///再通过ORGANIZATION获取COMPANY的扩展属性
            List<CompanyInfo> list = dal.GetList("and [OID] in ('" + string.Join("','", organlist.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "') and [VALID_FLAG] <> 0", string.Empty);
            List<CompanyInfo> returnList = new List<CompanyInfo>();
            foreach (var organinfo in organlist)
            {
                CompanyInfo returnInfo = list.FirstOrDefault(d => d.Oid == organinfo.Fid);
                if (returnInfo == null)
                {
                    ///当没有COMPANY扩展属性时则将部分属性赋予COMPANY
                    returnInfo = new CompanyInfo();
                    returnInfo.CName = organinfo.Name;
                    returnInfo.CCode = organinfo.Code;
                    ///ID非常重要，因为之后的更新及其它操作都会用到这个ID
                    returnInfo.Id = organinfo.Id;
                }
                ///构建树形结构的字段在ORGANIZATION中，需要拼接到COMPANY
                returnInfo.ParentOid = organinfo.ParentFid;
                returnInfo.Oid = organinfo.Fid;
                returnList.Add(returnInfo);
            }
            return returnList;
        }

        public long InsertInfo(CompanyInfo info)
        {
            OrganizationInfo organinfo = new OrganizationInfo();
            organinfo.Fid = Guid.NewGuid();
            ///把前台传来的父节点FID赋予ORGANIZATION对象上
            organinfo.ParentFid = info.ParentOid;
            organinfo.Code = info.CCode;
            organinfo.Name = info.CName;
            organinfo.ValidFlag = true;
            organinfo.CreateDate = DateTime.Now;
            organinfo.CreateUser = info.CreateUser;
            info.ValidFlag = true;
            ////再把ORGANIZATION对象与COMPANY对象以OID做关联
            info.Oid = organinfo.Fid;
            using (var trans = new TransactionScope())
            {
                if (new OrganizationDAL().Add(organinfo) == 0)
                    throw new Exception("ORGANIZATION ADD FAIL");
                info.Id = dal.Add(info);
                if (info.Id == 0)
                    throw new Exception("COMPANY ADD FAIL");
                trans.Complete();
            }
            return info.Id;
        }
        public bool UpdateInfo(string fields, long id)
        {
            CompanyInfo info = dal.GetInfo(id);
            if (info == null)
            {
                OrganizationInfo organinfo = new OrganizationDAL().GetInfo(id);
                if (organinfo == null)
                    throw new Exception("ORGANIZATION IS NULL");
                info = new CompanyInfo();
                info.Oid = organinfo.Fid;
                info.Fid = Guid.NewGuid();
                info.ValidFlag = true;
                info.CreateDate = DateTime.Now;
                info.Id = dal.Add(info);
            }
            return dal.UpdateInfo(fields, info.Id) > 0 ? true : false;
        }
        public bool LogicDeleteInfo(long id, DateTime modifyDate, string modifyUser)
        {
            Guid oid = dal.GetOid(id);
            if (oid == Guid.Empty)
                throw new Exception("COMPANY IS NULL");
            long organid = new OrganizationDAL().GetId(oid);
            using (var trans = new TransactionScope())
            {
                if (new OrganizationDAL().LogicDelete(organid, modifyDate, modifyUser) == 0)
                    throw new Exception("ORGANIZATION DEL FAIL");
                if (dal.LogicDelete(id, modifyDate, modifyUser) == 0)
                    throw new Exception("COMPANY DEL FAIL");
                trans.Complete();
            }
            return true;
        }
        public CompanyInfo SelectInfo(long id)
        {
            CompanyInfo info = dal.GetInfo(id);
            if (info == null)
            {
                OrganizationInfo organinfo = new OrganizationDAL().GetInfo(id);
                if (organinfo == null)
                    throw new Exception("ORGANIZATION IS NULL");
                info = new CompanyInfo();
                info.ParentOid = organinfo.ParentFid;
                info.Oid = organinfo.Fid;
                info.CName = organinfo.Name;
                info.CCode = organinfo.Code;
                info.Id = organinfo.Id;
            }
            return info;
        }
    }
}
