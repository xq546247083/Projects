/************************************************************************
* 标题: 文件工具
*************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;

namespace Manage.Common
{
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
                    Log.Write(ex.ToString(), LogType.Error);
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
                    Log.Write(ex.ToString(), LogType.Error);
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
    }
}
