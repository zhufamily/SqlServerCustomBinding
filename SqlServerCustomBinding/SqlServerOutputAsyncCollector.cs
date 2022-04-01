using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace SqlServerCustomBinding
{
    /// <summary>
    /// SqlServerOutput custom binding implementation 
    /// </summary>
    public class SqlServerOutputAsyncCollector : IAsyncCollector<SqlCommand>
    {
        /// <summary>
        /// SqlServerOutput attribute
        /// </summary>
        readonly SqlServerOutputAttribute attr;
        /// <summary>
        /// Multiple Sql commands
        /// </summary>
        private List<SqlCommand> sqlCommands;

        /// <summary>
        /// Constructor from Sql Server Aysnc Collector -- read from annotation
        /// </summary>
        /// <param name="attr"></param>
        public SqlServerOutputAsyncCollector(SqlServerOutputAttribute attr)
        {
            this.attr = attr;
            sqlCommands = new List<SqlCommand>();
        }

        /// <summary>
        /// Add a command to multiple sql commands
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddAsync(SqlCommand sqlCommand, CancellationToken cancellationToken = default(CancellationToken))
        {
            sqlCommands.Add(sqlCommand);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Execute all sql commands
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(attr.Connection))
                throw new ArgumentException("Missing required parameter ConnectionString");

            try
            {
                if (sqlCommands.Count() > 0)
                {
                    if (attr.UseTransaction)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            using (SqlConnection conn = new SqlConnection(attr.Connection))
                            {
                                conn.Open();

                                foreach (SqlCommand comm in sqlCommands)
                                {
                                    comm.Connection = conn;
                                    comm.ExecuteNonQuery();
                                }
                            }
                            scope.Complete();
                        }
                    }
                    else
                    {
                        using (SqlConnection conn = new SqlConnection(attr.Connection))
                        {
                            conn.Open();

                            if (attr.ContinueOnError)
                            {
                                foreach (SqlCommand comm in sqlCommands)
                                {
                                    try
                                    {
                                        comm.Connection = conn;
                                        comm.ExecuteNonQuery();
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine($"Continue after error ...");
                                    }
                                }
                            }
                            else
                            {
                                foreach (SqlCommand comm in sqlCommands)
                                {
                                    comm.Connection = conn;
                                    comm.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    sqlCommands.Clear();
                }
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
