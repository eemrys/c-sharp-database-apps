using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WebApplication1
{
    public abstract class BaseRepository
    {
        private readonly string _ConnectionString;

        protected BaseRepository()
        {
            _ConnectionString = "Data Source=DESKTOP-OV46CEP;Initial Catalog=Staff;Persist Security Info=True;User ID=admin;Password=Larrysvodka69";
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> task)
        {
            try
            {
                using (var connection = new SqlConnection(_ConnectionString))
                {
                    await connection.OpenAsync();
                    return await task(connection).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
