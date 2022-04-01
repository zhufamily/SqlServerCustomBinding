using SqlServerCustomBinding;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: WebJobsStartup(typeof(SqlServerStartup))]
namespace SqlServerCustomBinding
{
    /// <summary>
    /// Custom binding Web Job initializer
    /// </summary>
    public class SqlServerStartup : IWebJobsStartup
    {
        /// <summary>
        /// Add custom binding extensions to Web Job
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Configure(IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<SqlServerClient>();
            builder.AddExtension<SqlServerOutput >();
        }
    }
}
