using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace FireCodeCreatorWeb.Helper
{
    public class FileHelper
    {

        /**//// <summary>
        /// 读取日志文件
        /// </summary>
        public static string ReadLogFile(string url)
        {

            /**/
            ///从指定的目录以打开或者创建的形式读取日志文件  "keystringlog.txt"
            FileStream fs = new FileStream(url, FileMode.OpenOrCreate, FileAccess.Read);

            /**/
            ///定义输出字符串
            StringBuilder output = new StringBuilder();

            /**/
            ///初始化该字符串的长度为0
            output.Length = 0;

            /**/
            ///为上面创建的文件流创建读取数据流
            StreamReader read = new StreamReader(fs);

            /**/
            ///设置当前流的起始位置为文件流的起始点
            read.BaseStream.Seek(0, SeekOrigin.Begin);

            /**/
            ///读取文件
            while (read.Peek() > -1)
            {
                /**/
                ///取文件的一行内容并换行
                output.Append(read.ReadLine() + "\n");
            }

            /**/
            ///关闭释放读数据流
            read.Close();

            /**/
            ///返回读到的日志文件内容
            return output.ToString();
        }


        public static void SaveLogFile(string url, string txtStr)  //"keystringlog.txt"
        {
            FileStream fs = new FileStream(url, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(txtStr);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}