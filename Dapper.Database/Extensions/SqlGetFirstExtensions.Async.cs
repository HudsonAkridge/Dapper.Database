﻿using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Database.Adapters;
using Dapper.Mapper;

namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Database extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region GetFirstAsync Queries
        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>enumerable list of entities</returns>
        public static async Task<T> GetFirstAsync<T>(this IDbConnection connection, string sql = null, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return await connection.GetFirstAsync<T>(adapter, sql ?? "where 1 = 1", null, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T> GetFirstAsync<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var adapter = GetFormatter(connection);
            return await connection.GetFirstAsync<T>(adapter, sql, parameters, transaction, commandTimeout);
        }


        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return (await connection.QueryAsync<T1, T2>(sql, null, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return (await connection.QueryAsync<T1, T2>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return (await connection.QueryAsync<T1, T2, T3>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return (await connection.QueryAsync<T1, T2, T3>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();
        }


        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return (await connection.QueryAsync<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<T1> GetFirstAsync<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return (await connection.QueryAsync<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return (await connection.QueryAsync<T1, T2, TRet>(sql, mapper, null, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return (await connection.QueryAsync<T1, T2, TRet>(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return (await connection.QueryAsync<T1, T2, T3, TRet>(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return (await connection.QueryAsync<T1, T2, T3, TRet>(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3) }))).FirstOrDefault();
        }


        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return (await connection.QueryAsync<T1, T2, T3, T4, TRet>(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static async Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return (await connection.QueryAsync<T1, T2, T3, T4, TRet>(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: splitOn ?? SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) }))).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first matching T.  
        /// </summary>
        /// <param name="connection">Sql Connection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="sql">The where clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>True if records are deleted</returns>
        private static async Task<T> GetFirstAsync<T>(this IDbConnection connection, ISqlAdapter adapter, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var selectSql = adapter.GetListQuery(tinfo, sql);
            return await connection.QueryFirstOrDefaultAsync<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout);
        }

        #endregion


    }
}
