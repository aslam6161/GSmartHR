using GSmartHR.Core.Domain.Users;
using GSmartHR.IRepository.Users;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Linq;

namespace GSmartHR.Repository.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IConfiguration config) : base(config)
        {
        }


        public IEnumerable<User> GetUsersByUserType(int userType)
        {
            IEnumerable<User> items = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<User>("SELECT * FROM " + _tableName + " WHERE UserTypeId=@UserTypeId Order by CreatedOn desc", new { UserTypeId = userType });
                cn.Close();
            }
            return items;
        }

        public User GetUserByEmail(string usernameOrEmail)
        {
            var query = "Select * from [User] where (Email=@Email)";

            User item = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                item = cn.Query<User>(query, new { Email = usernameOrEmail }).FirstOrDefault();

                cn.Close();
            }
            return item;
        }



        public bool UserAlreadyExist(string userId, Guid id)
        {
            userId = userId.Trim().ToLower();

            var query = "Select count(1) from [User] where Username=@Username and Id!=@Id ";
            var result = 0;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                result = cn.ExecuteScalar<int>(query, new { Username = userId, Id = id });
                cn.Close();
            }

            return result > 0;
        }


        public User GetUserByUserId(string usernameOrEmail)
        {
            var query = "Select * from [User] where (Username=@username)";

            User item = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                item = cn.Query<User>(query, new { Username = usernameOrEmail }).FirstOrDefault();

                cn.Close();
            }
            return item;
        }

        public IEnumerable<UserDetailsContainer> GetUsersByUsernameAndRoleName(string username, string rolename,int pageno,int pagesize)
        {
            IEnumerable<UserDetailsContainer> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();

                items = cn.Query<UserDetailsContainer>("GetUsersByUsernameAndRoleName", new { username = username, Userrole = rolename, pageno= pageno, pagesize = pagesize },commandType: CommandType.StoredProcedure);

                cn.Close();
            }
            return items;
        }
    }
}
