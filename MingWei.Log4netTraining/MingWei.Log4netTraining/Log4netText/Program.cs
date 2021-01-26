using System;
using System.Configuration;
using log4netText;

namespace Log4netText
{
    class Program
    {
        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();
            //初始化日志文件
            //string state = ConfigurationManager.AppSettings["IsWriteLog"];
            ////判断是否开启日志记录
            //if (state == "1")
            //{
            //    var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase +
            //               ConfigurationManager.AppSettings["log4net"];
            //    var fi = new System.IO.FileInfo(path);
            //    log4net.Config.XmlConfigurator.Configure(fi);
            //}

            //LogHelper.WriteLog("hello world");


            //log4net日志
            log4net.ILog logInfo = log4net.LogManager.GetLogger("loginfo");
            logInfo.Info("测试日志写入1");
        }
    }
}
