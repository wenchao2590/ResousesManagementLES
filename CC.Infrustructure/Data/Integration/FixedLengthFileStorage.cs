using System;
using Infrustructure.Utilities;
using Infrustructure.Logging;

namespace Infrustructure.Data.Integration
{
	internal class FixedLengthFileStorage : DelimitedFileStorage
	{
		public FixedLengthFileStorage(string providername) : base(providername)
		{
		}

		public override DataItem GetDataItem(string row, DataSchema dataSchema, bool isSource)
		{
			Array.Sort(dataSchema.Fields, new DataSchemaFieldComparer());
			try
			{
				DataItem dataitem = new DataItem(dataSchema);
				foreach (DataSchemaField field in dataSchema.Fields)
				{
					if (field.Skip)
						row = StringUtil.GetSubByte(row, encoding, int.Parse(field.GetAnyAttributeValue("length")));
					else
					{
						DataItemField itemfield;
						if (isSource)
							itemfield = dataitem.GetSourceField(field.Source);
						else
							itemfield = dataitem.GetDestinationField(field.Destination);

                        string replaced = field.GetAnyAttributeValue("replaced");
                        string defaultvalue = field.GetAnyAttributeValue("default");

                        string parsev;
                        if (!StringUtil.TryGetSubByte(row, encoding, 0, int.Parse(field.GetAnyAttributeValue("length")), out parsev))
                        {
                            switch (dataSchema.TransferMode)
                            {
                                case DataFixMode.Skip:
                                    Logger.Instance.Info(this, row);
                                    return null;
                                case DataFixMode.Fix:
                                    itemfield.Value = string.Empty;
                                    break;
                                case DataFixMode.RaiseError:
                                    throw new ArgumentException(
                                        string.Format("row has baddata.{0}row:[{1}]", Environment.NewLine, row));
                                default:
                                    throw new ArgumentOutOfRangeException(
                                        string.Format("Invalid transfer mode [{0}] in schema, requires one of 'skip,fix,raiseerror'",
                                                      dataSchema.TransferMode));
                            }
                        }
                        else
                            itemfield.Value = parsev;

                        // 如果存在被替换值和默认值,则将被替换值替换为默认值
                        if (!string.IsNullOrEmpty(itemfield.Value as string) && !string.IsNullOrEmpty(replaced) && replaced.IndexOf(itemfield.Value.ToString()) != -1 &&
                            defaultvalue != null)
                            itemfield.Value = defaultvalue;
                        // 如果该项验证失败, 且为DataFixMode.Fix, 且有默认值, 则以默认值代替
                        if (!string.IsNullOrEmpty(dataSchema.TransferMode) && dataSchema.TransferMode.Equals("fix", StringComparison.InvariantCultureIgnoreCase) && defaultvalue != null)
                        {
                            ValidationResults vr = new ValidationResults();
                            itemfield.DoValidate(vr);
                            if (!vr.IsValid && itemfield.Value as string != defaultvalue)
                                itemfield.Value = defaultvalue;
                        }

                        try
                        {
                            Utilities.AdjustDataItemFieldValue(itemfield);
                        }
                        catch (System.Exception ex)
                        {
                            Logger.Instance.Error(this, ex);
                            return null;
                        }
						row = StringUtil.GetSubByte(row, encoding, int.Parse(field.GetAnyAttributeValue("length")));
					}
				}
				return dataitem;
			}
			catch (System.Exception ex)
			{
				throw new System.Exception(row, ex);
			}
		}
	}
}