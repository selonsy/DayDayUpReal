using System;
using System.IO;

namespace Devin
{
    /// <summary>
    /// 日志类
    /// </summary>
    public static class LogHelper
    {        
        /// <summary>
        /// 配置默认路径 
        /// </summary>                
        private static string defaultPath = @"D:\公用文件\MyLog";
       
        #region Exception异常日志

        /// <summary>
        /// 写异常日志，存放到默认路径
        /// </summary>
        /// <param name="ex">异常类</param>
        public static void WriteError(Exception ex)
        {
            WriteError(ex, defaultPath);
        }

        /// <summary>
        /// 写异常日志，存放到指定路径
        /// </summary>
        /// <param name="ex">异常类</param>
        /// <param name="path">日志存放路径</param>
        public static void WriteError(Exception ex, string path)
        {
            string errMsg = CreateErrorMeg(ex);
            WriteLog(errMsg, path, LogType.Error);
        }

        #endregion

        #region 普通日志

        /// <summary>
        /// 写普通日志，存放到默认路径，使用默认日志类型
        /// </summary>
        /// <param name="msg">日志内容</param>
        public static void WriteLog(string msg)
        {
            WriteLog(msg, LogType.Info);
        }

        /// <summary>
        /// 写普通日志，存放到默认路径，使用指定日志类型
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="logType">日志类型</param>
        public static void WriteLog(string msg, LogType logType)
        {
            WriteLog(msg, defaultPath, logType);
        }

        /// <summary>
        /// 写普通日志，存放到指定路径，使用默认日志类型
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="path">日志存放路径</param>
        public static void WriteLog(string msg, string path)
        {
            WriteLog(msg, path, LogType.Info);
        }

        /// <summary>
        /// 写普通日志，存放到指定路径，使用指定日志类型
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="path">日志存放路径</param>
        /// <param name="logType">日志类型</param>
        public static void WriteLog(string msg, string path, LogType logType)
        {
            string fileName = path.Trim('\\') + "\\" + CreateFileName(logType);
            string logContext = FormatMsg(msg, logType);
            WriteFile(logContext, fileName);
        }

        #endregion

        #region 其他辅助方法

        /// <summary>
        /// 写日志到文件
        /// </summary>
        /// <param name="logContext">日志内容</param>
        /// <param name="fullName">文件名</param>
        private static void WriteFile(string logContext, string fullName)
        {
            FileStream fs = null;
            StreamWriter sw = null;

            int splitIndex = fullName.LastIndexOf('\\');
            if (splitIndex == -1) return;
            string path = fullName.Substring(0, splitIndex);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            try
            {
                if (!File.Exists(fullName)) fs = new FileStream(fullName, FileMode.CreateNew);
                else fs = new FileStream(fullName, FileMode.Append);

                sw = new StreamWriter(fs);
                sw.WriteLine(logContext);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        /// 格式化日志，日志是默认类型
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <returns>格式化后的日志</returns>
        private static string FormatMsg(string msg)
        {
            return FormatMsg(msg, LogType.Info);
        }

        /// <summary>
        /// 格式化日志
        /// </summary>
        /// <param name="msg">日志内容</param>
        /// <param name="logType">日志类型</param>
        /// <returns>格式化后的日志</returns>
        private static string FormatMsg(string msg, LogType logType)
        {
            string result;
            string header = string.Format("[{0}][{1} {2}] ", logType.ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString());
            result = header + msg;
            return result;
        }

        /// <summary>
        /// 从异常类中获取日志内容
        /// </summary>
        /// <param name="ex">异常类</param>
        /// <returns>日志内容</returns>
        private static string CreateErrorMeg(Exception ex)
        {
            string result = string.Empty;
            result += "\r\n[GetType]" + ex.GetType() + "\r\n";
            result += "[Message]"+ex.Message + "\r\n";
            result += "[Source]" + ex.Source + "\r\n";
            result += "[TargetSite]" + ex.TargetSite + "\r\n";
            result += "[Data]" + ex.Data + "\r\n";                       
            result += "[StackTrace]\r\n" + ex.StackTrace + "\r\n";
            return result;
        }

        /// <summary>
        /// 生成日志文件名
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <returns>日志文件名</returns>
        private static string CreateFileName(LogType logType)
        {
            string result = DateTime.Now.ToString("yyyy-MM-dd");
            if (logType == LogType.Info)
            {
                result = logType.ToString() + "-" + result + ".txt";
            }
            else if (logType == LogType.Error)
            {
                result = logType.ToString() + "-" + result + ".log";
            }
            return result;
        }

        #endregion
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        Error,
        Info,
        Option
    }
}