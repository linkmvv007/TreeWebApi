﻿using Microsoft.Extensions.Options;

namespace TreeWebApi.Options;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private const string DatabaseOptionName = "DatabaseOptions";

    private readonly IConfiguration _configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString("Database");

        _configuration.GetSection(DatabaseOptionName).Bind(options);
    }
}
