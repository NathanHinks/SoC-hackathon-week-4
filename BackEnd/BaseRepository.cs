
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System;


public class BaseRepository
{
    IConfiguration _configuration;
    public BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // Generate new connection based on connection string
    private NpgsqlConnection SqlConnection()
    {
        // This builds a connection string from our separate credentials
        // TODO: add your connection settings
        var stringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = _configuration["PGHOST"], // e.g. ec2-1-2-3-4@eu-west-1.compute.amazonaws.com
            Database = _configuration["PGDATABASE"], // e.g. ksdjfhsafnfas
            Username = _configuration["PGUSER"], // e.g. lksfhdslkfsdflk
            Password = _configuration["PGPASSWORD"],// e.g KJZDldfj34567dslkfjsdlfksdjflsdkfjsdlkfjsdl34567fkjdsgDRTYUI
            Port = Int32.Parse(_configuration["PGPORT"]), // e.g 5432
            SslMode = Npgsql.SslMode.Require, // Heroku Specific Setting
            TrustServerCertificate = true // Heroku Specific Setting
        };

        return new NpgsqlConnection(stringBuilder.ConnectionString);
    }

    // Open new connection and return it for use
    public IDbConnection CreateConnection()
    {
        var connection = SqlConnection();
        connection.Open();
        return connection;
    }

}