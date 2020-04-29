using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NUT.WEB.Utils
{
    public static class Inst
    {

        public static log4net.ILog log = log4net.LogManager.GetLogger("NUT.WEB");

        public static void Error(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
        public static void Warn(object message, Exception ex)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
        }
        public static void Info(object message, Exception ex)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }

    }
}