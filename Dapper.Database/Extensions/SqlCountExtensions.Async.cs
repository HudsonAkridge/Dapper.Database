﻿using System.Data;
using System.Threading.Tasks;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region CountAsync Extensions
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Return Total Count of matching records</returns>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            return await connection.CountAsync<T>(sql, null, transaction, commandTimeout);
        }

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>Return Total Count of matching records</returns>
        public static async Task<int> CountAsync<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            return await connection.ExecuteScalarAsync<int>(adapter.CountQuery(tinfo, sql), parameters, transaction, commandTimeout);
        }
        #endregion

    }
}
