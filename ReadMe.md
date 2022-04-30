# Sql Server Custom Binding
## _A custom binding for Sql Server in Azure function_
## Features

- output binding for Sql Server commands
- Input / output binding for Sql Server Connection
## Dependencies
- .Net 6.x
- Microsoft.Azure.WebJob 3.0.32+
- Microsoft.Azure.WebJob.Core 3.0.32+
- Microsoft.Data.SqlClient 4.1.0+

## Installation

It is available from public Nuget source

Install the package and all dependencies.

```sh
PM> Install-Package SqlServerCustomBinding -version <latest_version>
```

## Samples

SQL Server Output Binding can be used as in the following sample

```sh
[FunctionName("<your_function_name>")]
public static async Task<IActionResult> <your_function_name>(
    <your_function_trigger>,
    [SqlServerOutput("%<your_sql_conn_string>%", UseTransaction = false)] IAsyncCollector<SqlCommand> requests,
    ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");
    SqlCommand comm = new SqlCommand("Insert Into books(ID, [Name]) values(2, 'book2')");
    await requests.AddAsync(comm);
    return new OkObjectResult("All good!");
}
```

For connection string information, please refer to https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring?view=sqlclient-dotnet-standard-4.1, and for SqlCommand please refer to https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlclient.sqlcommand?view=sqlclient-dotnet-standard-4.1.  You do not have to set up Connection property for the SqlCommands, and it will be automatically populated with your connection string.  All SqlCommands will NOT return anything, such as reader or etc.  If you need more complicated operation, please see the following sample for input / output binding.

```sh
[FunctionName("<your_function_name>")]
public static async Task<IActionResult> your_function_name(
    <your_function_trigger>,
    [SqlServerClient("%<your_sql_conn_string>%")] SqlConnection conn,
    ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");
    using(conn)
    {
        SqlCommand comm = new SqlCommand("select * from books", conn);
        conn.Open();
        SqlDataReader reader = comm.ExecuteReader();
        while (reader.Read())
        {
            // your actions with the record
        }
    }
    return new OkObjectResult("All good!");
}
```
You can use input / output binding to read / write data from Sql Server or whatever you can do with SqlConnection.

## License

Free software, absolutely no warranty, use at your own risk!
