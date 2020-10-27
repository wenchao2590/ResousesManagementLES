using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Infrustructure.Utilities;

namespace Infrustructure.Mail
{
    public class MailSendListInfo
    {
        #region Constructors
        public MailSendListInfo(){}
        public MailSendListInfo(int aMailSeqId,
                string aPlant,
                string aWorkshop,
                string aAssemblyLine,
                string aProduct,
                int aSysId,
                string aAlarmName,
                string aAlarmSubject,
                string aMailBody,
                string aCcMailGroup,
                string aMails,
                int aSendStatus,
                DateTime aSendDate,
                string aCreateUser,
                DateTime aCreateDate,
                string aUpdateUser,
                DateTime aUpdateDate
        )
        {

            MailSeqId = aMailSeqId;

            Plant = aPlant;

            Workshop = aWorkshop;

            AssemblyLine = aAssemblyLine;

            Product = aProduct;

            SysId = aSysId;

            AlarmName = aAlarmName;

            AlarmSubject = aAlarmSubject;

            MailBody = aMailBody;

            CcMailGroup = aCcMailGroup;

            Mails = aMails;

            SendStatus = aSendStatus;

            SendDate = aSendDate;

            CreateUser = aCreateUser;

            CreateDate = aCreateDate;

            UpdateUser = aUpdateUser;

            UpdateDate = aUpdateDate;
        }
        #endregion

        #region Attributes


        public int MailSeqId { get; set; }


        public string Plant { get; set; }


        public string Workshop { get; set; }


        public string AssemblyLine { get; set; }


        public string Product { get; set; }


        public int SysId { get; set; }


        public string AlarmName { get; set; }


        public string AlarmSubject { get; set; }


        public string MailBody { get; set; }


        public string CcMailGroup { get; set; }


        public string Mails { get; set; }


        public int? SendStatus { get; set; }


        public DateTime? SendDate { get; set; }


        public string CreateUser { get; set; }


        public DateTime CreateDate { get; set; }


        public string UpdateUser { get; set; }


        public DateTime? UpdateDate { get; set; }

        #endregion
    }

    public class AlartMailDAL
    {
        #region Sql Statements
        private const string TT_SYS_MAIL_SEND_LIST_INSERT =
            @"DECLARE @ReturnID INT; INSERT INTO [MES].[TT_SYS_MAIL_SEND_LIST] ( 
			PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				PRODUCT,
				SYS_ID,
				ALARM_NAME,
				ALARM_SUBJECT,
				MAIL_BODY,
				CC_MAIL_GROUP,
				MAILS,
				SEND_STATUS,
				SEND_DATE,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE				 
			) VALUES (
			 @PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@PRODUCT,
				@SYS_ID,
				@ALARM_NAME,
				@ALARM_SUBJECT,
				@MAIL_BODY,
				@CC_MAIL_GROUP,
				@MAILS,
				@SEND_STATUS,
				@SEND_DATE,
				@CREATE_USER,
				@CREATE_DATE,
				@UPDATE_USER,
				@UPDATE_DATE				 
			);
			SET @ReturnID=0;			 
			SELECT @ReturnID;";

        #endregion

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="info"> info</param>
        public int Add(MailSendListInfo info)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_SYS_MAIL_SEND_LIST_INSERT);


            db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);

            db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);

            db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);

            db.AddInParameter(dbCommand, "@PRODUCT", DbType.String, info.Product);

            db.AddInParameter(dbCommand, "@SYS_ID", DbType.Int32, info.SysId);

            db.AddInParameter(dbCommand, "@ALARM_NAME", DbType.String, info.AlarmName);

            db.AddInParameter(dbCommand, "@ALARM_SUBJECT", DbType.String, info.AlarmSubject);

            db.AddInParameter(dbCommand, "@MAIL_BODY", DbType.String, info.MailBody);

            db.AddInParameter(dbCommand, "@CC_MAIL_GROUP", DbType.String, info.CcMailGroup);

            db.AddInParameter(dbCommand, "@MAILS", DbType.String, info.Mails);

            db.AddInParameter(dbCommand, "@SEND_STATUS", DbType.Int32, info.SendStatus);

            db.AddInParameter(dbCommand, "@SEND_DATE", DbType.DateTime, info.SendDate);

            db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);

            db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);

            db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);

            db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);

            return int.Parse("0" + db.ExecuteScalar(dbCommand));
        }

        public bool Exists(MailSendListInfo info)
        {
            string SQL_EXISTS =
                @" DECLARE @Result INT;
                            IF EXISTS( 
                            SELECT TOP 1 *
                            FROM  [MES].[TT_SYS_MAIL_SEND_LIST](nolock)
                            WHERE SYS_ID= '{0}' AND DATEDIFF(DAY, CREATE_DATE,GETDATE()) = 0
                            )
                            SET @Result = 1; 
                            ELSE 
                            SET @Result = 0; SELECT @Result;";

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(string.Format(SQL_EXISTS, info.SysId));

            return int.Parse("0" + db.ExecuteScalar(dbCommand)) > 0;

        }

        public bool Exists2(MailSendListInfo info)
        {
            string SQL_EXISTS =
                @" DECLARE @Result INT;
                            IF EXISTS( 
                            SELECT TOP 1 *
                            FROM  [MES].[TT_SYS_MAIL_SEND_LIST](nolock)
                            WHERE SYS_ID= '{0}' AND MAIL_BODY = '{1}' 
                            )
                            SET @Result = 1; 
                            ELSE 
                            SET @Result = 0; SELECT @Result;";

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(string.Format(SQL_EXISTS, info.SysId,info.MailBody));

            return int.Parse("0" + db.ExecuteScalar(dbCommand)) > 0;

        }
    }
}

        