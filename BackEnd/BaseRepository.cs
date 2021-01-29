
using System.Data;
using Npgsql;


public class BaseRepository
{
    // Generate new connection based on connection string
    private NpgsqlConnection SqlConnection()
    {
        // This builds a connection string from our separate credentials
        // TODO: add your connection settings
        var stringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "ec2-18-203-7-163.eu-west-1.compute.amazonaws.com", // e.g. ec2-1-2-3-4@eu-west-1.compute.amazonaws.com
            Database = "dlrc3ukbga13p", // e.g. ksdjfhsafnfas
            Username = "ajvldhwnkguvba", // e.g. lksfhdslkfsdflk
            Password = "18d9d325b17c74af0507a2af68f3f5f454f5ad39361a76d0d922758431c8990c",// e.g KJZDldfj34567dslkfjsdlfksdjflsdkfjsdlkfjsdl34567fkjdsgDRTYUI
            Port = 5432, // e.g 5432
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