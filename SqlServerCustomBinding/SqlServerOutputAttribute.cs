using Microsoft.Azure.WebJobs.Description;

namespace SqlServerCustomBinding
{
    /// <summary>
    /// Dynamics365Output annotation attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.ReturnValue | AttributeTargets.Parameter)]
    [Binding]
    public sealed class SqlServerOutputAttribute : Attribute
    {
        /// <summary>
        /// Dynamics 365 connection string
        /// </summary>
        [AutoResolve]
        public string Connection { get; set; }
        /// <summary>
        /// Continue execution for errors, only applies when Trx is not used
        /// </summary>
        public bool ContinueOnError { get; set; }
        /// <summary>
        /// Whether to wrap all commands in one transaction
        /// </summary>
        public bool UseTransaction { get; set; }

        /// <summary>
        /// Constructor set ContinueOnError to false as default
        /// </summary>
        /// <param name="connection"></param>
        public SqlServerOutputAttribute(string connection)
        {
            Connection = connection;
            ContinueOnError = false;
            UseTransaction = false;
        }
    }
}
