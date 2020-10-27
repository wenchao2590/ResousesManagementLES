
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.BaseClass
{
    public partial class SupplierUser
    {
        #region ��Ӧ�̵�¼�Ļ�����Ϣ
        /// <summary>
        /// ��¼�û�ID
        /// </summary>
        public int UserID { get; set; } 

        /// <summary>
        /// ��¼����
        /// </summary>
        public string LoginName { get; set; } 

        /// <summary>
        /// �û���
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// �û�״̬
        /// </summary>
        public int UserStatus { get; set; }

        /// <summary>
        /// ʧ�ܵ�¼����
        /// </summary>
        public int FailLogin { get; set; }

        /// <summary>
        /// �������ʱ��
        /// </summary>
        public DateTime? PasswordExpireTime { get; set; }

        /// <summary>
        /// ��¼�û�����
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// ��Ӧ�̴���
        /// </summary>
        public string SupplierNum { get; set; }

        /// <summary>
        /// ��Ӧ������
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// ��Ӧ������
        /// </summary>
        public int SupplierType { get; set; }


        /// <summary>
        /// ��Ӧ�̵�ַ
        /// </summary>
        public string SupplierAddress { get; set; }

        
        #endregion

    }
}
