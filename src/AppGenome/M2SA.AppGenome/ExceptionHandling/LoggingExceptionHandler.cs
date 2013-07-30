﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using M2SA.AppGenome.Logging;
using M2SA.AppGenome.Reflection;

namespace M2SA.AppGenome.ExceptionHandling
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggingExceptionHandler : IExceptionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public LogLevel LogLevel
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string LogCategory
        {
            get;
            private set;
        }

        #region IExceptionHandler 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="handlingInstanceId"></param>
        /// <param name="bizInfo"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public Exception HandleException(Exception exception, Guid handlingInstanceId, IDictionary bizInfo)
        {
            try
            {
                LogManager.GetLogger(this.LogCategory).WriteMessage(this.LogLevel, bizInfo, exception);
            }
            catch (Exception ex)
            {
                var log = string.Format("{0}_LoggingException.log", DateTime.Now.ToString("yyyyMMddHH"));
                FileHelper.WriteInfo(log, ex.ToText());
            }
            return exception;
        }

        #endregion
    }
}