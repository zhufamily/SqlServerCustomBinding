using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Data.SqlClient;

namespace SqlServerCustomBinding
{
    /// <summary>
    /// SqlServerClient implementation class
    /// </summary>
    [Extension("SqlServerClient")]
    public class SqlServerClient : IExtensionConfigProvider
    {
        /// <summary>
        /// Initialize the SqlServerClient class
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<SqlServerClientAttribute>();
            rule.BindToInput<SqlConnection>(BuildItemFromAttribute);
        }

        /// <summary>
        /// Build for SqlServerClient to get SqlConnection object
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private SqlConnection BuildItemFromAttribute(SqlServerClientAttribute arg)
        {
            if (string.IsNullOrEmpty(arg.ConnectionString))
                throw new ArgumentException("Missing required parameter ConnectionString");

            try
            {
                SqlConnection conn = new SqlConnection(arg.ConnectionString);
                return conn;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

