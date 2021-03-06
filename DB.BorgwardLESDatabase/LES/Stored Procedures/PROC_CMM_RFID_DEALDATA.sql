﻿-- =============================================
-- Author:		XinPengZhang
-- Create date: 2017-11-21
-- Description:	入场协同RFID 物流门逻辑处理
-- EXEC [LES].[PROC_CMM_RFID_DEALDATA] '08FDC5DC-AF69-4809-A161-D76821CD1F89','RFIDDealDataService'
-- =============================================
CREATE PROCEDURE [LES].[PROC_CMM_RFID_DEALDATA]
	@FID uniqueidentifier,
	@TYPE INT,
	@Create_User NVARCHAR(128)
AS
BEGIN
	IF @TYPE = 1  --收货入库
	BEGIN
		 DECLARE @RECEIVE_ID INT		
		 IF EXISTS (SELECT * FROM LES.TI_MID_RFID_LPN_RFIDLIST WHERE FID = @FID)
		 BEGIN
	 		--按箱入库
			SELECT C.RECEIVE_ID,B.PART_NO,B.BARCODE_DATA,ActualQty=COUNT(*) INTO #SUBMITBOX FROM LES.TI_MID_RFID_LPN_RFIDLIST A
			JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] B ON A.RFID_NO = B.RFID_NO
			JOIN LES.TT_WMM_RECEIVE C ON B.PLAN_ASN_RUNSHEET_NO = C.RECEIVE_NO
			JOIN LES.TT_WMM_RECEIVE_DETAIL D ON D.RECEIVE_ID = C.RECEIVE_ID AND D.PART_NO = B.PART_NO
			WHERE A.FID = @FID
			GROUP BY C.RECEIVE_ID,B.PART_NO,B.BARCODE_DATA

			-- 使用游标遍历数据结果
			DECLARE @PART_NO NVARCHAR(50),@BARCODE_DATA NVARCHAR(50),@ACTUALQTY INT
    
			-- 声明游标
			DECLARE C_SUBMITBOX CURSOR FAST_FORWARD FOR
				SELECT RECEIVE_ID,PART_NO,BARCODE_DATA,ActualQty FROM #SUBMITBOX
    
			OPEN C_SUBMITBOX;
			-- 取第一条记录
			FETCH NEXT FROM C_SUBMITBOX INTO @RECEIVE_ID,@PART_NO,@BARCODE_DATA,@ACTUALQTY;
			WHILE @@FETCH_STATUS=0
			BEGIN
				--入库操作
				EXEC [LES].[PROC_WMM_RECEIVE_SUBMITBOX_New] @RECEIVE_ID,@PART_NO,@BARCODE_DATA,@ACTUALQTY,@Create_User
    
				-- 取下一条记录
				FETCH NEXT FROM C_SUBMITBOX INTO @RECEIVE_ID,@PART_NO,@BARCODE_DATA,@ACTUALQTY
			END
			-- 关闭游标
			CLOSE C_SUBMITBOX;
			-- 释放游标
			DEALLOCATE C_SUBMITBOX;

			SELECT RESULTMSG=RECEIVE_ID FROM 
			(
				SELECT DISTINCT RECEIVE_ID FROM #SUBMITBOX
			)A
		 END 
		 ELSE
		 BEGIN
			--按拖入库
			SELECT D.RECEIVE_ID,B.TRAY_NO INTO #TRAY FROM LES.TI_MID_RFID_Read_Lable A
			JOIN [LES].[TT_SPM_TRAY_INFO] B ON A.LPN_NO = B.RFID_NO
			JOIN [LES].[TT_SPM_TRAY_DETAIL] C ON B.TRAY_NO = C.TRAY_NO AND C.VALID_FLAG = 1
			JOIN LES.TT_WMM_RECEIVE D ON B.BILL_NO = D.RECEIVE_NO

			-- 使用游标遍历数据结果
			DECLARE @TRAY_NO NVARCHAR(50)
    
			-- 声明游标
			DECLARE C_TRAY CURSOR FAST_FORWARD FOR
				SELECT RECEIVE_ID,TRAY_NO FROM #TRAY
    
			OPEN C_TRAY;
			-- 取第一条记录
			FETCH NEXT FROM C_TRAY INTO @RECEIVE_ID,@TRAY_NO;
			WHILE @@FETCH_STATUS=0
			BEGIN
				--入库操作
				EXEC [LES].[PROC_WMM_RECEIVE_SUBMIT_TRAY] @RECEIVE_ID,@TRAY_NO,@Create_User
    
				-- 取下一条记录
				FETCH NEXT FROM C_TRAY INTO @RECEIVE_ID,@TRAY_NO
			END
			-- 关闭游标
			CLOSE C_TRAY;
			-- 释放游标
			DEALLOCATE C_TRAY;

			SELECT RESULTMSG=RECEIVE_ID FROM 
			(
				SELECT DISTINCT RECEIVE_ID FROM #TRAY
			)A
		 END
	 END
	 ELSE IF @TYPE = 2 --配送出库
	 BEGIN
			--按箱出库,检查料箱是否为已出库状态
			SELECT RESULTMSG=A.RFID_NO FROM LES.TI_MID_RFID_LPN_RFIDLIST A
			LEFT JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] B ON A.RFID_NO = B.RFID_NO
			WHERE A.FID = @FID
			AND ISNULL(B.BARCODE_STATUS,0) <> 2
	 END
	 ELSE IF @TYPE = 3 --空箱入库
	 BEGIN
			--库存调整信息
			SELECT IN_WMNO=A.WM_NO,IN_ZONENO=A.ZONE_NO,OUT_WMNO=C.WM_NO,OUT_ZONENO=C.ZONE_NO,C.PACKAGE_NO,A.NUM INTO #STOCK_NUM FROM 
			(
				SELECT C.WM_NO,C.ZONE_NO,B.RFID_NO,NUM=COUNT(*) FROM LES.TI_MID_RFID_Read_Lable A
				JOIN LES.TI_MID_RFID_LPN_RFIDLIST B ON A.FID = B.FID
				JOIN LES.TT_CMM_WM_RFID_DOOR C ON A.DOOR_NO = C.RFID_DOOR_NO AND C.WM_NO IS NOT NULL AND C.ZONE_NO IS NOT NULL
				WHERE A.FID = @FID
				GROUP BY C.WM_NO,C.ZONE_NO,B.RFID_NO
			)A
			--找料箱所在的原仓库还有问题，DETAIL中BARCODE_DATA多条时存在多个仓库
			JOIN LES.TT_SPM_DELIVERY_RUNSHEET_BARCODE B ON A.RFID_NO = B.RFID_NO
			JOIN LES.TT_RPM_PACKAGE_TRAN_DETAIL C ON B.BARCODE_DATA = C.BARCODE_DATA
			WHERE 
			B.RFID_NO = @FID			
			
			--入库，存在直接更新数量
			UPDATE A SET A.STOCK = A.STOCK + B.NUM FROM [LES].[TM_RPM_PACKAGE_STOCKS] A
			JOIN #STOCK_NUM B ON A.WM_NO = B.IN_WMNO AND A.ZONE_NO = B.IN_ZONENO AND A.PACKAGE_NO = B.PACKAGE_NO
			--入库，不存在新增
			INSERT INTO [LES].[TM_RPM_PACKAGE_STOCKS](PACKAGE_NO,PLANT,WM_NO,ZONE_NO,STOCK_TYPE,STOCK_STATE,DLOC,STOCK,CREATE_USER,CREATE_DATE)
			SELECT A.PACKAGE_NO,'9110',A.IN_WMNO,A.IN_ZONENO,3,1,NULL,A.NUM,'RFIDWebService',GETDATE() FROM #STOCK_NUM A
			WHERE NOT EXISTS 
			(
				SELECT A.PACKAGE_NO FROM [LES].[TM_RPM_PACKAGE_STOCKS] A
				JOIN #STOCK_NUM B ON A.WM_NO = B.IN_WMNO AND A.ZONE_NO = B.IN_ZONENO AND A.PACKAGE_NO = B.PACKAGE_NO
			)
			    
	 END
	 ELSE IF @TYPE = 4 --空箱返3PL
	 BEGIN
			--解除标签与TT_SPM_DELIVERY_RUNSHEET_BARCODE的绑定
			UPDATE B SET B.RFID_NO = NULL FROM LES.TI_MID_RFID_LPN_RFIDLIST A
			INNER JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] B ON A.RFID_NO = B.RFID_NO
			WHERE A.FID = @FID
	 END
END