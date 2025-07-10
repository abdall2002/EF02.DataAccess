using System.Data;
using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

// read from user input
var walletToInsert = new Wallet
{
    Holder = "ALi",
    Balance = 7000,
};


SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);

var sql = "INSERT INTO WALLETS (Holder, Balance) VALUES " +
                $"(@Holder, @Balance);" +
                $"SELECT CAST(scope_identity() AS int)";

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Balance,
};

SqlParameter balanceParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.VarChar,
    Direction = ParameterDirection.Input,
    Value = walletToInsert.Holder,
};


SqlCommand command = new SqlCommand(sql, conn);

command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);

command.CommandType = CommandType.Text;

conn.Open();

walletToInsert.Id = (int)command.ExecuteScalar();

Console.WriteLine($"wallet {walletToInsert} added successully");

conn.Close();
