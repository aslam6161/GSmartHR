using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace GSmartHR.Repository.DapperHelper
{

    public static class DapperQueryExtension
    {
        public static IEnumerable<TParent> OneToManyQuery<TParent, TChild, TParentKey>(
        this IDbConnection connection,
        string sql,
        Func<TParent, TParentKey> parentKeySelector,
        Func<TParent, ICollection<TChild>> childSelector,
        dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            connection.Query<TParent, TChild, TParent>(
                sql,
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    ICollection<TChild> children = childSelector(cachedParent);
                    if (child != null)
                    {
                        children.Add(child);
                    }
                    return cachedParent;
                },
                param as object, transaction, buffered, splitOn, commandTimeout, commandType);

            return cache.Values;
        }


        public static IEnumerable<TParent> ReadOneToManyData<TParent, TChild, TParentKey>(
        this GridReader multi,
        Func<TParent, TParentKey> parentKeySelector,
        Func<TParent, ICollection<TChild>> childSelector)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();

            multi.Read<TParent, TChild, TParent>(
                (parent, child) =>
                {
                    if (!cache.ContainsKey(parentKeySelector(parent)))
                    {
                        cache.Add(parentKeySelector(parent), parent);
                    }

                    TParent cachedParent = cache[parentKeySelector(parent)];
                    ICollection<TChild> children = childSelector(cachedParent);
                    if (child != null)
                    {
                        children.Add(child);
                    }
                    return cachedParent;
                });

            return cache.Values;
        }

        public static bool IsChildObjDuplicate<TChild, TParentKey, TChildKey>(TChild child, Func<TChild, TChildKey> childKeySelector, ref Dictionary<TChildKey, TChild> items)
        {
            if (child != null && !items.ContainsKey(childKeySelector(child)))
            {
                if (child != null && !items.ContainsKey(childKeySelector(child)))
                {
                    items.Add(childKeySelector(child), child);

                    return false;
                }

            }

            return true;
        }

        public static void AddParentData<TParent, TParentKey>(TParent parent, Func<TParent, TParentKey> parentKeySelector, ref Dictionary<TParentKey, TParent> items)
        {
            if (!items.ContainsKey(parentKeySelector(parent)))
            {
                items.Add(parentKeySelector(parent), parent);
            }

        }

    }


}
