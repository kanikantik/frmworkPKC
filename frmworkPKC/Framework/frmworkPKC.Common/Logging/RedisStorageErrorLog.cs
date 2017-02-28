// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RedisStorageErrorLog.cs" company="EPAM Systems">
// Copyright 2015
// </copyright>
// <summary>
//   Defines the RedisStorageErrorLog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace frmworkPKC.Common.Logging
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Elmah;
    using ServiceStack.Redis;

    /// <summary>
    /// Provides storage for error logging using Redis.
    /// </summary>
    public class RedisStorageErrorLog : ErrorLog
    {
        /// <summary>
        /// The redis client factory
        /// </summary>
        private readonly IRedisClientFactory redisClientFactory;
        /// <summary>
        /// The database
        /// </summary>
        private readonly long db;
        /// <summary>
        /// The host string.
        /// </summary>
        private readonly string host;
        /// <summary>
        /// The port integer.
        /// </summary>
        private readonly int port;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStorageErrorLog" /> class.
        /// Constructor for the REDIS Storage Error Log.
        /// </summary>
        /// <param name="config">IDictionary of the configuration.</param>
        public RedisStorageErrorLog(IDictionary config)
        {
            this.redisClientFactory = RedisClientFactory.Instance;

            this.host = config["host"] as string ?? RedisNativeClient.DefaultHost;
            this.port = int.Parse(config["port"] as string ?? RedisNativeClient.DefaultPort.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            this.db = long.Parse(config["db"] as string ?? RedisNativeClient.DefaultDb.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStorageErrorLog" /> class.
        /// </summary>
        /// <param name="host">The host value.</param>
        /// <param name="port">The port value.</param>
        /// <param name="db">The database value.</param>
        public RedisStorageErrorLog(string host, int port, long db)
        {
            this.redisClientFactory = RedisClientFactory.Instance;
            this.host = host;
            this.port = port;
            this.db = db;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStorageErrorLog" /> class.
        /// </summary>
        /// <param name="host">The host value.</param>
        /// <param name="port">The port value.</param>
        public RedisStorageErrorLog(string host, int port)
        {
            this.redisClientFactory = RedisClientFactory.Instance;
            this.host = host;
            this.port = port;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStorageErrorLog" /> class.
        /// </summary>
        /// <param name="clientFactory">The redis client factory.</param>
        /// <param name="host">The host value.</param>
        /// <param name="port">The port value.</param>
        /// <param name="db">The database value.</param>
        public RedisStorageErrorLog(IRedisClientFactory clientFactory, string host, int port, long db)
        {
            this.redisClientFactory = clientFactory;
            this.host = host;
            this.port = port;
            this.db = db;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisStorageErrorLog" /> class.
        /// </summary>
        /// <param name="clientFactory">The redis client factory.</param>
        /// <param name="host">The host value.</param>
        /// <param name="port">The port value.</param>
        public RedisStorageErrorLog(IRedisClientFactory clientFactory, string host, int port)
        {
            this.redisClientFactory = clientFactory;
            this.host = host;
            this.port = port;
        }

        /// <summary>
        /// Name of the ErrorLog provider.
        /// </summary>
        /// <value>
        /// The name string.
        /// </value>
        public override string Name
        {
            get
            {
                return "Redis Storage Error Log";
            }
        }

        /// <summary>
        /// Logs the error passed in.
        /// </summary>
        /// <param name="error">Error object.</param>
        /// <returns>
        /// returns the error log entry id as string
        /// </returns>
        /// <exception cref="System.ArgumentNullException">error message.</exception>
        public override string Log(Error error)
        {
            if (error == null)
            {
                throw new ArgumentNullException("error");
            }

            var errorXml = ErrorXml.EncodeString(error);
            var id = Guid.NewGuid();
            var redisKey = GetRedisKey(id.ToString());
            var errorLog = new RedisErrorData
            {
                Id = id,
                ErrorTime = error.Time,
                ErrorXml = errorXml
            };

            using (var redisClient = this.GetRedisClient())
            {
                redisClient.Add(redisKey, errorLog);
            }

            return id.ToString();
        }

        /// <summary>
        /// Get the specific error by Id.
        /// </summary>
        /// <param name="id">id of the error.</param>
        /// <returns>
        /// returns the ErrorLogEntry
        /// </returns>
        /// <exception cref="System.ArgumentNullException">id passed in exception</exception>
        /// <exception cref="System.ArgumentException">null;id
        /// or
        /// id</exception>
        public override ErrorLogEntry GetError(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (id.Length == 0)
            {
                throw new ArgumentException(null, "id");
            }

            Guid errorGuid;

            try
            {
                errorGuid = new Guid(id);
            }
            catch (FormatException e)
            {
                throw new ArgumentException(e.Message, "id", e);
            }

            var redisKey = GetRedisKey(errorGuid.ToString());

            RedisErrorData loggedError;
            using (var redisClient = this.GetRedisClient())
            {
                loggedError = redisClient.Get<RedisErrorData>(redisKey);
            }

            var errorXml = loggedError.ErrorXml;

            if (errorXml == null)
            {
                return null;
            }

            var error = ErrorXml.DecodeString(errorXml);
            return new ErrorLogEntry(this, id, error);
        }

        /// <summary>
        /// Gets the Errors from the REDIS DB
        /// </summary>
        /// <param name="pageIndex">current page index</param>
        /// <param name="pageSize">page size in integer</param>
        /// <param name="errorEntryList">Error entry list.</param>
        /// <returns>
        /// return count of errors.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">pageIndex;null
        /// or
        /// pageSize;null</exception>
        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            if (pageIndex < 0)
            {
                throw new ArgumentOutOfRangeException("pageIndex", pageIndex, null);
            }

            if (pageSize < 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, null);
            }

            if (errorEntryList == null)
            {
                errorEntryList = new ArrayList();
            }

            IEnumerable<RedisErrorData> allLoggedErrors;
            using (var redisClient = this.GetRedisClient())
            {
                var keyPattern = GetRedisKey("*");
                var errorLogKeys = redisClient.SearchKeys(keyPattern);

                allLoggedErrors
                    = redisClient
                        .GetAll<RedisErrorData>(errorLogKeys)
                        .Values
                        .OrderByDescending(eachLog => eachLog.ErrorTime);
            }

            foreach (var eachLog in allLoggedErrors.Skip(pageSize * pageIndex).Take(pageSize))
            {
                var error = ErrorXml.DecodeString(eachLog.ErrorXml);
                errorEntryList.Add(new ErrorLogEntry(this, eachLog.Id.ToString(), error));
            }

            return allLoggedErrors.Count();
        }

        /// <summary>
        /// Gets the redis key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns String Value</returns>
        private static string GetRedisKey(string id)
        {
            return "elmah-error:" + id;
        }

        /// <summary>
        /// Gets the redis client.
        /// </summary>
        /// <returns>Returns Interface of Redis Client</returns>
        private IRedisClient GetRedisClient()
        {
            var redisEndpoint = new RedisEndpoint(this.host, this.port);

            var redisClient = this.redisClientFactory.CreateRedisClient(redisEndpoint);
            redisClient.Db = this.db;

            return redisClient;
        }

        /// <summary>
        /// The Redis Error Data
        /// </summary>
        private sealed class RedisErrorData
        {
            /// <summary>
            /// Gets or sets the error time.
            /// </summary>
            /// <value>
            /// The error time.
            /// </value>
            public DateTime ErrorTime { get; set; }

            /// <summary>
            /// Gets or sets the error XML.
            /// </summary>
            /// <value>
            /// The error XML.
            /// </value>
            public string ErrorXml { get; set; }

            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public Guid Id { get; set; }
        }
    }
}