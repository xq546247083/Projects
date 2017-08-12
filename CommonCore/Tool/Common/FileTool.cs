/************************************************************************
* 标题: 文件工具
* 描述: 文件工具
* 作者：肖强
* 日期：2017-5-3 10:43:36
* 版本：V1
*************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tool.Common
{
    using Ionic.Zip;
    using Tool.Extension;
    using Tool.Log;

    /// <summary>
    /// 文件工具
    /// </summary>
    public static class FileTool
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>读取字符串</returns>
        public static String ReadFile(String fileName)
        {
            String result = String.Empty; ;

            //读取文件
            if (!String.IsNullOrEmpty(fileName))
            {
                try
                {
                    FileInfo info = new FileInfo(fileName);
                    if (info.Exists)
                    {
                        using (StreamReader reader = new StreamReader(info.FullName))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.ToMessage(), LogType.Error);
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="append">是否追加</param>
        /// <param name="msgs">写入数据</param>
        public static void WriteFile(String filePath, String fileName, bool append, params String[] msgs)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                try
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    FileInfo info = new FileInfo(Path.Combine(filePath, fileName));
                    using (StreamWriter writer = new StreamWriter(info.FullName, append))
                    {
                        foreach (String str in msgs)
                        {
                            writer.WriteLine(str);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex.ToMessage(), LogType.Error);
                    throw;
                }
            }
        }

        /// <summary>
        /// 获取指定目录下的所有文件、包括子文件夹里面的文件
        /// </summary>
        /// <param name="path">目录</param>
        /// <param name="suffix">文件后缀，用于过滤文件</param>
        /// <returns>文件的全路径列表</returns>
        public static List<String> GetFileList(String path, String suffix)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            return GetFileList(dirInfo, suffix);
        }

        /// <summary>
        /// 获取指定目录对象下的所有文件、包括子文件夹里面的文件
        /// </summary>
        /// <param name="dirInfo">目录信息对象</param>
        /// <param name="suffix">文件后缀，用于过滤文件</param>
        /// <returns>文件的全路径列表</returns>
        public static List<String> GetFileList(DirectoryInfo dirInfo, String suffix)
        {
            //文件列表
            List<String> list = new List<String>();

            //获取文件夹列表
            FileInfo[] files = dirInfo.GetFiles();
            FileInfo[] array = files;

            //获取直接文件
            for (int i = 0; i < array.Length; i++)
            {
                FileInfo fileInfo = array[i];
                if (String.IsNullOrEmpty(suffix) || fileInfo.Extension.Equals(suffix, StringComparison.Ordinal))
                {
                    list.Add(fileInfo.FullName);
                }
            }

            //获取子文件夹下面的文件
            DirectoryInfo[] directories = dirInfo.GetDirectories();
            DirectoryInfo[] array2 = directories;
            for (int j = 0; j < array2.Length; j++)
            {
                DirectoryInfo dirInfo2 = array2[j];
                list.AddRange(FileTool.GetFileList(dirInfo2, suffix));
            }

            return list;
        }

        /// <summary>
        /// 创建压缩文件，zip格式
        /// </summary>
        /// <param name="files">文件</param>
        /// <param name="folders">文件夹</param>
        /// <param name="savePath">压缩文件存放地址</param>
        public static void CreateZip(IEnumerable<String> files, IEnumerable<String> folders, String savePath)
        {
            if (((files == null) || (!files.Any())) && ((folders == null) || (!folders.Any())))
            {
                throw new ArgumentNullException("files，folders不能全为空。");
            }

            //获取目录
            String directoryName = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(directoryName))
            {
                if (directoryName != null) Directory.CreateDirectory(directoryName);
            }

            using (ZipFile file = new ZipFile(savePath,Encoding.UTF8))
            {
                if (files != null)
                {
                    file.UpdateFiles(files, "");
                }

                if (folders != null)
                {
                    foreach (String str2 in folders)
                    {
                        file.UpdateDirectory(str2, Path.GetFileNameWithoutExtension(str2));
                    }
                }

                file.Save(savePath);
            }
        }

        /// <summary>
        /// 解压缩文件，zip格式
        /// </summary>
        /// <param name="zipPath">压缩文件路径</param>
        /// <param name="targetPath">释放路径</param>
        public static void ExtractZip(String zipPath, String targetPath)
        {
            if (!File.Exists(zipPath))
            {
                throw new FileNotFoundException(String.Format("找不到待解压缩文件：{0}", zipPath));
            }

            if (new FileInfo(zipPath).Extension != ".zip")
            {
                throw new FormatException(String.Format("待解压缩文件：{0}不是zip文件", zipPath));
            }

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            using (ZipFile file = new ZipFile(zipPath, Encoding.UTF8))
            {
                file.ExtractAll(targetPath);
            }
        }
    }
}
