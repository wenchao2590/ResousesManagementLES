using BLL.LES;
using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimulationoOverPoint
{
    public partial class SimulationoOverPoint : Form
    {
        public SimulationoOverPoint()
        {
            InitializeComponent();
        }

        private void SimulationoOverPoint_Load(object sender, EventArgs e)
        {
            this.button2.Visible = false;
            this.button3.Visible = false;
            this.button4.Visible = false;
        }

        /// <summary>
        /// 确认按钮点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string orderNo = this.textBox1.Text;
            if(!string.IsNullOrEmpty(orderNo))
            {
                PullOrdersInfo pullOrdersInfo = new PullOrdersBLL().GetInfoByOrderNo(orderNo);
                if(pullOrdersInfo == null)
                {
                    MessageBox.Show("生产单号不存在！");
                }
                else
                {
                    new  StocksBLL().CheckMaterialStock(orderNo, pullOrdersInfo.AssemblyLine, "SimulationoOverPoint", true);
                    this.button2.Visible = true;
                    this.button3.Visible = true;
                    this.button4.Visible = true;
                }
            }
        }

        /// <summary>
        /// 采集点 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string orderNo = this.textBox1.Text;
            PullOrdersInfo pullOrdersInfo = new PullOrdersBLL().GetInfoByOrderNo(orderNo);
            MesVehiclePointScanInfo mesVehiclePointScanInfo = new MesVehiclePointScanInfo();
            mesVehiclePointScanInfo.Fid = Guid.NewGuid();
            ///ENTERPRISE 	工厂编号
            mesVehiclePointScanInfo.Enterprise = new PlantBLL().GetSapPlantByPlantCode(pullOrdersInfo.Werk); 
            ///车间
            mesVehiclePointScanInfo.SiteNo = null;
            ///AREA_NO     生产线编号
            mesVehiclePointScanInfo.AreaNo = new AssemblyLineBLL().GetSapAssemblyLineByAssemblyLine(pullOrdersInfo.AssemblyLine);
            //UNIT_NO     采集点编号
            mesVehiclePointScanInfo.UnitNo = this.button2.Text;
            //DMS_SEQ     过点顺序号
            mesVehiclePointScanInfo.DmsSeq = Convert.ToInt32(pullOrdersInfo.VehicleOrder);
            //DMS_NO      计划订单号
            mesVehiclePointScanInfo.DmsNo = pullOrdersInfo.OrderNo;
            mesVehiclePointScanInfo.ProcessFlag = (int)ProcessFlagResuitsConstants.Created;
            //SEND_TIME 	发送时间
            mesVehiclePointScanInfo.SendTime = DateTime.Now;
            mesVehiclePointScanInfo.ValidFlag = true;
            mesVehiclePointScanInfo.CreateDate= DateTime.Now;
            mesVehiclePointScanInfo.CreateUser = "SimulationoOverPoint";
            label2.Text= new MesVehiclePointScanBLL().InsertInfo(mesVehiclePointScanInfo).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string orderNo = this.textBox1.Text;
            PullOrdersInfo pullOrdersInfo = new PullOrdersBLL().GetInfoByOrderNo(orderNo);
            MesVehiclePointScanInfo mesVehiclePointScanInfo = new MesVehiclePointScanInfo();
            mesVehiclePointScanInfo.Fid = Guid.NewGuid();
            ///ENTERPRISE 	工厂编号
            mesVehiclePointScanInfo.Enterprise = new PlantBLL().GetSapPlantByPlantCode(pullOrdersInfo.Werk);
            ///车间
            mesVehiclePointScanInfo.SiteNo = null;
            ///AREA_NO     生产线编号
            mesVehiclePointScanInfo.AreaNo = new AssemblyLineBLL().GetSapAssemblyLineByAssemblyLine(pullOrdersInfo.AssemblyLine);
            //UNIT_NO     采集点编号
            mesVehiclePointScanInfo.UnitNo = this.button3.Text;
            //DMS_SEQ     过点顺序号
            mesVehiclePointScanInfo.DmsSeq = Convert.ToInt32(pullOrdersInfo.VehicleOrder);
            //DMS_NO      计划订单号
            mesVehiclePointScanInfo.DmsNo = pullOrdersInfo.OrderNo;
            mesVehiclePointScanInfo.ProcessFlag = (int)ProcessFlagResuitsConstants.Created;
            //SEND_TIME 	发送时间
            mesVehiclePointScanInfo.SendTime = DateTime.Now;
            mesVehiclePointScanInfo.ValidFlag = true;
            mesVehiclePointScanInfo.CreateDate = DateTime.Now;
            mesVehiclePointScanInfo.CreateUser = "SimulationoOverPoint";
            label3.Text = new MesVehiclePointScanBLL().InsertInfo(mesVehiclePointScanInfo).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string orderNo = this.textBox1.Text;
            PullOrdersInfo pullOrdersInfo = new PullOrdersBLL().GetInfoByOrderNo(orderNo);
            MesVehiclePointScanInfo mesVehiclePointScanInfo = new MesVehiclePointScanInfo();
            mesVehiclePointScanInfo.Fid = Guid.NewGuid();
            ///ENTERPRISE 	工厂编号
            mesVehiclePointScanInfo.Enterprise = new PlantBLL().GetSapPlantByPlantCode(pullOrdersInfo.Werk);
            ///车间
            mesVehiclePointScanInfo.SiteNo = null;
            ///AREA_NO     生产线编号
            mesVehiclePointScanInfo.AreaNo = new AssemblyLineBLL().GetSapAssemblyLineByAssemblyLine(pullOrdersInfo.AssemblyLine);
            //UNIT_NO     采集点编号
            mesVehiclePointScanInfo.UnitNo = this.button4.Text;
            //DMS_SEQ     过点顺序号
            mesVehiclePointScanInfo.DmsSeq = Convert.ToInt32(pullOrdersInfo.VehicleOrder);
            mesVehiclePointScanInfo.ProcessFlag = (int)ProcessFlagResuitsConstants.Created;
            //DMS_NO      计划订单号
            mesVehiclePointScanInfo.DmsNo = pullOrdersInfo.OrderNo;
            //SEND_TIME 	发送时间
            mesVehiclePointScanInfo.SendTime = DateTime.Now;
            mesVehiclePointScanInfo.ValidFlag = true;
            mesVehiclePointScanInfo.CreateDate = DateTime.Now;
            mesVehiclePointScanInfo.CreateUser = "SimulationoOverPoint";
            label4.Text = new MesVehiclePointScanBLL().InsertInfo(mesVehiclePointScanInfo).ToString();
        }
    }
}
