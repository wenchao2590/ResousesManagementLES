using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Infrustructure.Utilities
{
    /// <summary>
    /// A class with two static methods <see cref="BindObjectToControls"/> and <see cref="BindControlsToObject"/>
    /// used to bind the values of and object's properties with web <see cref="Control"/>s that have their
    /// ID property set to the name of the object property.
    /// </summary>
    public class FormBinding
    {
        /// <summary>
        /// Binds an object's properties to <see cref="Control"/>s with the same ID as the propery name. 
        /// </summary>
        /// <param name="obj">The object whose properties are being bound to forms Controls</param>
        /// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
        public static void BindObjectToControls(object obj, Control container)
        {
            if (obj == null) return;

            // Get the properties of the business object
            //
            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {

                Control control = FindControlRecursive(container, objProperty.Name);
                //Control control = container.FindControl(objProperty.Name);
                // handle ListControls (DropDownList, CheckBoxList, RadioButtonList)
                //
                if (control == null)
                    continue;

                // TODO: alex@20080704: ������һ������bug
                // ���������control��ExtDropDownList,����plant��workshop��parent control
                // ��ǰ��objType.GetProperties()��workshop������plantǰ
                // ��workshop�ȱ�����, ��ʱplant��û��, ����workshop��������Ч
                // Ŀǰ����ʱ���������, ȷ����ʵ���ඨ�������, parent control��Ӧ������д��child control��Ӧ������ǰ��
                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;

                    string propertyValue = null;
                    if (objProperty.GetValue(obj, null) != null)
                    {
                        propertyValue = objProperty.GetValue(obj, null).ToString();
                        if ((propertyValue == "sps") || (propertyValue == "jis") || (propertyValue == "pcs"))
                        listControl.SelectedValue = propertyValue.ToUpper();
                        else
                            listControl.SelectedValue = propertyValue;
                    }

                    //���ǵ�propertyValue����Ϊbool�͵�ֵ,�ڴ�ת��ֵ���жϡ�
                    //luchao 2011-07-15
                    //if (propertyValue == "True")
                    //{
                    //    listControl.SelectedValue = "0";
                    //    propertyValue = listControl.SelectedValue;
                    //}
                    //else
                    //{
                    //    listControl.SelectedValue = "1";
                    //    propertyValue = listControl.SelectedValue;
                    //} 


                    listControl.DataBind();


                    //���ǵ����ܳ��ֵĴ�Сд��ƥ��������������һ�����⴦��
                    //xuehaijun 2011-05-07 
                    if ((propertyValue == "sps") || (propertyValue == "jis") || (propertyValue == "pcs"))
                        continue;
                    if (listControl.SelectedValue != propertyValue && !string.IsNullOrEmpty(propertyValue))
                    {
                        //��ֵ�����߻�����ȣ����ô�Сд�����滻
                        listControl.SelectedValue = (listControl.SelectedValue == propertyValue.ToUpper()) ? propertyValue.ToUpper() : propertyValue.ToLower();

                        #region Added by ���Ѿ������ListControl�б����Ѿ�û�ж�Ӧ��ֵ������ֳ���������ʾֵ�޷�ȡ����
                        if (listControl.SelectedValue != propertyValue)
                        {
                            listControl.Items.Add("");
                            listControl.Text = "";
                        }
                        #endregion
                    }
                }
                else
                {
                    // get the properties of the control
                    //
                    Type controlType = control.GetType();
                    PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                    // test for common properties
                    //
                    bool success = false;
                    success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool));

                    if (!success)
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime));

                    if (!success)
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String));

                    if (!success)
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String));

                }
            }
        }

        /// <summary>
        /// Looks for a property name and type on a control and attempts to set it to the value in an object's property 
        /// of the same name.
        /// </summary>
        /// <param name="obj">The object whose properties are being retrieved</param>
        /// <param name="objProperty">The property of the object being retrieved</param>
        /// <param name="control">The control whose ID matches the object's property name.</param>
        /// <param name="controlPropertiesArray">An array of the control's properties</param>
        /// <param name="propertyName">The name of the Control property being set</param>
        /// <param name="type">The correct type for the Control property</param>
        /// <returns>Boolean for whether the property was found and set</returns>
        private static bool FindAndSetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            // iterate through control properties
            //
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // check for matching name and type
                //
                if (controlProperty.Name == propertyName && controlProperty.PropertyType == type)
                {
                    // set the control's property to the business object property value
                    //
                    controlProperty.SetValue(control, ChangeType(objProperty.GetValue(obj, null), type), null);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Binds your the values in <see cref="Control"/>s to a business object.
        /// </summary>
        /// <param name="obj">The object whose properties are being bound to Control values</param>
        /// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
        public static object BindControlsToObject(object obj, Control container)
        {
            if (obj == null) return obj;

            // Get the properties of the business object
            //			
            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {

                Control control = FindControlRecursive(container, objProperty.Name);

                if (control == null)
                    continue;

                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;
                    if (listControl.SelectedItem != null)
                    {
                        objProperty.SetValue(obj, ChangeType(listControl.SelectedItem.Value, objProperty.PropertyType), null);
                    }

                    //2012-02-06 Modified by Orange Cheng
                    //else
                    //{
                    //    objProperty.SetValue(obj, ChangeType(" ", objProperty.PropertyType), null);
                    //}

                }
                else
                {
                    // get the properties of the control
                    //
                    Type controlType = control.GetType();
                    PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                    // test for common properties
                    //
                    bool success = false;
                    success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool));

                    if (!success)
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime));

                    if (!success)
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String));

                    if (!success)
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String));
                }
            }

            return obj;
        }

        public static object ChangeType(object value, Type conversionType)
        {
            //�������������Nullable������Ҫ���⴦��
            if (conversionType.IsGenericType &&
                conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {

                if (value == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }

                System.ComponentModel.NullableConverter nullableConverter
                    = new System.ComponentModel.NullableConverter(conversionType);

                conversionType = nullableConverter.UnderlyingType;
            }

            #region Set value to null if the value is int.Minvalue
            // Modified by PuGong 2008-03-15
            try
            {
                int tmpValue = 0;
                if (value == null)
                {
                    return null;
                }
                value = value.ToString().Trim();

                if (int.TryParse(value.ToString(), out tmpValue))
                {
                    if (tmpValue == int.MinValue)
                        value = null;
                }
            }
            catch { }
            #endregion

            return Convert.ChangeType(value, conversionType);
        }


        /// <summary>
        /// Looks for a property name and type on a control and attempts to set it to the value in an object's property 
        /// of the same name.
        /// </summary>
        /// <param name="obj">The object whose properties are being set</param>
        /// <param name="objProperty">The property of the object being set</param>
        /// <param name="control">The control whose ID matches the object's property name.</param>
        /// <param name="controlPropertiesArray">An array of the control's properties</param>
        /// <param name="propertyName">The name of the Control property being retrieved</param>
        /// <param name="type">The correct type for the Control property</param>
        /// <returns>Boolean for whether the property was found and retrieved</returns>
        private static bool FindAndGetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            // iterate through control properties
            //
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // check for matching name and type
                //
                if (controlProperty.Name == propertyName && controlProperty.PropertyType == type)
                {
                    // set the control's property to the business object property value
                    //
                    try
                    {
                        objProperty.SetValue(obj, ChangeType(controlProperty.GetValue(control, null), objProperty.PropertyType), null);
                        return true;
                    }
                    catch
                    {
                        // the data from the form control could not be converted to objProperty.PropertyType
                        //
                        return false;
                    }
                }
            }
            return false;
        }


        public static Control FindControlRecursive(Control Root, string ID)
        {

            if (Root.ID == ID)

                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, ID);

                if (FoundCtl != null)

                    return FoundCtl;
            }

            return null;
        }

    }
}
