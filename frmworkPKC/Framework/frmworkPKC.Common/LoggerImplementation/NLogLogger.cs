// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLogLogger.cs" company="EPAM Systems">
// Copyright 2016
// </copyright>
// <summary>
//   Defines the RedisStorageErrorLog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.LoggingImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web;
    using frmworkPKC.Common.Logging;
    using NLog;
    /// <summary>
    /// NLogLogger
    /// </summary>
    /// <seealso cref="frmworkPKC.Common.Logging.ILoggerHelper" />
    public class NLogLogger : ILogHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            logger.Debug(message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Debug(object message, Exception exception)
        {
            logger.Debug("" + message, exception);
        }

        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void DebugFormat(string format, params object[] args)
        {
            logger.Debug(format, args);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            logger.Error(message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(object message, Exception exception)
        {
            logger.Error("" + message + exception.ToString());
        }

        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            logger.Error(format, args);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Info(object message, Exception exception)
        {
            logger.Info("" + message, exception);
        }

        /// <summary>
        /// Informations the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void InfoFormat(string format, params object[] args)
        {
            logger.Info(format, args);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(object message)
        {
            logger.Fatal(message);
        }

        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(object message, Exception exception)
        {
            logger.Fatal("" + message, exception);
        }

        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void FatalFormat(string format, params object[] args)
        {
            logger.Fatal(format, args);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warning(object message)
        {
            logger.Warn(message);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Warning(object message, Exception exception)
        {
            logger.Warn("" + message, exception);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public void WarnFormat(string format, params object[] args)
        {
            logger.Warn(format, args);
        }

        /// <summary>
        /// Logs the specified logging data.
        /// </summary>
        /// <param name="loggingData">The logging data.</param>
        public void Log(LoggingData loggingData)
        {
            var logEvent = new LogEventInfo();
            logEvent.LoggerName = loggingData.LoggerName;
            logEvent.Message = loggingData.Message;
            logEvent.TimeStamp = loggingData.TimeStamp;

            if (!string.IsNullOrEmpty(loggingData.Exception))
                logEvent.Exception = new Exception(loggingData.Exception);

            if (loggingData.Properties != null && loggingData.Properties.Count > 0)
            {
                var properties = loggingData.Properties;
                var dict = new Dictionary<object, object>();

                foreach (var k in properties.Keys.OfType<string>())
                {
                    properties.Add(k, properties[k]);
                }
            }

            logger.Log(logEvent);
        }
    }
}