﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Dapper.Database
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface ISqlDatabase : IDisposable
    {

        //Task<int> CountAsync(string sql = null);

        #region Execute Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<int> ExecuteAsync(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<int> ExecuteAsync(string sql, object parameters);

        #endregion

        #region ExecuteScaler Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<T> ExecuteScalerAsync<T>(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<T> ExecuteScalerAsync<T>(string sql, object parameters);

        #endregion

        //        #region GetDataTable Methods
        //#if !NETSTANDARD1_3 && !NETCOREAPP1_0
        //        /// <summary>
        //        /// Execute a query
        //        /// </summary>
        //        /// <param name="sql">The sql clause to count</param>
        //        /// <returns>Return Total Count of matching records</returns>
        //        Task<DataTable> GetDataTableAsync(string sql);

        //        /// <summary>
        //        /// Execute a query
        //        /// </summary>
        //        /// <param name="sql">The sql clause to count</param>
        //        /// <param name="parameters">The parameters of the clause</param>
        //        /// <returns>Return Total Count of matching records</returns>
        //        Task<DataTable> GetDataTableAsync(string sql, object parameters);
        //#endif
        //        #endregion

        #region GetMultiple Methods
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<GridReader> GetMultipleAsync(string sql);

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the clause</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<GridReader> GetMultipleAsync(string sql, object parameters);
        #endregion

        #region Count Methods
        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<int> CountAsync(string sql);

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<int> CountAsync(string sql, object parameters);

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<int> CountAsync<T>(string sql = null) where T : class;

        /// <summary>
        /// Count of entities
        /// </summary>
        /// <param name="sql">The sql clause to count</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>Return Total Count of matching records</returns>
        Task<int> CountAsync<T>(string sql, object parameters) where T : class;

        #endregion

        #region Exists Methods
        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>true if record is found</returns>
        Task<bool> ExistsAsync(string sql = null);

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if record is found</returns>
        Task<bool> ExistsAsync(string sql, object parameters);

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToExists">Entity to delete</param>
        /// <returns>true if record is found</returns>
        Task<bool> ExistsAsync<T>(T entityToExists) where T : class;

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="primaryKey">a Single primary key to check</param>
        /// <returns>true if record is found</returns>
        Task<bool> ExistsAsync<T>(object primaryKey) where T : class;

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <returns>true if record is found</returns>
        Task<bool> ExistsAsync<T>(string sql = null) where T : class;

        /// <summary>
        /// Check if a record exists
        /// </summary>
        /// <param name="sql">The sql clause to check for existence</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if record is found</returns>
        Task<bool> ExistsAsync<T>(string sql, object parameters) where T : class;

        #endregion

        #region Get Methods
        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToGet">Entity to Retrieve with keys populated</param>
        /// <returns>the entity, else null</returns>
        Task<T> GetAsync<T>(T entityToGet) where T : class;

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <returns>the entity, else null</returns>
        Task<T> GetAsync<T>(object primaryKey) where T : class;

        /// <summary>
        /// Returns a single entity of type 'T'.  
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <returns>the entity, else null</returns>
        Task<T> GetAsync<T>(string sql, object parameters) where T : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">The parameters of the sql</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a single entity of type 'T1'.  
        /// </summary>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;
        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;


        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a single entity of type 'TRet'.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The sql clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <param name="parameters">Parameters of the sql clause</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        #endregion

        #region GetFirst Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        Task<T> GetFirstAsync<T>(string sql = null) where T : class;

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T> GetFirstAsync<T>(string sql, object parameters) where T : class;


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetFirstAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetFirstAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetFirstAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetFirstAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<T1> GetFirstAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetFirstAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<TRet> GetFirstAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;
        #endregion

        #region GetList Methods
        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        Task<IEnumerable<T>> GetListAsync<T>(string sql = null) where T : class;

        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T>> GetListAsync<T>(string sql, object parameters) where T : class;


        /// <summary>
        /// Returns a list entities of type T.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T1>> GetListAsync<T1, T2>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T1>> GetListAsync<T1, T2, T3>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;


        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type T1.  
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<T1>> GetListAsync<T1, T2, T3, T4>(string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<TRet>> GetListAsync<T1, T2, TRet>(Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IEnumerable<TRet>> GetListAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;
        #endregion

        #region Get Paged List Methods
        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>enumerable list of entities</returns>
        Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql = null) where T : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T>> GetPageListAsync<T>(int page, int pageSize, string sql, object parameters) where T : class;


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class;


        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<T1>> GetPageListAsync<T1, T2, T3, T4>(int page, int pageSize, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Data mapping function</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, TRet>(int page, int pageSize, Func<T1, T2, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where TRet : class;

        /// <summary>
        /// Returns a paged list entities of type T.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, TRet>(int page, int pageSize, Func<T1, T2, T3, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where TRet : class;


        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        /// <summary>
        /// Returns a list entities of type TRet.  
        /// </summary>
        /// <param name="page">The page to request</param>
        /// <param name="pageSize">Number of records per page</param>
        /// <param name="mapper">Open SqlConnection</param>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">Parameters of the clause</param>
        /// <param name="splitOn">The field we should split the result on to return the next object</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<IPagedEnumerable<TRet>> GetPageListAsync<T1, T2, T3, T4, TRet>(int page, int pageSize, Func<T1, T2, T3, T4, TRet> mapper, string sql, object parameters, string splitOn = null) where T1 : class where T2 : class where T3 : class where T4 : class where TRet : class;

        #endregion

        #region Insert Methods

        /// <summary>
        /// Inserts an entity into table "Ts" and returns identity id or number of inserted rows if inserting a list.
        /// </summary>
        /// <typeparam name="T">The type to insert.</typeparam>
        /// <param name="entityToInsert">Entity to insert, can be list of entities</param>
        /// <returns>the entity to insert or the list of entities</returns>
        Task<bool> InsertAsync<T>(T entityToInsert) where T : class;

        #endregion

        #region Update Queries
        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpdateAsync<T>(T entityToUpdate) where T : class;


        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpdateAsync<T>(T entityToUpdate, IEnumerable<string> columnsToUpdate) where T : class;

        #endregion

        #region Upsert Queries

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert) where T : class;

        /// <summary>
        /// Updates entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate) where T : class;

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert, Action<T> insertAction, Action<T> updateAction) where T : class;

        /// <summary>
        /// Updates entity in table "Ts", checks if the entity is modified if the entity is tracked by the Get() extension.
        /// </summary>
        /// <typeparam name="T">Type to be updated</typeparam>
        /// <param name="entityToUpsert">Entity to be inserted or updated</param>
        /// <param name="columnsToUpdate">Columns to be updated</param>
        /// <param name="insertAction">Callback action when inserting</param>
        /// <param name="updateAction">Update action when updatinRg</param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpsertAsync<T>(T entityToUpsert, IEnumerable<string> columnsToUpdate, Action<T> insertAction, Action<T> updateAction) where T : class;
        #endregion

        #region Delete Methods
        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="entityToDelete">Entity to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<bool> DeleteAsync<T>(T entityToDelete) where T : class;

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="primaryKey">a Single primary key to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<bool> DeleteAsync<T>(object primaryKey) where T : class;

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<bool> DeleteAsync<T>(string sql = null) where T : class;

        /// <summary>
        /// Delete entity in table "Ts".
        /// </summary>
        /// <param name="sql">The where clause to delete</param>
        /// <param name="parameters">The parameters of the where clause to delete</param>
        /// <returns>true if deleted, false if not found</returns>
        Task<bool> DeleteAsync<T>(string sql, object parameters) where T : class;

        #endregion
    }
}
