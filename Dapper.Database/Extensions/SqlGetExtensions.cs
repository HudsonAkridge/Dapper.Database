﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper.Mapper;
using System.Collections.Concurrent;
using System.Reflection.Emit;

using Dapper;
using System.ComponentModel.DataAnnotations;

#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#else
using System.Threading;
#endif



namespace Dapper.Database.Extensions
{
    /// <summary>
    /// The Dapper.Contrib extensions for Dapper
    /// </summary>
    public static partial class SqlMapperExtensions
    {

        #region Get Queries
        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static T Get<T>(this IDbConnection connection, T entityToGet, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            if (entityToGet == null)
                throw new ArgumentException("Cannot Get null Object", nameof(entityToGet));

            var adapter = GetFormatter(connection);

            return connection.Get<T>(adapter, null, entityToGet, transaction, commandTimeout, true);
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static T Get<T>(this IDbConnection connection, object primaryKey, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            var tinfo = TableInfoCache(type);
            var key = tinfo.GetSingleKey("Get");
            var dynParms = new DynamicParameters();
            dynParms.Add(key.ColumnName, primaryKey);

            return connection.Get<T>(adapter, null, dynParms, transaction, commandTimeout, true);
        }

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>the entity, else null</returns>
        public static T Get<T>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
        {
            var type = typeof(T);
            var adapter = GetFormatter(connection);
            return connection.Get<T>(adapter, sql, parameters, transaction, commandTimeout);
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class
        {
            return connection.Query<T1, T2>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class
        {
            return connection.Query<T1, T2, T3>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3: class
        {
            return connection.Query<T1, T2, T3>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static T1 Get<T1, T2, T3, T4>(this IDbConnection connection, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return connection.Query<T1, T2, T3, T4>(sql, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static  T1 Get<T1, T2, T3, T4>(this IDbConnection connection, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return connection.Query<T1, T2, T3, T4>(sql, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return connection.Query<T1, T2, TRet>(sql, mapper, null, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, TRet>(this IDbConnection connection, Func<T1, T2, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where TRet : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, TRet>(this IDbConnection connection, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where TRet : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3) })).SingleOrDefault();
        }


        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class
        {
            return connection.Query(sql, mapper, new { }, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <returns>true if deleted, false if not found</returns>
        public static TRet Get<T1, T2, T3, T4, TRet>(this IDbConnection connection, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null) where T1 : class where T2 : class where T3 : class where T4 : class
        {
            return connection.Query(sql, mapper, parameters, transaction, commandTimeout: commandTimeout, splitOn: SplitOnArgument(new[] { typeof(T2), typeof(T3), typeof(T4) })).SingleOrDefault();
        }

        /// <summary>
        /// Returns a single entity of type T.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="connection">Open SqlConnection</param>
        /// <param name="adapter">ISqlAdapter for getting the sql statement</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the where clause</param>
        /// <param name="transaction">The transaction to run under, null (the default) if none</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout</param>
        /// <param name="fromCache">Cache the query.</param>
        /// <returns>the entity, else null</returns>
        private static T Get<T>(this IDbConnection connection, ISqlAdapter adapter, string sql, object parameters, IDbTransaction transaction = null, int? commandTimeout = null, bool fromCache = false) where T : class
        {
            var type = typeof(T);
            var tinfo = TableInfoCache(type);
            var selectSql = adapter.GetQuery(tinfo, sql, fromCache);

            return connection.Query<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout).SingleOrDefault();

            //T obj;

            //if (type.IsInterface())
            //{
            //    var res = connection.Query(selectSql, parameters, transaction, commandTimeout: commandTimeout).SingleOrDefault() as IDictionary<string, object>;

            //    if (res == null)
            //        return null;

            //    obj = ProxyGenerator.GetInterfaceProxy<T>();

            //    foreach (var property in tinfo.PropertyList)
            //    {
            //        var val = res[property.Name];
            //        if (val == null) continue;
            //        if (property.PropertyType.IsGenericType() && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            //        {
            //            var genericType = Nullable.GetUnderlyingType(property.PropertyType);
            //            if (genericType != null) property.SetValue(obj, Convert.ChangeType(val, genericType), null);
            //        }
            //        else
            //        {
            //            property.SetValue(obj, Convert.ChangeType(val, property.PropertyType), null);
            //        }
            //    }

            //    ((IProxy)obj).IsDirty = false;   //reset change tracking and return
            //}
            //else
            //{
            //    obj = connection.Query<T>(selectSql, parameters, transaction, commandTimeout: commandTimeout).SingleOrDefault();
            //}
            //return obj;
        }
        #endregion

    }
}
