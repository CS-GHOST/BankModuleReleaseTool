using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankModuleReleaseTool
{
    [XmlRoot("PathConfig")]
    public class PathConfigEntity
    {
        private string resourcePath = string.Empty;
        private string releasePath = string.Empty;
        private List<Bank> bankList = new List<Bank>();

        /// <summary>
        /// 源文件目录
        /// </summary>
        public string ResourcePath
        {
            get { return resourcePath; }
            set { resourcePath = value; }
        }

        /// <summary>
        /// 发布目录
        /// </summary>
        public string ReleasePath
        {
            get { return releasePath; }
            set { releasePath = value; }
        }

        [XmlElement("Bank")]
        public List<Bank> BankList
        {
            get
            {
                return bankList;
            }

            set
            {
                bankList = value;
            }
        }
    }

    public class Bank
    {
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string Folder { get; set; }
    }
}
