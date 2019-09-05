using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        /// 银行配置列表
        /// </summary>
        private static List<Bank> _bankList = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Tools.CopyDirectory(_resourcePath, _releasePath, true);
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

                    _bankList = entity.BankList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取配置文件异常：" + ex.Message); ;
                }
            }
            else
            {
                _bankList = new List<Bank>();
                MessageBox.Show("配置文件不存在！");
            }
        }
    }
}
