using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace BankModuleReleaseTool
{
    class Tools
    {
        /// <summary>
        /// 复制整个文件夹下的文件到指定目录
        /// </summary>
        /// <param name="srcPath">原文件夹</param>
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
                        else if (!File.Exists(destPath + "\\" + i.Name))
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
        /// 复制文件夹，过滤指定文件夹、文件
        /// </summary>
        /// <param name="srcPath">原文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        /// <param name="overwrite">当重名是否覆盖</param>
        /// <param name="excludeDirs">排除文件夹列表（英文逗号分隔）</param>
        /// <param name="excludeFiles">排除文件列表（英文逗号分隔）</param>
        public static void CopyDirectory(string srcPath, string destPath, bool overwrite, string excludeDirs, string excludeFiles)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        excludeDirs = excludeDirs.EndsWith(",") ? excludeDirs : excludeDirs + ",";
                        if (excludeDirs.Contains(i.Name + ","))
                        {
                            continue;
                        }

                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name, overwrite);    //递归调用复制子文件夹
                    }
                    else
                    {
                        excludeFiles = excludeFiles.EndsWith(",") ? excludeFiles : excludeFiles + ",";
                        if (excludeFiles.Contains(i.Name + ","))
                        {
                            continue;
                        }
                        if (!Directory.Exists(destPath))
                        {
                            Directory.CreateDirectory(destPath);
                        }
                        if (overwrite)
                        {
                            File.Copy(i.FullName, destPath + "\\" + i.Name, overwrite);
                        }
                        else if (!File.Exists(destPath + "\\" + i.Name))
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
        /// 复制文件夹
        /// </summary>
        /// <param name="srcPath">原文件夹</param>
        /// <param name="destPath">目标文件夹</param>
        public static void CopyDirectory(string srcPath, string destPath)
        {
            string folderName = srcPath.Substring(srcPath.LastIndexOf("\\") + 1);
            string desfolderdir = destPath + "\\" + folderName;

            if (destPath.LastIndexOf("\\") == (destPath.Length - 1))
            {
                desfolderdir = destPath + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {

                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;


                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }


                    File.Copy(file, srcfileName);
                }
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

        /// <summary>  
        /// 压缩目录（包括子目录及所有文件）  
        /// </summary>  
        /// <param name="rootPath">要压缩的根目录</param>  
        /// <param name="relativePath">相对压缩目录路径</param>
        /// <param name="destinationPath">保存路径</param>  
        /// <param name="compressLevel">压缩程度，范围0-9，数值越大，压缩程序越高</param>  
        /// <param name="rootRemove">压缩时是否将根目录打包进去</param>  
        /// <param name="password">压缩密码</param>  
        public static void ZipFileFromDirectory(string rootPath, string relativePath, string destinationPath, int compressLevel, bool rootRemove = false, string password = "")
        {
            string resourPath = rootPath;
            if (rootPath[rootPath.Length - 1] != Path.DirectorySeparatorChar)
            {
                rootPath += Path.DirectorySeparatorChar;
            }
            else
            {
                if (rootRemove)
                {
                    resourPath = rootPath;
                }
                else
                {
                    resourPath = rootPath.TrimEnd(Path.DirectorySeparatorChar);
                }
            }
            ZipOutputStream zips = new ZipOutputStream(File.Create(destinationPath));
            if (!string.IsNullOrEmpty(password))//添加压缩密码
            {
                zips.Password = password;
            }
            zips.SetLevel(compressLevel);
            zip(rootPath, relativePath, zips, destinationPath, resourPath);
            zips.Finish();
            zips.Close();
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="strFile"></param>
        /// <param name="relativePath"></param>
        /// <param name="zips"></param>
        /// <param name="zipFile"></param>
        /// <param name="resourPath"></param>
        private static void zip(string strFile, string relativePath, ZipOutputStream zips, string zipFile, string resourPath)
        {
            if (strFile[strFile.Length - 1] != Path.DirectorySeparatorChar)
            {
                strFile += Path.DirectorySeparatorChar;
            }
            Crc32 crc = new Crc32();
            string[] filenames = Directory.GetFileSystemEntries(strFile);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    zip(file, relativePath, zips, zipFile, resourPath);
                }
                else
                {
                    FileStream fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string tempfile = relativePath + resourPath.Substring(resourPath.LastIndexOf("\\") + 1) + file.Replace(resourPath, "");
                    ZipEntry entry = new ZipEntry(tempfile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zips.PutNextEntry(entry);
                    zips.Write(buffer, 0, buffer.Length);
                }
            }
        }

    }
}
