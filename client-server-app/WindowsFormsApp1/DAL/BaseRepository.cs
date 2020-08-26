using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAL
{
    public abstract class BaseRepository
    {
        private readonly string _ConnectionString;

        protected BaseRepository(string login, string pass)
        {
            string format = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            _ConnectionString = string.Format(format, login, pass);
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
