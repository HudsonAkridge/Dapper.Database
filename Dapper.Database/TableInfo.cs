﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Dapper.Database.Extensions;
using static Dapper.Database.Extensions.SqlMapperExtensions;
#if NETSTANDARD1_3
using DataException = System.InvalidOperationException;
#else
using System.Threading;
#endif

#if NET451
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#endif

namespace Dapper.Database
{

    /// <summary>
    /// 
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tablenameMapper"></param>
        public TableInfo(Type type, TableNameMapperDelegate tablenameMapper)
        {
            if (tablenameMapper != null)
            {
                TableName = TableNameMapper(type);
            }
            else
            {
                //NOTE: This as dynamic trick should be able to handle both our own Table-attribute as well as the one in EntityFramework 
                var tableAttr = type
#if NETSTANDARD1_3
                .GetTypeInfo()
#endif
                .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;

                if (tableAttr != null)
                {
                    TableName = tableAttr.Name;
                    if (tableAttr.Schema != null)
                    {
                        SchemaName = tableAttr.Schema;
                    }
                }
                else
                {
                    TableName = type.Name + "s";
                    if (type.IsInterface() && TableName.StartsWith("I"))
                        TableName = TableName.Substring(1);
                }
            }

            ColumnInfos = type.GetProperties()
                .Where(t => t.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                .Select(t =>
                {
                    var columnAtt = t.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "ColumnAttribute") as dynamic;

                    var ci = new ColumnInfo
                    {
                        Property = t,
                        ColumnName = columnAtt?.Name ?? t.Name,
                        PropertyName = t.Name,
                        IsKey = t.GetCustomAttributes(true).Any(a => a is KeyAttribute),
                        IsIdentity = t.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute
                          && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption == DatabaseGeneratedOption.Identity),
                        IsGenerated = t.GetCustomAttributes(true).Any(a => a is DatabaseGeneratedAttribute
                            && (a as DatabaseGeneratedAttribute).DatabaseGeneratedOption != DatabaseGeneratedOption.None),
                        ExcludeOnSelect = t.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute)
                    };

                    ci.ExcludeOnInsert = ci.IsGenerated
                        || t.GetCustomAttributes(true).Any(a => a is IgnoreInsertAttribute)
                        || t.GetCustomAttributes(true).Any(a => a is ReadOnlyAttribute);

                    ci.ExcludeOnUpdate = ci.IsGenerated
                        || t.GetCustomAttributes(true).Any(a => a is IgnoreUpdateAttribute)
                        || t.GetCustomAttributes(true).Any(a => a is ReadOnlyAttribute);

                    return ci;
                })
                .ToArray();

            if (!ColumnInfos.Any(k => k.IsKey))
            {
                var idProp = ColumnInfos.FirstOrDefault(p => string.Equals(p.PropertyName, "id", StringComparison.CurrentCultureIgnoreCase));

                if(idProp != null)
                {
                    idProp.IsKey = idProp.IsGenerated = idProp.IsIdentity = idProp.ExcludeOnInsert = idProp.ExcludeOnUpdate = true;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ColumnInfo> ColumnInfos { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="withSchema"></param>
        /// <returns></returns>
        public string GetTableName(string format, bool withSchema = false) => 
            withSchema && !string.IsNullOrEmpty(SchemaName) ?  string.Format(format, SchemaName)  + "." : null + string.Format(format, TableName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetInsertColumns(string format) => string.Join(",",
                ColumnInfos.Where(ci => !ci.ExcludeOnInsert)
                .Select(ci => string.Format(format, ci.ColumnName)));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetInsertParameters() => string.Join(",",
                ColumnInfos.Where(ci => !ci.ExcludeOnInsert)
                .Select(ci => string.Format("@{0}", ci.PropertyName)));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnInfo> GetInsertGeneratedAndKey() => ColumnInfos.Where(ci => ci.IsGenerated && ci.IsKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public string GetUpdateValues(string format, IEnumerable<string> updateColumns) => string.Join(",",
            ColumnInfos.Where(ci => !ci.ExcludeOnUpdate && (updateColumns == null || !updateColumns.Any() || updateColumns.Contains(ci.PropertyName)))
            .Select(ci => string.Format(format, ci.ColumnName, ci.PropertyName)));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetUpdateWhere(string format) => string.Join(" and ",
                ColumnInfos.Where(ci => ci.IsKey)
                .Select(ci => string.Format(format, ci.ColumnName, ci.PropertyName)));
    }

    /// <summary>
    /// 
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGenerated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcludeOnInsert { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcludeOnUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ExcludeOnSelect { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo Property { get; set; }
    }

}
