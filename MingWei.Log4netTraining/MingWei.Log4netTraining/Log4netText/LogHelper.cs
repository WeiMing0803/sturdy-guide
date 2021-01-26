using System;

namespace log4netText
{
    /// <summary>
    /// LogHelper的摘要说明
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 静态只读实体对象info信息
        /// </summary>
        public static readonly log4net.ILog Loginfo = log4net.LogManager.GetLogger("loginfo");

        /// <summary>
        /// 静态只读实体对象error信息
        /// </summary>
        public static readonly log4net.ILog Logerror = log4net.LogManager.GetLogger("logerror");

        public static void WriteLog(string info)
        {
            try
            {
                if (Loginfo.IsInfoEnabled)
                {
                    Loginfo.Info(info);
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void WriteLog(string info, Exception ex)
        {
            try
            {
                if (Logerror.IsErrorEnabled)
                {
                    Logerror.Error(info, ex);
                }
            }
            catch
            {
                // ignored
            }
        }

    }
}