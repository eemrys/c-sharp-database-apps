using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using static WebApplication1.Controllers.DataController;

namespace WebApplication1
{
    public class ChildRepository : BaseRepository
    {
        public ChildRepository(): base() { }

        public async Task<Child[]> GetChildren()
        {
            string query = "select ChildID as Id, LastName as Lastname, FirstName as Firstname, Gender, BirthDate as Birthdate from Children where ChildID not in (select ChildID from ParentChildRelation)";
            Child[] arr = null;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        arr = (await c.QueryAsync<Child>(query, transaction: t).ConfigureAwait(false)).ToArray();
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return arr;
            }).ConfigureAwait(false);
        }


        public async Task<int> AddChild(Child child)
        {
            int res = 0;
            string query = "insert into Children values (@Lastname, @Firstname, @Gender, CONVERT(date, @Birthdate, 103))";
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync(query, child, transaction: t).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> RemoveChild(int id)
        {
            int res = 0;
            string query = "delete from Children where ChildID = @id";
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync(query, new { id }, transaction: t).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }
    }
}
