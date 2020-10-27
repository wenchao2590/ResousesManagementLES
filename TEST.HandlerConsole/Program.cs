using BLL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
//using WS.PDA.BaseService;
using BLL.SYS;
using System.Data;
using DAL.SYS;

namespace TEST.HandlerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.Title = "HandlerConsole";
            /////
            //string flowCode = "Receive";
            //string taskTypeCode = "ScanReceiveOrderHandler";
            //ITaskHandler handler = null;
            //IMessage msgHandler = new MessageHandler();
            //TaskContext context = null;
            //while (true)
            //{
            //    handler = TaskHandler.CreateHandler(taskTypeCode);
            //    ///
            //    context = handler.CreateTask(taskTypeCode, flowCode, context);
            //    ///
            //    context.userInputContext = msgHandler.AlertMessage(context);
            //    ///
            //    taskTypeCode = handler.TaskHandle(ref context);
            //    ///
            //    msgHandler.AlertMessage(context);
            //    ///
            //    if (string.IsNullOrEmpty(taskTypeCode)) break;
            //}
            
            string jsonstr = "{ 'Import':{ 'orderno':'A201805150008'},'Export':{ },'Tables':{ },'ErrCode':'','Msg':'','Token':'58b97fa71f254573af67deb29c3c6e80','Language':'cn-ch','LoginUser':'admin','RoleFid':'f04a3b27-b674-4a5e-bd4f-b2e2889318e4','UserFid':'731f7d72-0884-4c13-a73f-5f1c1aec8660','Result':false,'DataServiceUrl':null}";
            //"{ 'Import':{ 'cardnos':\"['BF00000284','BF00000285']\",'opUser':'admin'},'Export':{ },'Tables':{ },'ErrCode':'','Msg':'','Token':'58b97fa71f254573af67deb29c3c6e80','Language':'cn-ch','LoginUser':'admin','RoleFid':'f04a3b27-b674-4a5e-bd4f-b2e2889318e4','UserFid':'731f7d72-0884-4c13-a73f-5f1c1aec8660','Result':false,'DataServiceUrl':null}";
            BaseDataInfo baseinfo = JsonConvert.DeserializeObject<BaseDataInfo>(jsonstr);
            //BaseDataInfo baseinfo = new BaseDataInfo();
            //baseinfo.Import.Add("cardnos", "'BF00000284','BF00000285'");

            BaseDataInfo ys = new BaseService().GetReceiveAndOutOrder(baseinfo);

            //string jsonData = JsonConvert.SerializeObject(model);
            //BaseDataInfo baseinfo = new BaseDataInfo();
            //baseinfo.Export.Add("Info", jsonData);
            //string jsonDataStr = JsonConvert.SerializeObject(baseinfo);
            //KanbanCardBLL temp = new KanbanCardBLL();
            //temp.SelectInfoByCardNo("A0001");

            ///ZheliYang 20180502
            //DataSet dataSet = CommonDAL.ExecuteDataSetBySql("select * from [LES].[TM_MPM_KANBAN_CARD] with(nolock)");
            //List<string> files = new PrintConfigBLL().CreatePrintFiles("KANBAN_CARD", dataSet, @"D:\PROJECTS\JXINFO\LES\CODE\UI.WEB\");
            ///
        }
    }
}
