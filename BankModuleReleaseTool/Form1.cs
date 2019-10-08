using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankModuleReleaseTool
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 源文件目录
        /// </summary>
        private static string _resourcePath = string.Empty;
        /// <summary>
        /// 发布目录
        /// </summary>
        private static string _releasePath = string.Empty;
        /// <summary>
        /// 过滤文件夹
        /// </summary>
        private static string _excludeFolder = string.Empty;
        /// <summary>
        /// 过滤文件
        /// </summary>
        private static string _excludeFiles = string.Empty;
        /// <summary>
        /// 银行配置列表
        /// </summary>
        private static List<Bank> _bankList = null;

        private const string BankModule = "BankModule";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            Thread thread = new Thread(new ThreadStart(DoWork));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// 加载窗体时，初始化配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            string fileName = Environment.CurrentDirectory + "\\path.xml";
            if (File.Exists(fileName))
            {
                try
                {
                    PathConfigEntity entity = Tools.ReadXML<PathConfigEntity>(fileName);
                    _resourcePath = entity.ResourcePath;
                    _releasePath = entity.ReleasePath;
                    _excludeFolder = entity.ExcludeFolder;
                    _excludeFiles = entity.ExcludeFile;
                    _bankList = entity.BankList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取配置文件异常：" + ex.Message);
                }
            }
            else
            {
                _bankList = new List<Bank>();
                MessageBox.Show("配置文件不存在！");
            }
        }

        private void DoWork()
        {
            try
            {
                string tempDir = _releasePath + "\\" + BankModule;
                if (!string.IsNullOrEmpty(_resourcePath) && Directory.Exists(_resourcePath))
                {
                    #region 第一步：复制公共文件至临时文件夹

                    ShowMsg("正在拷贝临时文件！", Color.Black);
                    Tools.CopyDirectory(_resourcePath, tempDir, true, BankModule, _excludeFiles);
                    StringBuilder sb = new StringBuilder();
                    foreach (Bank bank in _bankList)
                    {
                        sb.Append(bank.Folder + ",");
                    }
                    string bankFolders = sb.ToString();
                    Tools.CopyDirectory(_resourcePath + "\\" + BankModule, tempDir + "\\" + BankModule, true, bankFolders + _excludeFolder, _excludeFiles);

                    #endregion

                    #region 第二步：复制各银行文件夹至临时文件夹，并压缩，再删除该银行文件夹

                    foreach (Bank bank in _bankList)
                    {
                        string bankFolderSrc = _resourcePath + "\\" + BankModule + "\\" + bank.Folder;
                        string bankFolder = tempDir + "\\" + BankModule;
                        if (!string.IsNullOrEmpty(bank.Folder) && Directory.Exists(bankFolderSrc))
                        {
                            //2.1复制各银行文件夹
                            Tools.CopyDirectory(bankFolderSrc, bankFolder);

                            //2.2压缩文件
                            ShowMsg("正在打包:" + bank.BankName, Color.Black);
                            Tools.ZipFileFromDirectory(tempDir, "", _releasePath + "\\" + BankModule + "_" + bank.BankName + ".zip", 9);

                            //2.3删除各银行文件夹
                            if (Directory.Exists(bankFolder + "\\" + bank.Folder))
                            {
                                Directory.Delete(bankFolder + "\\" + bank.Folder, true);
                            }
                        }
                    }

                    #endregion

                    #region 第三步，删除临时文件夹

                    ShowMsg("正在删除临时文件！", Color.Black);
                    if (Directory.Exists(tempDir))
                    {
                        Directory.Delete(tempDir, true);
                    }

                    ShowMsg("发布完成！", Color.Green);

                    this.BeginInvoke(new EventHandler(delegate
                    {
                        this.btnRelease.Enabled = true;
                    }));

                    #endregion
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("发布异常：" + e.Message);
            }

        }

        private void ShowMsg(string msg, Color color)
        {
            this.BeginInvoke(new EventHandler(delegate
            {
                this.lab_Tips.Text = msg;
                this.lab_Tips.ForeColor = color;
            }));
        }
    }
}
