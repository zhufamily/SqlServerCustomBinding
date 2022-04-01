using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Data.SqlClient;

namespace SqlServerCustomBinding
{
    /// <summary>
    /// SqlServer output implementation class
    /// </summary>
    [Extension("Dynamics365Output")]
    public class SqlServerOutput : IExtensionConfigProvider
    {
        /// <summary>
        /// Initialize SqlServerOutput custom binding extension configuration
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<SqlServerOutputAttribute>();
            rule.BindToCollector<SqlCommand>(attr => new SqlServerOutputAsyncCollector(attr));
        }
    }
}
