
Create View LES.V_TT_SPM_CONTENT
AS 

SELECT [CONTENT_NO]
      ,[PLANT]
      ,[ASSEMBLY_LINE]
      ,[WORKSHOP]
      ,[PLANT_ZONE]
      ,[CONTENT_TYPE]
      ,[CONTENT_STATUS]
      ,[CLOSE_DATE]
      ,[START_EFFECTIVE_DATE]
      ,[EXPIRE_DATE]
      ,[SUBJECT]
      ,[BODY]
      ,[DEPT_CODE]
      ,[DEPT_NAME]
      ,[SUPPLIER_NUM]
      ,[SUPPLIER_GROUP_ID]
      ,[CUSTOM_NO]
      ,[PLANNER_CODE]
      ,[PUBLISH_TIME]
      ,[PUBLISHER]
	  ,[EXPIRE_STATUS]=Case When [CONTENT_STATUS]=0 And GETDATE()<START_EFFECTIVE_DATE Then 0 --公告未发布
	                        When [CONTENT_STATUS]=1 And GETDATE()<START_EFFECTIVE_DATE Then 1 --已经发布但未到生效时间 未生效
	                        When [CONTENT_STATUS]=1 And GETDATE() Between START_EFFECTIVE_DATE And EXPIRE_DATE Then 2 --已经发布在生效时间范围 已生效
							When [CONTENT_STATUS]=1 And GETDATE()>EXPIRE_DATE Then 3 --已经发布但已过生效时间范围 已过期
							Else 0 End
      ,[COMMENTS]
      ,[CREATE_USER]
      ,[CREATE_DATE]
      ,[UPDATE_USER]
      ,[UPDATE_DATE]
  FROM [LES].[TT_SPM_CONTENT]