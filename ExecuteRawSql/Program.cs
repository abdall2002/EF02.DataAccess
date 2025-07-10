using System.Data;
using System.Net.Http.Headers;
using ExecuteRawSql;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
//Console.WriteLine(configuration.GetSection("constr").Value);

SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);

var sql = "SELECT * FROM WALLETS";

SqlCommand command = new SqlCommand(sql, conn);

command.CommandType = CommandType.Text;

conn.Open();

SqlDataReader reader = command.ExecuteReader();

Wallet wallet;

while (reader.Read())
{
    wallet = new Wallet
    {
        Id = reader.GetInt32("Id"),
        Holder = reader.GetString("Holder"),
        Balance = reader.GetDecimal("Balance"),
    };

    Console.WriteLine(wallet);
}

conn.Close();
