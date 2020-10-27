using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public partial class ActionInfo
    {
        private int displayOrder;
        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            get
            {
                return displayOrder;
            }

            set
            {
                displayOrder = value;
            }
        }
        /// <summary>
        /// JS事件
        /// </summary>
        public string ClientJs
        {
            get
            {
                return clientJs;
            }

            set
            {
                clientJs = value;
            }
        }
        /// <summary>
        /// 是否已授权
        /// </summary>
        public bool IsRelationed
        {
            get
            {
                return isRelationed;
            }

            set
            {
                isRelationed = value;
            }
        }
        /// <summary>
        /// 是否需要授权
        /// </summary>
        public bool NeedAuth
        {
            get
            {
                return needAuth;
            }

            set
            {
                needAuth = value;
            }
        }
        /// <summary>
        /// 是否LIST的ACTION
        /// </summary>
        public bool IsListAction
        {
            get
            {
                return isListAction;
            }

            set
            {
                isListAction = value;
            }
        }
        /// <summary>
        /// 是否详情功能按钮
        /// </summary>
        public bool DetailFlag { get => detailFlag; set => detailFlag = value; }


        private bool detailFlag;

        private string clientJs;

        private bool isRelationed;

        private bool needAuth;

        private bool isListAction;

    }
}
