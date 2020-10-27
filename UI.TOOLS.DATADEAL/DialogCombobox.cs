namespace UI.TOOLS.DATADEAL
{
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    public partial class DialogCombobox : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public DialogCombobox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public DialogCombobox(List<StringValueDatasourceInfo> items)
        {
            InitializeComponent();
            _stringItems = items;
            _guidItems = new List<GuidValueDatasourceInfo>();
        }
        public DialogCombobox(List<GuidValueDatasourceInfo> items)
        {
            InitializeComponent();
            _stringItems = new List<StringValueDatasourceInfo>();
            _guidItems = items;
        }
        /// <summary>
        /// 
        /// </summary>
        List<StringValueDatasourceInfo> _stringItems;
        List<GuidValueDatasourceInfo> _guidItems;
        private string selectedValue;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedValue
        {
            get
            {
                return selectedValue;
            }

            set
            {
                selectedValue = value;
            }
        }

        public Guid SelectedGuidValue
        {
            get
            {
                return selectedGuidValue;
            }

            set
            {
                selectedGuidValue = value;
            }
        }

        private Guid selectedGuidValue;
        /// <summary>
        /// 
        /// </summary>
        private void LoadStringItems()
        {
            cbList.DisplayMember = "StringValue";
            cbList.ValueMember = "ItemValue";
            cbList.DataSource = _stringItems;
        }

        private void LoadGuidItems()
        {
            cbList.DisplayMember = "StringDisplay";
            cbList.ValueMember = "GuidValue";
            cbList.DataSource = _guidItems;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {
            if (_stringItems.Count > 0)
            {
                StringValueDatasourceInfo item = cbList.SelectedItem as StringValueDatasourceInfo;
                selectedValue = item.StringValue;
            }
            if (_guidItems.Count > 0)
            {
                GuidValueDatasourceInfo item = cbList.SelectedItem as GuidValueDatasourceInfo;
                selectedGuidValue = item.GuidValue;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogCombobox_Load(object sender, EventArgs e)
        {
            if (_stringItems.Count > 0)
                LoadStringItems();
            if (_guidItems.Count > 0)
                LoadGuidItems();
        }
    }
}
