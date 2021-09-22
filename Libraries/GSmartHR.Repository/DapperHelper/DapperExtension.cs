using Dapper;
using GSmartHR.Repository.DapperHelper;
using GSmartHR.Repository.DapperHelper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MGSmartHR.Repository.DapperHelper
{
    public static class DapperExtension
    {
        public static void InsertItem(this IDbConnection cn, object item, string tableName = "")
        {
            var insertQuery = item.GetInsertQuery(tableName);
            cn.Execute(insertQuery.Sql, insertQuery.Param);
        }

        public static void InsertItems<T>(this IDbConnection cn, ICollection<T> list)
        {
            foreach (var item in list)
            {
                var insertQuery = item.GetInsertQuery();

                cn.Execute(insertQuery.Sql, insertQuery.Param);
            }
        }

        public static void UpdateItem(this IDbConnection cn, object item, string tableName = "")
        {
            var updateQuery = item.GetUpdateQuery(tableName);
            cn.Execute(updateQuery.Sql, updateQuery.Param);
        }

        public static void UpdateItems<T>(this IDbConnection cn, ICollection<T> list)
        {
            foreach (var item in list)
            {
                var updateQuery = item.GetUpdateQuery();
                cn.Execute(updateQuery.Sql, updateQuery.Param);
            }
        }

        public static void InsertOrUpdateItem(this IDbConnection cn, object item, string[] ignoreFieldsOnUpdate = null, string tableName = "")
        {
            IgnoreFieldResult _IgnoreFieldResult = new IgnoreFieldResult();

            _IgnoreFieldResult.Properties = new List<string>();

            if (ignoreFieldsOnUpdate == null)
            {
                ignoreFieldsOnUpdate = new string[] { "CreatedOn" };
            }
            var insertQuery = item.GetInsertQuery();
            _IgnoreFieldResult.Object = item;
            var updateQuery = _IgnoreFieldResult.GetUpdateQuery();
            var insertOrUpdateQuery = insertQuery.AppendUpdateQuery(updateQuery);
            cn.Execute(insertOrUpdateQuery.Sql, insertOrUpdateQuery.Param);
        }


        public static void InsertOneToManyItems<T>(this IDbConnection cn, ICollection<T> list)
        {
            foreach (var item in list)
            {
                var insertQuery = item.GetInsertQuery();

                cn.Execute(insertQuery.Sql, insertQuery.Param);
            }
        }

        public static void UpdateOneToManyItems<T>(this IDbConnection cn, ICollection<T> list, Expression<Func<T,Guid>> foreignKey,object value, string[] ignoreFieldsOnUpdate = null)
        {

            IgnoreFieldResult _IgnoreFieldResult = new IgnoreFieldResult();

            _IgnoreFieldResult.Properties = new List<string>();

            if (ignoreFieldsOnUpdate == null)
            {
                ignoreFieldsOnUpdate = new string[] { "CreatedOn" };
            }

            _IgnoreFieldResult.Properties.AddRange(ignoreFieldsOnUpdate);

            var _tableName = string.Format("[{0}]", typeof(T).Name);

            string parameterName = ((MemberExpression)foreignKey.Body).Member.Name;;
            var parameterValue = value;


            var deleteQuery = "Delete from " + _tableName + " where [" + parameterName + "]=@ForeignKey and Id not in (@Ids)";

            var ids = list.Select(x => x.GetType().GetProperty("Id").GetValue(x, null)).ToList();

            if (ids.Count() == 0)
            {
                ids.Add(Guid.Empty);
            }



            cn.Execute(deleteQuery, new { ForeignKey = parameterValue, Ids = string.Join(",",ids) });

            foreach (var item in list)
            {
                var insertQuery = item.GetInsertQuery();
                _IgnoreFieldResult.Object = item;
                var updateQuery = _IgnoreFieldResult.GetUpdateQuery();
                var insertOrUpdateQuery = insertQuery.AppendUpdateQuery(updateQuery);
                cn.Execute(insertOrUpdateQuery.Sql, insertOrUpdateQuery.Param);
            }
        }
    }
}
