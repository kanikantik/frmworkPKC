// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILoggerHelper.cs" company="EPAM Systems">
// Copyright 2016
// </copyright>
// <summary>
//   Defines the RedisStorageErrorLog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace frmworkPKC.Common.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ILoggerHelper
    /// </summary>
    public interface ILogHelper
    {
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(object message);
        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Debug(object message, Exception exception);
        /// <summary>
        /// Debugs the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void DebugFormat(string format, params object[] args);
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(object message);
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(object message, Exception exception);
        /// <summary>
        /// Errors the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void ErrorFormat(string format, params object[] args);
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(object message);
        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Info(object message, Exception exception);
        /// <summary>
        /// Informations the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void InfoFormat(string format, params object[] args);
        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(object message);
        /// <summary>
        /// Fatals the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Fatal(object message, Exception exception);
        /// <summary>
        /// Fatals the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void FatalFormat(string format, params object[] args);
        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(object message);
        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Warning(object message, Exception exception);
        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        void WarnFormat(string format, params object[] args);
        /// <summary>
        /// Logs the specified logging data.
        /// </summary>
        /// <param name="loggingData">The logging data.</param>
        void Log(LoggingData loggingData);

    }
}

