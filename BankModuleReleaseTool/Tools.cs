using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankModuleReleaseTool
{
    class Tools
    {
        /// <summary>  
        /// 检测指定文件是否存在,如果存在则返回true。  
        /// </summary>  
        /// <param name="filePath">文件的绝对路径</param>          
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 复制整个文件夹下的文件到指定目录
        /// </summary>
        /// <param name="srcPath">当前文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        /// <param name="overwrite">当重名是否覆盖</param>
        public static void CopyDirectory(string srcPath, string destPath, bool overwrite)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name, overwrite);    //递归调用复制子文件夹
                    }
                    else
                    {
                        if (overwrite)
                        {
                            File.Copy(i.FullName, destPath + "\\" + i.Name, overwrite);
                        }
                        else if (!IsExistFile(destPath + "\\" + i.Name))
                        {
                            File.Copy(i.FullName, destPath + "\\" + i.Name, overwrite);      //不是文件夹即复制文件
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 读取xml文件，并将文件序列化为类
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="path">xml文件路径</param>
        /// <returns>对象</returns>
        public static T ReadXML<T>(string path)
        {
            try
            {
                XmlSerializer reader = new XmlSerializer(typeof(T));
                using (StreamReader file = new StreamReader(path))
                {
                    return (T)reader.Deserialize(file);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
