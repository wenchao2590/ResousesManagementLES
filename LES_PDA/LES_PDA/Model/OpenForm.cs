using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LES_PDA.Forms;

namespace LES_PDA
{
    public class OpenForm
    {
        /// <summary>
        /// 入库
        /// </summary>
        public  void Storage()
        {
            //frm_Storage Storage = new frm_Storage();
            //Storage.Show();
        }

        /// <summary>
        /// 返厂/返线
        /// </summary>
        public  void ReturnLine()
        {
            //frmReturnLine Return = new frmReturnLine();
            //Return.Show();
        }

        /// <summary>
        /// 冲压成品出库
        /// </summary>
        public void CyProduct()
        {
            //FrmcyProduct Product = new FrmcyProduct();
            //Product.ShowDialog();
        }

        /// <summary>
        /// 装箱
        /// </summary>
        public void Packing()
        {
            //frm_Packing Pack = new frm_Packing();
            //Pack.Show();
        }

        /// <summary>
        /// 出库
        /// </summary>
        public void BindSheet()
        {
            //frmBindSheet Sheet = new frmBindSheet();
            //Sheet.Show();
        }

        /// <summary>
        /// 绑定/解绑定
        /// </summary>
        public void Freeze()
        {
            //frmFreeze Free = new frmFreeze();
            //Free.Show();
        }

        /// <summary>
        /// 冻结/解冻结
        /// </summary>
        public void Hold()
        {
            //frmHold hd = new frmHold();
            //hd.Show();
            //FrmUNFreeze UNFreeze = new FrmUNFreeze();
            //UNFreeze.ShowDialog();
        }

        /// <summary>
        /// 空箱扫描
        /// </summary>
        public void FrmPatrolLine()
        {
            //FrmOutBound bound = new FrmOutBound();
            //bound.ShowDialog();
        }

        /// <summary>
        /// 出库单查询
        /// </summary>
        public void OutDate()
        {
            //frmOutDate date = new frmOutDate();
            //date.ShowDialog();
        }

        /// <summary>
        /// 装箱/拆箱/合并
        /// </summary>
        public void PackUNMerger()
        {
            //frmPackUNMerger unmerger = new frmPackUNMerger();
            //unmerger.Show();
        }

        /// <summary>
        /// 缺陷管理
        /// </summary>
        public void Quality()
        {
            //frmQuality lity = new frmQuality();
            //lity.Show();
        }

        /// <summary>
        /// 主界面
        /// </summary>
        public void Main()
        {
            //Main mi = new Main();
            //mi.ShowDialog();
        }

        /// <summary>
        /// 收货
        /// </summary>
        public void Receipt()
        {
            //FrmReceipt Receipt = new FrmReceipt();
            //Receipt.ShowDialog();
        }

        /// <summary>
        /// 外检
        /// </summary>
        public void ExternalExamination()
        {
            //FrmExternalExamination ExternalExamination = new FrmExternalExamination();
            //ExternalExamination.ShowDialog();
        }

        /// <summary>
        /// 拣货
        /// </summary>
        public void Picking()
        {
            //FrmPicking Pocking = new FrmPicking();
            //Pocking.ShowDialog();
        }

        /// <summary>
        /// 上架
        /// </summary>
        public void Added()
        {
            //FrmAdded added = new FrmAdded();
            //added.ShowDialog();
        }

        /// <summary>
        /// 库存冻结
        /// </summary>
        public void StockFreeze()
        {
            //FrmStockFreeze StockFreeze = new FrmStockFreeze();
            //StockFreeze.ShowDialog();
        }
        /// <summary>
        /// 库存移动
        /// </summary>
        public void StockMovement()
        {
            //FrmStockMovement StockMovement = new FrmStockMovement();
            //StockMovement.ShowDialog();
        }

        /// <summary>
        /// 库存状态变更
        /// </summary>
        public void QualityStatusChange()
        {
            //FrmQualityStatusChange QualityStatusChange = new FrmQualityStatusChange();
            //QualityStatusChange.ShowDialog();
        }
    }
}
