using Microsoft.Azure.WebJobs.Description;

namespace SqlServerCustomBinding
{
    /// <summary>
    /// Annotation class for SqlServerClient
    /// </summary>
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public class SqlServerClientAttribute : Attribute
    {
        /// <summary>
        /// Connection string property
        /// </summary>
        [AutoResolve]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Constructor for annotation class
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlServerClientAttribute(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
