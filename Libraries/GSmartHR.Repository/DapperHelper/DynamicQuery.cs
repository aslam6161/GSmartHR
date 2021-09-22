using GSmartHR.Repository.DapperHelper.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GSmartHR.Repository.DapperHelper
{

    public static class DynamicQuery
    {
        private static CSharpDataTypes _cSharpDataTypes = new CSharpDataTypes();
        public static IgnoreFieldResult IgnoreField<T>(this object item, Expression<Func<T>> property)
        {
            var propertyName = ((MemberExpression)property.Body).Member.Name;

            var propertyObj = item.GetType().GetProperty(propertyName);

            IgnoreFieldResult ignoreFieldResult = new IgnoreFieldResult();

            List<string> properties = new List<string>();

            properties.Add(propertyName);

            ignoreFieldResult.Object = item;

            ignoreFieldResult.Properties = properties;

            return ignoreFieldResult;
        }

        public static IgnoreFieldResult IgnoreField<T>(this IgnoreFieldResult item, Expression<Func<T>> property)
        {
            var propertyName = ((MemberExpression)property.Body).Member.Name;
            var propertyObj = item.GetType().GetProperty(propertyName);
            item.Properties.Add(propertyName);

            return item;
        }

        public static QueryResult GetInsertQuery(this IgnoreFieldResult ignoreFieldResult, string tableName = "")
        {
            var result = GetInsertQueryByProperty(ignoreFieldResult.Object, tableName, ignoreFieldResult.Properties);

            return result;
        }

        public static QueryResult GetInsertQuery(this object item, string tableName = "")
        {
            var result = GetInsertQueryByProperty(item, tableName);

            return result;
        }

        public static QueryResult GetUpdateQuery(this IgnoreFieldResult ignoreFieldResult, string tableName = "")
        {
            var result = GetUpdateQueryByProperty(ignoreFieldResult.Object, tableName, ignoreFieldResult.Properties);

            return result;
        }

        public static QueryResult GetUpdateQuery(this object item, string tableName = "")
        {
            var result = GetUpdateQueryByProperty(item, tableName);

            return result;
        }

        public static QueryResult AppendUpdateQuery(this QueryResult insertQuery, QueryResult updateQuery)
        {
            var query = updateQuery.Sql;
            query += "\n";
            query += "IF @@ROWCOUNT = 0";
            query += "\n";
            query += insertQuery.Sql;
            return new QueryResult(query, insertQuery.Param);
        }

        public static QueryResult Where<T>(Expression<Func<T, bool>> expression)
        {
            var type = typeof(T).Name;
            var tableName = "[" + type + "]";

            var queryProperties = new List<QueryParameter>();
            var body = (BinaryExpression)expression.Body;
            IDictionary<string, Object> expando = new ExpandoObject();

            var builder = new StringBuilder();

            // walk the tree and build up a list of query parameter objects
            // from the left and right branches of the expression tree
            ExpressionToSql.WalkTree(body, ExpressionType.Default, ref queryProperties);

            // convert the query parms into a SQL string and dynamic property object
            builder.Append("SELECT * FROM ");
            builder.Append(tableName);

            builder.Append(" WHERE ");

            for (int i = 0; i < queryProperties.Count(); i++)
            {
                QueryParameter item = queryProperties[i];

                if (item.Left)
                {
                    builder.Append("(");
                    continue;
                }

                if (item.Right)
                {
                    builder.Append(")");

                    if (!string.IsNullOrEmpty(item.LinkingOperator))
                    {
                        builder.Append(" " + item.LinkingOperator + " ");
                    }

                    continue;
                }


                if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                {
                    builder.Append(string.Format("{0} {1} @{2} ", type + "." + item.PropertyName,
                                                 item.QueryOperator, type + "_" + item.PropertyName));
                }
                else
                {
                    builder.Append(string.Format("{0} {1} @{2} ", tableName + "." + item.PropertyName, item.QueryOperator, type + "_" + item.PropertyName));
                }


                expando[type + "_" + item.PropertyName] = item.PropertyValue;
            }

            return new QueryResult(builder.ToString().TrimEnd(), expando);
        }

        #region utility

        public static bool Allow(PropertyInfo prop)
        {
            if (!prop.CanRead)
            {
                return false;
            }

            if (!prop.CanWrite)
            {
                return false;
            }

            object[] attrs = prop.GetCustomAttributes(true);

            if(attrs!=null && attrs.Count()>0)
            {
                var exist = attrs.Any(x => x.GetType().Name.ToString().Contains("DatabaseGenerated"));
                if (exist)
                    return false;
            }

            return _cSharpDataTypes.InDataTypes(prop.PropertyType);
        }

        private static QueryResult GetInsertQueryByProperty(object item, string tableName = "", List<string> ignoreFields = null)
        {
            if (ignoreFields == null)
            {
                ignoreFields = new List<string>();
            }

            if (string.IsNullOrEmpty(tableName))
            {
                var isAnonymousType = item.GetType().GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Count() > 0;
                tableName = item.GetType().Name;
                if (isAnonymousType)
                {
                    throw new Exception("Please specify table name . Table name ca not be empty for Anonymous Type!");
                }
            }

            if (!tableName.StartsWith("["))
            {
                tableName = "[" + tableName + "]";
            }

            PropertyInfo[] props = item.GetType().GetProperties().Where(prop => Allow(prop)).ToArray();

            string[] columns = props.Where(p => !ignoreFields.Any(x => x == p.Name)).Select(p => p.Name).ToArray();

            var query = string.Format("INSERT INTO {0} ({1}) VALUES (@{2})",
                                 tableName,
                                 string.Join(",", columns.Select(x => string.Format("[{0}]", x))),
                                 string.Join(",@", columns));

            var valueOfparameters = GetParameters(item);

            return new QueryResult(query, valueOfparameters);
        }



        private static QueryResult GetUpdateQueryByProperty(object item, string tableName = "", List<string> ignoreFields = null)
        {
            if (ignoreFields == null)
            {
                ignoreFields = new List<string>();
            }

            if (string.IsNullOrEmpty(tableName))
            {
                var isAnonymousType = item.GetType().GetCustomAttributes(typeof(CompilerGeneratedAttribute), true).Count() > 0;
                tableName = item.GetType().Name;
                if (isAnonymousType)
                {
                    throw new Exception("Please specify table name . Table name ca not be empty for Anonymous Type!");
                }
            }

            if (!tableName.StartsWith("["))
            {
                tableName = "[" + tableName + "]";
            }

            PropertyInfo[] props = item.GetType().GetProperties().Where(prop => Allow(prop)).ToArray();

            string[] columns = props.Where(p => !ignoreFields.Any(x => x == p.Name)).Select(p => p.Name).ToArray();


            var parameters = columns.Select(name => name + "=@" + name).ToList();

            var query = string.Format("UPDATE {0} SET {1} WHERE Id=@Id", tableName, string.Join(",", parameters));

            var valueOfparameters = GetParameters(item);

            return new QueryResult(query, valueOfparameters);

        }


        private static object GetParameters(object item)
        {
            PropertyInfo[] props = item.GetType().GetProperties().Where(prop => Allow(prop)).ToArray();

            dynamic obj = new ExpandoObject();

            foreach (var prop in props)
            {
                var name = prop.Name;
                var value = prop.GetValue(item, null);
                ((IDictionary<string, object>)obj).Add(name, value);

            }

            return obj;
        }


        #endregion
    }
}
