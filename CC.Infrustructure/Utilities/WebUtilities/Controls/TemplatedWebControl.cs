//-------------------------------------------------------------------
//版权所有：版权所有(C) 2006，Microsoft(China) Co.,LTD
//系统名称：GMCC-ADC
//文件名称：TemplatedWebControl
//模块名称：
//模块编号：
//作　　者：SuChuanyi
//完成日期：12/11/2006 14:14:06
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：  
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Infrustructure.Utilities.WebUtility.Controls
{
    /// <summary>
    /// 用户控件后台代码得基类，用于使用户控件跟后台代码实现真正得分离。
    /// 简单得说就是同一功能的用户控件可以拥有不同的界面，但只有一个后台代码，
    /// </summary>
    [
    ParseChildren(true),
    PersistChildren(false),
    ]
	[Obsolete("Never be used.")]
    public abstract class TemplatedWebControl : WebControl, INamingContainer
    {

        #region Composite Controls

        /// <exclude/>
        public override ControlCollection Controls
        {
            get
            {
                this.EnsureChildControls();
                return base.Controls;
            }
        }

        /// <exclude/>
        public override void DataBind()
        {
            this.EnsureChildControls();
        }

        #endregion

        #region External Skin

        /// <summary>
        /// 主题名，目前写死为default
        /// </summary>
        protected virtual string ThemeName
        {
            get
            {
                return "default"; 
            }
        }

        protected virtual string SkinFolder
        {
            get
            {
                return "~/Themes/" + ThemeName + "/Skins/"; //TODO Use correct skin folder
            }
        }

        //用户控件存放的路径
        private String SkinPath
        {
            get
            {
                return this.SkinFolder + ExternalSkinFileName;
            }
        }

        /// <summary>
        /// Gets the name of the skin file to load from
        /// </summary>
        protected virtual String ExternalSkinFileName
        {
            get
            {
                if (SkinName == null)
                    return CreateExternalSkinFileName(null);

                return SkinName;
            }
            set
            {
                SkinName = value;
            }
        }

        string skinName;
        public string SkinName
        {
            get
            {
                return skinName;
            }
            set
            {
                skinName = value;
            }
        }

        protected virtual string CreateExternalSkinFileName(string path)
        {
            return CreateExternalSkinFileName(path, "Skin-" + this.GetType().Name);
        }

        protected virtual string CreateExternalSkinFileName(string path, string name)
        {
            if (path != null && !path.EndsWith("/"))
                path = path + "/";

            return string.Format("{0}{1}.ascx", path, name);
        }

        /// <summary>
        /// 判断皮肤文件夹是否存在
        /// </summary>
        private Boolean SkinFolderExists
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    String folderPath = context.Server.MapPath(this.SkinFolder);
                    return System.IO.Directory.Exists(folderPath);
                }
                return true;
            }
        }

        /// <summary>
        /// 判断皮肤文件是否存在
        /// </summary>
        private Boolean SkinFileExists
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    String filePath = context.Server.MapPath(this.SkinPath);
                    return System.IO.File.Exists(filePath);
                }
                return true;
            }
        }

        /// <summary>
        /// 判断默认皮肤文件是否存在
        /// </summary>
        private Boolean DefaultSkinFileExists
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context != null)
                {
                    String filePath = context.Server.MapPath(this.DefaultSkinPath);
                    return System.IO.File.Exists(filePath);
                }
                return true;
            }
        }

        /// <summary>
        /// 默认皮肤文件路径
        /// </summary>
        protected virtual string DefaultSkinPath
        {
            get
            {
                return "~/Themes/default/Skins/" + ExternalSkinFileName;
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The template used to override the default UI of the control.
        /// </summary>
        /// <remarks>
        /// All serverside controls that are in the default UI must exist and have the same ID's.
        /// </remarks>
        [
        Browsable(false),
        DefaultValue(null),
        Description("TODO SkinTemplate Description"),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public ITemplate SkinTemplate
        {
            get
            {
                return _skinTemplate;
            }
            set
            {
                _skinTemplate = value;
                ChildControlsCreated = false;
            }
        }
        private ITemplate _skinTemplate;

        #endregion

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// No End Span
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            //we don't need a span tag
        }

        protected virtual string ControlText()
        {
            return null;
        }

        public override Page Page
        {
            get
            {
                if (base.Page == null)
                {
                    base.Page = HttpContext.Current.Handler as System.Web.UI.Page;
                }

                return base.Page;
            }
            set
            {
                base.Page = value;
            }
        }


        /// <exclude/>
        public override Control FindControl(string id)
        {
            Control ctrl = base.FindControl(id);
            if (ctrl == null && this.Controls.Count == 1)
            {
                ctrl = this.Controls[0].FindControl(id);
            }
            return ctrl;
        }

        /// <summary>
        /// First choice for skins. The value of Control() text will be interpreted as a skin. The 
        /// primary usage of this feature will be to serve skins from the database
        /// </summary>
        /// <returns></returns>
        protected virtual bool LoadTextBasedControl()
        {
            string text = ControlText();

            if (!string.IsNullOrEmpty(text))
            {
                Control skin = this.Page.ParseControl(text);
                skin.ID = "_";
                this.Controls.Add(skin);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads the skin file from the users current theme
        /// </summary>
        /// <returns></returns>
        protected virtual bool LoadThemedControl()
        {
            if (!string.IsNullOrEmpty(ThemeName) && SkinFolderExists)
            {
                if (SkinFileExists && this.Page != null)
                {
                    Control skin = this.Page.LoadControl(this.SkinPath);
                    skin.ID = "_";
                    this.Controls.Add(skin);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Load the skin as an inline template. By default, this will be the second option
        /// </summary>
        /// <returns></returns>
        protected virtual bool LoadSkinTemplate()
        {
            if (SkinTemplate != null)
            {
                SkinTemplate.InstantiateIn(this);
                return true;
            }

            return false;
        }

        /// <summary>
        /// By default, as a last ditch effort, we will try to load the skin file 
        /// from the default Theme
        /// </summary>
        /// <returns></returns>
        protected virtual bool LoadDefaultThemedControl()
        {
            if (this.Page != null && this.DefaultSkinFileExists)
            {
                Control defaultSkin = this.Page.LoadControl(this.DefaultSkinPath);
                defaultSkin.ID = "_";
                this.Controls.Add(defaultSkin);
                return true;
            }

            return false;
        }

        /// <exclude/>
        protected override void CreateChildControls()
        {
            Controls.Clear();

            // 1) String Control
            Boolean _skinLoaded = LoadTextBasedControl();

            // 2) Inline Template
            if (!_skinLoaded)
            {
                _skinLoaded = LoadSkinTemplate();
            }

            // 3) Themed Control
            if (!_skinLoaded)
            {
                _skinLoaded = LoadThemedControl();
            }

            // 4) Default Control
            if (!_skinLoaded)
            {
                _skinLoaded = LoadDefaultThemedControl();
            }

            // 5) If none of the skin locations were successful, throw.
            if (!_skinLoaded)
            {
                throw new ApplicationException(string.Format("无法找到皮肤文件: {0}", this.GetType().ToString()));
            }

            if (_skinLoaded)
                AttachChildControls();
        }


        /// <summary>
        /// Override this method to attach templated or external skin controls to local references.
        /// </summary>
        /// <remarks>
        /// This will only be called if the non-default skin is used.
        /// </remarks>
        protected abstract void AttachChildControls();

        protected override void Render(HtmlTextWriter writer)
        {
            SourceMarker(true, writer);
            base.Render(writer);
            SourceMarker(false, writer);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        protected void SourceMarker(bool isStart, HtmlTextWriter writer)
        {

            if (isStart)
            {
                writer.WriteLine("<!-- Start: {0} -->", this.GetType());

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(this.SkinPath)))
                    writer.WriteLine("<!-- Skin Path: {0} -->", this.SkinPath);
                else if (SkinTemplate != null)
                    writer.WriteLine("<!-- Inline Skin: {0} -->", true);
                else
                    writer.WriteLine("<!-- Skin Path: {0} -->", this.DefaultSkinPath);

            }
            else
                writer.WriteLine("<!-- End: {0} -->", this.GetType());
        }

        /// <summary>
        /// Identifies the control that fired posback.
        /// </summary>
        public Control GetPostBackControl()
        {
            return GetPostBackControl(this);
        }

        public Control GetPostBackControl(Control container)
        {
            // Note: this is an adapted eggheadcafe.com code.
            //
            Control control = null;

            string ctrlname = Page.Request.Params["__EVENTTARGET"];
            if (ctrlname != null && ctrlname != String.Empty)
            {
                string[] tokens = ctrlname.Split(new char[1] { ':' });
                if (tokens != null && tokens.GetLength(0) > 0)
                    ctrlname = tokens[(tokens.GetLength(0) - 1)];

                control = this.FindControl(ctrlname);
            }
            else
            {
                // If __EVENTTARGET is null, control is a button type and need to 
                // iterate over the form collection to find it
                //
                string ctrlStr = String.Empty;
                Control c = null;
                foreach (string ctl in Page.Request.Form)
                {

                    // Handle ImageButton controls
                    if (ctl.EndsWith(".x") || ctl.EndsWith(".y"))
                    {
                        ctrlStr = ctl.Substring(0, (ctl.Length - 2));
                        c = this.FindControl(ctrlStr);
                    }
                    else
                    {
                        c = this.FindControl(ctl);
                    }
                    if (c is System.Web.UI.WebControls.Button ||
                        c is System.Web.UI.WebControls.ImageButton)
                    {
                        control = c;
                        break;
                    }
                }
            }

            return control;
        }

    }
}
