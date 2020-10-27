namespace UI.TOOLS.SETUP
{
    using BLL.SYS;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSetup : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormSetup()
        {
            InitializeComponent();
            LoadFunctions();
        }
        /// <summary>
        /// 获取PACKAGE下的目录作为功能模块
        /// </summary>
        private void LoadFunctions()
        {
            string[] dirs = Directory.GetDirectories(Application.StartupPath + @"\PACKAGE");
            List<FunctionInfo> functionInfos = new List<FunctionInfo>();
            foreach (var dir in dirs)
            {
                string dirName = dir.Substring(dir.LastIndexOf(@"\") + 1);
                string[] functionName = dirName.Split(new char[] { '-' });
                if (functionName.Length < 2) continue;
                FunctionInfo functionInfo = new FunctionInfo();
                functionInfo.ModuleName = functionName[0];
                functionInfo.FunctionName = dirName.Substring(functionInfo.ModuleName.Length + 1);
                functionInfos.Add(functionInfo);
            }
            LoadTreeView(functionInfos);
        }
        /// <summary>
        /// 加载功能列表到TREE
        /// </summary>
        /// <param name="functionInfos"></param>
        private void LoadTreeView(List<FunctionInfo> functionInfos)
        {
            var moduleNames = functionInfos.GroupBy(s => new { s.ModuleName })
                                .Select(p => new { p.Key.ModuleName }).ToList();
            foreach (var moduleName in moduleNames)
            {
                TreeNode treeNode = new TreeNode(moduleName.ModuleName);
                List<FunctionInfo> functions = functionInfos.Where(d => d.ModuleName == moduleName.ModuleName).ToList();
                foreach (var function in functions)
                {
                    treeNode.Nodes.Add(function.FunctionName);
                }
                this.treeViewFunction.Nodes.Add(treeNode);
            }
            this.treeViewFunction.CheckBoxes = true;
        }
        /// <summary>
        /// 获取安装所需脚本
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        private List<FunctionScript> GetSetupScripts(string[] fileNames)
        {
            List<FunctionScript> functionScripts = new List<FunctionScript>();
            foreach (string fileName in fileNames)
            {
                FunctionScript functionScript = new FunctionScript();
                string scriptName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                if (scriptName.StartsWith("DB_CREATE_"))
                {
                    functionScript.ScriptSeq = 10;
                    functionScript.ScriptFileName = fileName;
                    functionScript.TableName = scriptName.Replace("DB_CREATE_", string.Empty);
                    functionScripts.Add(functionScript);
                    continue;
                }
                if (scriptName.StartsWith("DELETE_"))
                {
                    functionScript.ScriptSeq = 20;
                    functionScript.ScriptFileName = fileName;
                    functionScript.TableName = scriptName.Replace("DELETE_", string.Empty);
                    functionScripts.Add(functionScript);
                    continue;
                }
                if (scriptName.StartsWith("CREATE_"))
                {
                    functionScript.ScriptSeq = 30;
                    functionScript.ScriptFileName = fileName;
                    functionScript.TableName = scriptName.Replace("CREATE_", string.Empty);
                    functionScripts.Add(functionScript);
                    continue;
                }
            }
            return functionScripts.OrderBy(d => d.ScriptSeq).ToList();
        }
        /// <summary>
        /// 功能卸载
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        private List<FunctionScript> GetUndoSetupScripts(string[] fileNames)
        {
            List<FunctionScript> functionScripts = new List<FunctionScript>();
            foreach (string fileName in fileNames)
            {
                FunctionScript functionScript = new FunctionScript();
                string scriptName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                if (scriptName.StartsWith("DELETE_"))
                {
                    functionScript.ScriptSeq = 20;
                    functionScript.ScriptFileName = fileName;
                    functionScript.TableName = scriptName.Replace("DELETE_", string.Empty);
                    functionScripts.Add(functionScript);
                    continue;
                }
            }
            return functionScripts.OrderBy(d => d.ScriptSeq).ToList();
        }
        /// <summary>
        /// 删库跑路
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        private List<FunctionScript> GetDropScripts(string[] fileNames)
        {
            List<FunctionScript> functionScripts = new List<FunctionScript>();
            foreach (string fileName in fileNames)
            {
                FunctionScript functionScript = new FunctionScript();
                string scriptName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                if (scriptName.StartsWith("DB_DROP_"))
                {
                    functionScript.ScriptSeq = 40;
                    functionScript.ScriptFileName = fileName;
                    functionScript.TableName = scriptName.Replace("DB_DROP_", string.Empty);
                    functionScripts.Add(functionScript);
                    continue;
                }
            }
            return functionScripts.OrderBy(d => d.ScriptSeq).ToList();
        }
        /// <summary>
        /// 一键配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem02_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    DoSettings();
                }
                catch (ThreadAbortException)
                {
                }
            }, null);
        }
        /// <summary>
        /// 执行配置
        /// </summary>
        private void DoSettings()
        {
            Invoke(new Action(() =>
            {
                richTextBoxMessage.Clear();
            }));
            StringBuilder @string = new StringBuilder();
            ///--用户
            @string.AppendLine("if not exists (select * from dbo.TS_SYS_USER with(nolock) " +
                "where [LOGIN_NAME] = 'admin' and [VALID_FLAG] = 1)");
            @string.AppendLine("insert into dbo.TS_SYS_USER " +
                "(FID, LOGIN_NAME, PASSWORD, EMPLOYEE_NAME, USER_STATUS, USER_VALID_TYPE, VALID_FLAG, CREATE_USER, CREATE_DATE) values " +
                "('731F7D72-0884-4C13-A73F-5F1C1AEC8660', 'admin', 'xMpCOKC5I4INzFCab3WEmw==', '系统管理员', 20, 10, 1, '', GETDATE());");
            ///--角色
            @string.AppendLine("if not exists (select * from dbo.TS_SYS_ROLE with(nolock) " +
                "where [ROLE_NAME] = 'administrator' and [VALID_FLAG] = 1)");
            @string.AppendLine("insert into dbo.TS_SYS_ROLE " +
                "(FID, ROLE_NAME, ROLE_TYPE, VALID_FLAG, CREATE_USER, CREATE_DATE) values " +
                "('F04A3B27-B674-4A5E-BD4F-B2E2889318E4', 'administrator', 10, 1, '', GETDATE());");
            ///--组织
            @string.AppendLine("if not exists (select * from dbo.TS_SYS_ORGANIZATION with(nolock) " +
                "where [FID] = '0D175141-E802-4FBC-AA88-3F183313DF50' and [VALID_FLAG] = 1)");
            @string.AppendLine("insert into dbo.TS_SYS_ORGANIZATION " +
                "(FID, PARENT_FID, CODE, [NAME], COMMENTS, ORGANZATION_LEVEL, VALID_FLAG, CREATE_USER, CREATE_DATE) values " +
                "('0D175141-E802-4FBC-AA88-3F183313DF50', NULL, 'BFDA', '福田戴姆勒', '', 0, 1, '', GETDATE());");
            ///--用户角色
            @string.AppendLine("if not exists (select * from dbo.TS_SYS_USER_ROLE with(nolock) " +
                "where [USER_FID] = '731F7D72-0884-4C13-A73F-5F1C1AEC8660' and [ROLE_FID] = 'F04A3B27-B674-4A5E-BD4F-B2E2889318E4' and [VALID_FLAG] = 1)");
            @string.AppendLine("insert into dbo.TS_SYS_USER_ROLE " +
                "(FID, USER_FID, ROLE_FID, ORGANIZATION_FID, VALID_FLAG, CREATE_USER, CREATE_DATE) values " +
                "(NEWID(),'731F7D72-0884-4C13-A73F-5F1C1AEC8660','F04A3B27-B674-4A5E-BD4F-B2E2889318E4',  '0D175141-E802-4FBC-AA88-3F183313DF50', 1, '', GETDATE());");
            try
            {
                CommonBLL.ExecuteNonQueryBySql(@string.ToString());
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
                return;
            }
            string dirName = Application.StartupPath + @"\PACKAGE\系统配置";
            string[] fileNames = Directory.GetFiles(dirName);
            Invoke(new Action(() =>
            {
                toolStripProgressBar.Maximum = fileNames.Length;
                toolStripProgressBar.Value = 0;
            }));
            foreach (var fileName in fileNames)
            {
                @string.Clear();
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    @string.Append(sr.ReadToEnd());
                }
                try
                {
                    CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.Message);
                    return;
                }
                Invoke(new Action(() =>
                {
                    toolStripProgressBar.Value++;
                }));
            }
            ShowMessage("系统配置更新成功");
        }
        /// <summary>
        /// 一键安装
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem04_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    DoSetup();
                }
                catch (ThreadAbortException)
                {
                }
            }, null);
        }
        /// <summary>
        /// 执行安装
        /// </summary>
        private void DoSetup()
        {
            this.Invoke(new Action(() =>
            {
                richTextBoxMessage.Clear();
            }));
            int cntNodes = 0;
            foreach (TreeNode moduleNode in this.treeViewFunction.Nodes)
            {
                ///不检测一级菜单  if (!moduleNode.Checked) continue;
                foreach (TreeNode functionNode in moduleNode.Nodes)
                {
                    if (!functionNode.Checked) continue;
                    cntNodes++;
                }
            }
            this.Invoke(new Action(() =>
            {
                toolStripProgressBar.Maximum = cntNodes;
                toolStripProgressBar.Value = 0;
            }));
            foreach (TreeNode moduleNode in this.treeViewFunction.Nodes)
            {
                ///不检测一级菜单  if (!moduleNode.Checked) continue;
                foreach (TreeNode functionNode in moduleNode.Nodes)
                {
                    if (!functionNode.Checked) continue;
                    string dirName = Application.StartupPath + @"\PACKAGE\" + moduleNode.Text + "-" + functionNode.Text;
                    string[] fileNames = Directory.GetFiles(dirName);
                    ///获取安装需要的文件
                    List<FunctionScript> functionScripts = GetSetupScripts(fileNames);
                    foreach (var functionScript in functionScripts)
                    {
                        StringBuilder sbSql = new StringBuilder();
                        using (StreamReader sr = new StreamReader(functionScript.ScriptFileName, Encoding.UTF8))
                        {
                            sbSql.Append(sr.ReadToEnd());
                        }
                        try
                        {
                            CommonBLL.ExecuteNonQueryBySql(sbSql.ToString());
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage(moduleNode.Text + "-" + functionNode.Text + "\r\n" + ex.Message);
                            return;
                        }
                    }
                    this.Invoke(new Action(() =>
                    {
                        richTextBoxMessage.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + moduleNode.Text + "-" + functionNode.Text + "  完成\r\n");
                        richTextBoxMessage.ForeColor = Color.Black;
                        richTextBoxMessage.ScrollToCaret();
                        toolStripProgressBar.Value++;
                    }));
                }
            }
            ///刷新数据模型错误
            toolStripMenuItem14_Click(null, null);
            ///更新管理员权限
            toolStripMenuItem12_Click(null, null);
            ///
            ShowMessage("安装完成");
        }

        /// <summary>
        /// 超级管理员权限更新语句
        /// </summary>
        private string adminAuthUpdateSql =
            "delete from dbo.TS_SYS_ROLE_AUTH where [ROLE_FID] = 'F04A3B27-B674-4A5E-BD4F-B2E2889318E4';" +
            "insert into dbo.TS_SYS_ROLE_AUTH " +
            "(FID, VALID_FLAG, CREATE_USER, CREATE_DATE, ROLE_FID, AUTH_TYPE, IS_AUTH, AUTH_SOURCE_FID) " +
            "select NEWID(),1,'',GETDATE(),'F04A3B27-B674-4A5E-BD4F-B2E2889318E4', 1, 1, FID from TS_SYS_MENU " +
            "where [NEED_AUTH] = 1;" +
            "insert into dbo.TS_SYS_ROLE_AUTH (FID, VALID_FLAG, CREATE_USER, CREATE_DATE, ROLE_FID, AUTH_TYPE, IS_AUTH, AUTH_SOURCE_FID) " +
            "select NEWID(),1,'',GETDATE(),'F04A3B27-B674-4A5E-BD4F-B2E2889318E4', 2, 1, FID " +
            "from TS_SYS_MENU_ACTION where [NEED_AUTH] = 1;" +
            "delete from dbo.TS_SYS_USER_ROLE_RANGE_AUTH where [USER_FID] = N'731F7D72-0884-4C13-A73F-5F1C1AEC8660' and [ROLE_FID] = N'F04A3B27-B674-4A5E-BD4F-B2E2889318E4';" +
            "insert into dbo.TS_SYS_USER_ROLE_RANGE_AUTH " +
            "(FID, USER_FID, ROLE_FID, CONDITION_FID, CONDITION_CONTEXT, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) " +
            "select NEWID(), N'731F7D72-0884-4C13-A73F-5F1C1AEC8660', N'F04A3B27-B674-4A5E-BD4F-B2E2889318E4', FID, N'1=1', COMMENTS, 1, CREATE_USER, GETDATE() from dbo.TS_SYS_RANGE_AUTH_CONDITION " +
            "where [VALID_FLAG] = 1;";
        /// <summary>
        /// 刷新管理员权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            try
            {
                CommonBLL.ExecuteNonQueryBySql(adminAuthUpdateSql);
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    ErrorMessage(ex.Message);
                }));
            }
            ShowMessage("已成功刷新管理员权限");
        }
        /// <summary>
        /// 刷新数据模型错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            StringBuilder @string = new StringBuilder();
            @string.AppendLine("update [dbo].[TS_SYS_ENTITY_FIELD] set [EXTEND1] = '' where [EXTEND1] is null;");
            @string.AppendLine("update [dbo].[TS_SYS_ENTITY_FIELD] set [EXTEND2] = '' where [EXTEND2] is null;");
            @string.AppendLine("update [dbo].[TS_SYS_ENTITY_FIELD] set [EXTEND3] = '' where [EXTEND3] is null;");
            try
            {
                CommonBLL.ExecuteNonQueryBySql(@string.ToString());
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    ErrorMessage(ex.Message);
                }));
            }
            ShowMessage("已成功刷新数据模型");
        }

        #region MESSAGE
        /// <summary>
        /// ShowMessage
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            this.Invoke(new EventHandler(delegate
            {
                MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + message
                    , "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }
        /// <summary>
        /// ErrorMessage
        /// </summary>
        /// <param name="message"></param>
        private void ErrorMessage(string message)
        {
            this.Invoke(new EventHandler(delegate
            {
                MessageBox.Show(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n" + message
                    , "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }));
        }
        #endregion

        /// <summary>
        /// 新建数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem05_Click(object sender, EventArgs e)
        {
            richTextBoxMessage.Clear();
            ///要求用户填写要创建的数据库名称
            DialogInput dialogInput = new DialogInput("请输入您要创建的数据库名称", "LES");
            if (dialogInput.ShowDialog(this) != DialogResult.Yes)
            {
                ErrorMessage("已取消创建数据库");
                return;
            }
            if (string.IsNullOrEmpty(dialogInput.InputMsg))
            {
                ErrorMessage("已取消创建数据库");
                return;
            }
            ///数据库名称
            string dbName = dialogInput.InputMsg;
            dialogInput.Dispose();
            if (string.IsNullOrEmpty(dbName)) return;

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder(configuration.ConnectionStrings.ConnectionStrings["SqlConnection"].ConnectionString);

            DialogConnection dialogConnection = new DialogConnection("master", sqlConnection.DataSource, sqlConnection.UserID, sqlConnection.Password);
            if (dialogConnection.ShowDialog(this) != DialogResult.Yes)
            {
                ErrorMessage("已取消创建数据库");
                return;
            }
            sqlConnection.InitialCatalog = dialogConnection.txtInitialCatalog.Text;
            sqlConnection.UserID = dialogConnection.txtUserID.Text;
            sqlConnection.Password = dialogConnection.txtPassword.Text;
            sqlConnection.DataSource = dialogConnection.txtDataSource.Text;
            dialogConnection.Dispose();
            ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings("SqlConnection", sqlConnection.ToString(), "System.Data.SqlClient");

            configuration.ConnectionStrings.ConnectionStrings.Remove("SqlConnection");
            ///将新的连接串添加到配置文件中
            configuration.ConnectionStrings.ConnectionStrings.Add(connectionStringSettings);
            ///保存对配置文件所作的更改
            configuration.Save(ConfigurationSaveMode.Modified);
            ///强制重新载入配置文件的ConnectionStrings配置节
            ConfigurationManager.RefreshSection("ConnectionStrings");

            StringBuilder @string = new StringBuilder();
            @string.AppendLine("select * from sysdatabases where name='" + dbName + "'");
            try
            {
                object result = CommonBLL.ExecuteScalar(@string.ToString());
                if (result != null && result != DBNull.Value)
                {
                    ErrorMessage("已存在" + dbName + "的数据库");
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
                return;
            }

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择数据库文件保存的目录";
            string selectedPath = string.Empty;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                selectedPath = folderBrowserDialog.SelectedPath;
            }
            folderBrowserDialog.Dispose();
            if (string.IsNullOrEmpty(selectedPath)) return;

            @string.Clear();
            @string.AppendLine("create database " + dbName);
            @string.AppendLine("on primary");
            @string.AppendLine("(");
            @string.AppendLine("    name = '" + dbName + "',");
            @string.AppendLine("    filename = '" + selectedPath + @"\" + dbName + ".mdf',");
            @string.AppendLine("    size = 50MB,");
            @string.AppendLine("    filegrowth = 10MB");
            @string.AppendLine(")");
            @string.AppendLine("log on");
            @string.AppendLine("(");
            @string.AppendLine("    name = '" + dbName + "_log',");
            @string.AppendLine("    filename = '" + selectedPath + @"\" + dbName + "_log.ldf',");
            @string.AppendLine("    size = 10MB,");
            @string.AppendLine("    filegrowth = 2MB");
            @string.AppendLine(")");

            try
            {
                CommonBLL.ExecuteNonQueryBySql(@string.ToString());
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
                return;
            }
            sqlConnection.InitialCatalog = dbName;
            connectionStringSettings = new ConnectionStringSettings("SqlConnection", sqlConnection.ToString(), "System.Data.SqlClient");
            configuration.ConnectionStrings.ConnectionStrings.Remove("SqlConnection");
            ///将新的连接串添加到配置文件中
            configuration.ConnectionStrings.ConnectionStrings.Add(connectionStringSettings);
            ///保存对配置文件所作的更改
            configuration.Save(ConfigurationSaveMode.Modified);
            ///强制重新载入配置文件的ConnectionStrings配置节
            ConfigurationManager.RefreshSection("ConnectionStrings");

            ShowMessage("已成功创建数据库");
        }
        /// <summary>
        /// 创建系统表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem06_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    SysCreate();
                }
                catch (ThreadAbortException)
                {
                }
            }, null);
        }
        /// <summary>
        /// 创建系统表
        /// </summary>
        private void SysCreate()
        {
            Invoke(new Action(() =>
            {
                richTextBoxMessage.Clear();
            }));
            StringBuilder @string = new StringBuilder("CREATE SCHEMA [LES]");
            try
            {
                CommonBLL.ExecuteNonQueryBySql(@string.ToString());
            }
            catch (Exception ex)
            {
                Invoke(new Action(() =>
                {
                    ErrorMessage(ex.Message);
                }));
                return;
            }
            @string.Clear();
            string configFileName = Application.StartupPath + @"\CONFIG\SYSTEM.xml";
            XmlWrapper xmlWrapper = new XmlWrapper(configFileName, LoadType.FromFile);
            List<object> configs = xmlWrapper.XmlToList("/Entitys/Entity", typeof(ConfigEntity));
            Invoke(new Action(() =>
            {
                toolStripProgressBar.Maximum = configs.Count;
                toolStripProgressBar.Value = 0;
            }));
            foreach (ConfigEntity config in configs)
            {
                Invoke(new Action(() =>
                {
                    toolStripProgressBar.Value++;
                }));
                if (config.DbCreateType != 10) continue;
                if (string.IsNullOrEmpty(config.TableName)) continue;
                if (string.IsNullOrEmpty(config.Schema)) continue;
                if (string.IsNullOrEmpty(config.PathName)) continue;
                string fileName = Application.StartupPath + @"\PACKAGE\" + config.PathName + @"\DB_CREATE_" + config.TableName + ".sql";
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    @string.AppendLine(sr.ReadToEnd());
                }
                try
                {
                    CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                }
                catch (Exception ex)
                {
                    Invoke(new Action(() =>
                    {
                        ErrorMessage(ex.Message);
                    }));
                    return;
                }
                Invoke(new Action(() =>
                {
                    richTextBoxMessage.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + config.TableName + "\r\n");
                    richTextBoxMessage.ForeColor = Color.Black;
                    richTextBoxMessage.ScrollToCaret();
                }));
            }
            ShowMessage("系统表创建完成");
        }
        /// <summary>
        /// TREE节点勾选后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewFunction_AfterCheck(object sender, TreeViewEventArgs e)
        {
            checkAllChildNode(e.Node);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeThis"></param>
        private void checkAllChildNode(TreeNode nodeThis)
        {
            foreach (TreeNode childNode in nodeThis.Nodes)
            {
                childNode.Checked = nodeThis.Checked;
                if (childNode.Nodes != null)
                {
                    checkAllChildNode(childNode);
                }
            }
        }
        /// <summary>
        /// 功能卸载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                  "您确定要进行功能卸载吗？",
                  "提示",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;
            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    UndoSetup();
                }
                catch (ThreadAbortException)
                {
                }
            }, null);
        }
        /// <summary>
        /// 卸载功能
        /// </summary>
        private void UndoSetup()
        {
            Invoke(new Action(() =>
            {
                richTextBoxMessage.Clear();
            }));
            int cntNodes = 0;
            foreach (TreeNode moduleNode in this.treeViewFunction.Nodes)
            {
                if (!moduleNode.Checked) continue;
                foreach (TreeNode functionNode in moduleNode.Nodes)
                {
                    if (!functionNode.Checked) continue;
                    cntNodes++;
                }
            }
            Invoke(new Action(() =>
            {
                toolStripProgressBar.Maximum = cntNodes;
                toolStripProgressBar.Value = 0;
            }));
            foreach (TreeNode moduleNode in this.treeViewFunction.Nodes)
            {
                if (!moduleNode.Checked) continue;
                foreach (TreeNode functionNode in moduleNode.Nodes)
                {
                    if (!functionNode.Checked) continue;
                    string dirName = Application.StartupPath + @"\PACKAGE\" + moduleNode.Text + "-" + functionNode.Text;
                    string[] fileNames = Directory.GetFiles(dirName);
                    ///获取安装需要的文件
                    List<FunctionScript> functionScripts = GetUndoSetupScripts(fileNames);
                    foreach (var functionScript in functionScripts)
                    {
                        StringBuilder @string = new StringBuilder();
                        using (StreamReader sr = new StreamReader(functionScript.ScriptFileName, Encoding.UTF8))
                        {
                            @string.AppendLine(sr.ReadToEnd());
                        }
                        try
                        {
                            CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage(moduleNode.Text + "-" + functionNode.Text + "\r\n" + ex.Message);
                            return;
                        }
                    }
                    this.Invoke(new Action(() =>
                    {
                        richTextBoxMessage.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + moduleNode.Text + "-" + functionNode.Text + "  完成\r\n");
                        richTextBoxMessage.ForeColor = Color.Black;
                        richTextBoxMessage.ScrollToCaret();
                        toolStripProgressBar.Value++;
                    }));
                }
            }
            ///更新管理员权限
            toolStripMenuItem12_Click(null, null);
            ///
            ShowMessage("卸载完成");
        }
        /// <summary>
        /// 数据结构删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "您确定要删除数据结构吗？",
                "提示",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                return;

            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    DropTable();
                }
                catch (ThreadAbortException)
                {
                }
            }, null);
        }
        /// <summary>
        /// 删库跑路
        /// </summary>
        private void DropTable()
        {
            Invoke(new Action(() =>
            {
                richTextBoxMessage.Clear();
            }));
            int cntNodes = 0;
            foreach (TreeNode moduleNode in this.treeViewFunction.Nodes)
            {
                if (!moduleNode.Checked) continue;
                foreach (TreeNode functionNode in moduleNode.Nodes)
                {
                    if (!functionNode.Checked) continue;
                    cntNodes++;
                }
            }
            Invoke(new Action(() =>
            {
                toolStripProgressBar.Maximum = cntNodes;
                toolStripProgressBar.Value = 0;
            }));
            foreach (TreeNode moduleNode in this.treeViewFunction.Nodes)
            {
                if (!moduleNode.Checked) continue;
                foreach (TreeNode functionNode in moduleNode.Nodes)
                {
                    if (!functionNode.Checked) continue;
                    string dirName = Application.StartupPath + @"\PACKAGE\" + moduleNode.Text + "-" + functionNode.Text;
                    string[] fileNames = Directory.GetFiles(dirName);
                    ///获取安装需要的文件
                    List<FunctionScript> functionScripts = GetDropScripts(fileNames);
                    foreach (var functionScript in functionScripts)
                    {
                        StringBuilder @string = new StringBuilder();
                        using (StreamReader sr = new StreamReader(functionScript.ScriptFileName, Encoding.UTF8))
                        {
                            @string.AppendLine(sr.ReadToEnd());
                        }
                        try
                        {
                            CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage(moduleNode.Text + "-" + functionNode.Text + "\r\n" + ex.Message);
                            return;
                        }
                    }
                    Invoke(new Action(() =>
                    {
                        richTextBoxMessage.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + moduleNode.Text + "-" + functionNode.Text + "  完成\r\n");
                        richTextBoxMessage.ForeColor = Color.Black;
                        richTextBoxMessage.ScrollToCaret();
                        toolStripProgressBar.Value++;
                    }));
                }
            }
            ///更新管理员权限
            toolStripMenuItem12_Click(null, null);
            ///
            ShowMessage("数据结构删除完成");
        }
    }

    public class FunctionScript
    {
        private int scriptSeq;
        private string scriptFileName;
        private string tableName;

        public int ScriptSeq { get => scriptSeq; set => scriptSeq = value; }
        public string ScriptFileName { get => scriptFileName; set => scriptFileName = value; }
        public string TableName { get => tableName; set => tableName = value; }
    }
    public class FunctionInfo
    {
        private string moduleName;
        private string functionName;

        public string ModuleName { get => moduleName; set => moduleName = value; }
        public string FunctionName { get => functionName; set => functionName = value; }
    }
    /// <summary>
    /// Config Entity
    /// </summary>
    [XmlRoot("Entity")]
    public class ConfigEntity
    {
        [XmlAttribute]
        public string Name;
        [XmlAttribute]
        public string TableName;
        [XmlAttribute]
        public bool MenuFlag;
        [XmlAttribute]
        public string PathName;
        [XmlAttribute]
        public string Schema;
        [XmlAttribute]
        public int DbCreateType;
    }
}
