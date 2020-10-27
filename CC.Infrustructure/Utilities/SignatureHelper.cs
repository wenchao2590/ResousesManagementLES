using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Infrustructure.Utilities
{
    public class SignatureHelper
    {
        /// <summary>
        /// 计算一个字符串的特征值
        /// <remarks>
        /// 一个长字符串的比较非常缓慢，通过MD5算法转换成一个较小的字符串进行比较，提高比较性能
        /// </remarks>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetSignature(string text)
        {
            MD5CryptoServiceProvider md5 = null;
            try
            {

                md5 = new MD5CryptoServiceProvider();

                byte[] inputBytes = Encoding.UTF8.GetBytes(text);
                byte[] hash = md5.ComputeHash(inputBytes);

                return Convert.ToBase64String(hash);
            }
            finally
            {
                if(md5!=null)
                {
                    md5.Dispose();
                }
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Plant"></param>
        /// <param name="Model"></param>
        /// <param name="HandKeptRecord"></param>
        /// <param name="InternalColor"></param>
        /// <param name="ExteriorColor"></param>
        /// <param name="PartOption"></param>
        /// <param name="PartNo"></param>
        /// <param name="Vws"></param>
        /// <param name="Vorserie"></param>
        /// <param name="ModelYear"></param>
        /// <param name="AmountFlag"></param>
        /// <param name="StartProductionDate"></param>
        /// <param name="EndProductionDate"></param>
        /// <returns></returns>
        public static string CalTermalLogicalPk(string Plant, string Model, string HandKeptRecord,
                                                 string InternalColor, string ExteriorColor, string PartOption,
                                                 string PartNo, string Vws, string Vorserie,
                                                 string ModelYear, string AmountFlag,
                                                 DateTime? StartProductionDate, DateTime? EndProductionDate)
        {
            string logicalPk = (string.IsNullOrEmpty(Plant) ? string.Empty : Plant.Trim().ToUpper())
           + (string.IsNullOrEmpty(Model) ? string.Empty : Model.Trim().ToUpper())
           + (string.IsNullOrEmpty(HandKeptRecord) ? string.Empty : HandKeptRecord.Trim().ToUpper())
           + (string.IsNullOrEmpty(InternalColor) ? string.Empty : InternalColor.Trim().ToUpper())
           + (string.IsNullOrEmpty(ExteriorColor) ? string.Empty : ExteriorColor.Trim().ToUpper())
           + (string.IsNullOrEmpty(PartOption) ? string.Empty : PartOption.Trim().ToUpper())
           + (string.IsNullOrEmpty(PartNo) ? string.Empty : PartNo.Trim().ToUpper())
           + (string.IsNullOrEmpty(Vws) ? string.Empty : Vws.Trim().ToUpper())
           + Vorserie
           + (string.IsNullOrEmpty(ModelYear) ? string.Empty : ModelYear.Trim().ToUpper())
           + (string.IsNullOrEmpty(AmountFlag) ? string.Empty : AmountFlag.Trim().ToUpper()
           + (StartProductionDate.HasValue ? StartProductionDate.Value.ToString("yyyyMMdd") : string.Empty)
           + (EndProductionDate.HasValue ? EndProductionDate.Value.ToString("yyyyMMdd") : string.Empty));

            return GetSignature(logicalPk);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Plant"></param>
        /// <param name="Model"></param>
        /// <param name="HandKeptRecord"></param>
        /// <param name="InternalColor"></param>
        /// <param name="ExteriorColor"></param>
        /// <param name="PartOption"></param>
        /// <param name="PartNo"></param>
        /// <param name="Vws"></param>
        /// <param name="Vorserie"></param>
        /// <param name="ModelYear"></param>
        /// <param name="AmountFlag"></param>
        /// <returns></returns>
        public static string CalTermalBusinessPk(string Plant, string Model, string HandKeptRecord, 
                                                 string InternalColor, string ExteriorColor,string PartOption,
                                                 string PartNo, string Vws, string Vorserie,
                                                 string ModelYear,string AmountFlag)
        {
            
             string businessPk = (string.IsNullOrEmpty(Plant) ? string.Empty : Plant.Trim().ToUpper())
            + (string.IsNullOrEmpty(Model) ? string.Empty : Model.Trim().ToUpper())
            + (string.IsNullOrEmpty(HandKeptRecord) ? string.Empty : HandKeptRecord.Trim().ToUpper())
            + (string.IsNullOrEmpty(InternalColor) ? string.Empty : InternalColor.Trim().ToUpper())
            + (string.IsNullOrEmpty(ExteriorColor) ? string.Empty : ExteriorColor.Trim().ToUpper())
            + (string.IsNullOrEmpty(PartOption) ? string.Empty : PartOption.Trim().ToUpper())
            + (string.IsNullOrEmpty(PartNo) ? string.Empty : PartNo.Trim().ToUpper())
            + (string.IsNullOrEmpty(Vws) ? string.Empty : Vws.Trim().ToUpper())
            + Vorserie
             + (string.IsNullOrEmpty(ModelYear) ? string.Empty : ModelYear.Trim().ToUpper())
            + (string.IsNullOrEmpty(AmountFlag) ? string.Empty : AmountFlag.Trim().ToUpper());

             return GetSignature(businessPk);
        }

        public static string CalTermalBusinessPkWithOutMD5(string Plant, string Model, string HandKeptRecord,
                                         string InternalColor, string ExteriorColor, string PartOption,
                                         string PartNo, string Vws, string Vorserie,
                                         string ModelYear, string AmountFlag)
        {

            string businessPk = (string.IsNullOrEmpty(Plant) ? string.Empty : Plant.Trim().ToUpper())
           + (string.IsNullOrEmpty(Model) ? string.Empty : Model.Trim().ToUpper())
           + (string.IsNullOrEmpty(HandKeptRecord) ? string.Empty : HandKeptRecord.Trim().ToUpper())
           + (string.IsNullOrEmpty(InternalColor) ? string.Empty : InternalColor.Trim().ToUpper())
           + (string.IsNullOrEmpty(ExteriorColor) ? string.Empty : ExteriorColor.Trim().ToUpper())
           + (string.IsNullOrEmpty(PartOption) ? string.Empty : PartOption.Trim().ToUpper())
           + (string.IsNullOrEmpty(PartNo) ? string.Empty : PartNo.Trim().ToUpper())
           + (string.IsNullOrEmpty(Vws) ? string.Empty : Vws.Trim().ToUpper())
           + Vorserie
            + (string.IsNullOrEmpty(ModelYear) ? string.Empty : ModelYear.Trim().ToUpper())
           + (string.IsNullOrEmpty(AmountFlag) ? string.Empty : AmountFlag.Trim().ToUpper());

            return businessPk;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="Plant"></param>
        /// <param name="PartNo"></param>
        /// <param name="AssemblyLine"></param>
        /// <param name="Location"></param>
        /// <param name="StartEffectiveDate"></param>
        /// <param name="ZpFlag"></param>
        /// <returns></returns>
        public static string CalInhouseLogicalPk(string Model, string Plant, string PartNo, 
                                                 string AssemblyLine, string Location,
                                                 DateTime? StartEffectiveDate, string ZpFlag)
        {
            string logicalPk = (string.IsNullOrEmpty(Model) ? string.Empty : Model.Trim().ToUpper())
                 + (string.IsNullOrEmpty(Plant) ? string.Empty :Plant.Trim().ToUpper())
                 + (string.IsNullOrEmpty(PartNo) ? string.Empty :PartNo.Trim().ToUpper())
                 + (string.IsNullOrEmpty(AssemblyLine) ? string.Empty :AssemblyLine.Trim().ToUpper())
                 + (string.IsNullOrEmpty(Location) ? string.Empty :Location.Trim().ToUpper())
                 + (StartEffectiveDate.HasValue ? StartEffectiveDate.Value.ToString("yyyyMMdd") : string.Empty)
                 + (string.IsNullOrEmpty(ZpFlag) ? string.Empty :ZpFlag.Trim().ToUpper());

            return GetSignature(logicalPk);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Plant"></param>
        /// <param name="PartNo"></param>
        /// <param name="AssemblyLine"></param>
        /// <param name="Model"></param>
        /// <param name="SupplierNum"></param>
        /// <param name="PurchaseStyle"></param>
        /// <returns></returns>
        public static string CalInboundLogicalPk(string Plant, string PartNo, string AssemblyLine,
                                                 string Model, string SupplierNum, string PurchaseStyle)
        {

            string logicalPk = (string.IsNullOrEmpty(Plant) ? string.Empty : Plant.Trim().ToUpper())
                  + (string.IsNullOrEmpty(PartNo) ? string.Empty : PartNo.Trim().ToUpper())
                  + (string.IsNullOrEmpty(AssemblyLine) ? string.Empty : AssemblyLine.Trim().ToUpper())
                  + (string.IsNullOrEmpty(Model) ? string.Empty : Model.Trim().ToUpper())
                  + (string.IsNullOrEmpty(SupplierNum) ? string.Empty : SupplierNum.Trim().ToUpper())
                  + (string.IsNullOrEmpty(PurchaseStyle) ? string.Empty : PurchaseStyle.Trim().ToUpper())
                  ;

            return GetSignature(logicalPk);
        }

    }
}
