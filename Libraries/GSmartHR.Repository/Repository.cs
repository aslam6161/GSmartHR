using GSmartHR.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using GSmartHR.Core;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using MGSmartHR.Repository.DapperHelper;

namespace GSmartHR.Repository
{
    public partial class Repository<T> : GSmartHR.IRepository.IRepository<T> where T : BaseEntity
    {
        protected readonly string _tableName;
        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
        private string _connectionString;
        public Repository(IConfiguration config)
        {
            _tableName = string.Format("[{0}]", typeof(T).Name);
            _connectionString = config.GetConnectionString("GSmartHRConnectionString");

        }

        public T GetById(object id)
        {
            T item = default(T);
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE Id=@Id", new { Id = id }).SingleOrDefault();
                cn.Close();
            }
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>("SELECT * FROM " + _tableName);
                cn.Close();
            }
            return items;
        }


        public void Insert(T entity)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.InsertItem(entity);
                cn.Close();
            }
        }

        public void Insert(IEnumerable<T> entities)
        {
      
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.InsertItems(entities.ToList());
                cn.Close();
            }
        }


        public void InsertAll(IEnumerable<T> entities)
        {

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.InsertItems(entities.ToList());
                cn.Close();
            }
        }


        public void Update(T entity)
        {
      
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.UpdateItem(entity);
                cn.Close();
            }
        }

        public void Update(IEnumerable<T> entities)
        {

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.UpdateItems(entities.ToList());
                cn.Close();
            }
        }

        public void Delete(T entity)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + _tableName + " WHERE Id=@Id", new { Id = entity.Id });
                cn.Close();
            }
        }
        public void Delete(IEnumerable<T> entities)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                cn.Execute("DELETE FROM " + _tableName + " WHERE Id=@Id", entities.Select(x => new { Id = x.Id }));

                cn.Close();
            }
        }

    }
}
