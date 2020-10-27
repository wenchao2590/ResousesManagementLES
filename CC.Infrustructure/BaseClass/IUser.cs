using System;
using System.Collections.Generic;
using System.Drawing;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Infrustructure.BaseClass
{
    [DataContract]
    public enum UserType
    {
        [EnumMember]
        JITDD,
        [EnumMember]
        Supplier,
        [EnumMember]
        EPS,
        [EnumMember]
        RDC
    }

    [DataContract]
    public enum UserStatus
    {
        [EnumMember]
        Active,
        [EnumMember]
        Stop
    }

    public enum UserToken
    {
        Login, Logout, Unknown
    }

    [KnownType(typeof(User))]
    [KnownType(typeof(AnonymousUser))]
    [KnownType(typeof(AuthrationUser))]
    [DataContract]
    public abstract class IUser
    {
        [DataMember]
        public long UserID { get; set; }
        [DataMember]
        public Guid UserFid { get; set; }
        [DataMember]
        public string UserLoginName { get; set; }
        [DataMember]
        public string UserPassword { get; set; }

        [DataMember]
        public string EmployeeName { get; set; }

        [DataMember]
        public DateTime? PasswordExpireTime { get; set; }
        [DataMember]
        public List<int> Roles { get; set; }
        [DataMember]
        public Guid CurrentRoleFid { get; set; }

        [DataMember]
        public string MenuName { get; set; }

        [DataMember]
        public Guid WorkCellFid { get; set; }

        [DataMember]
        public Guid ProdLineFid { get; set; }

        [DataMember]
        public Guid WorkShopFid { get; set; }

        [DataMember]
        public string WorkCellCode { get; set; }

        [DataMember]
        public string WorkShopCode { get; set; }

        [DataMember]
        public string ProdLineCode { get; set; }


        /// <summary>
        /// 用户能访问的菜单资源
        /// </summary>
        [DataMember]
        public List<string> CanAccessMenuURLs { get; set; }

        /// <summary>
        /// 用户所能访问的菜单
        /// </summary>
        public dynamic Menus { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        public dynamic RoleInfos { get; set; }

        public UserType UserType
        {
            get
            {
                switch (_UserType)
                {
                    case 1:
                        return UserType.EPS;
                    case 0:
                        return UserType.JITDD;
                    case 2:
                        return UserType.RDC;
                    case 3:
                        return UserType.Supplier;
                    default:
                        throw new System.Exception();
                }
            }
            set
            {
                switch (value)
                {
                    case UserType.EPS:
                        _UserType = 1;
                        break;
                    case UserType.JITDD:
                        _UserType = 0;
                        break;
                    case UserType.RDC:
                        _UserType = 2;
                        break;
                    case UserType.Supplier:
                        _UserType = 3;
                        break;
                }
            }
        }

        [DataMember]
        public UserStatus UserStatus
        {
            get
            {
                return _UserStatus;
            }
            set
            {
                if (value == UserStatus.Stop)
                    _UserStatus = UserStatus.Stop;
                else if (value == UserStatus.Active)
                    _UserStatus = UserStatus.Active;
            }
        }

        private int _UserType;
        private UserStatus _UserStatus;

        [DataMember]
        public string PlantCode;

        [DataMember]
        public Guid PlantFid;

        /// <summary>
        /// 用户头像
        /// </summary>
        public Image Img { get; set; }
    }
}
