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
    Holder = "Abdallah",
    Balance = 5000,
};


SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);

var sql = $"INSERT INTO WALLETS (Holder, Balance) VALUES (@Holder, @Balance)";

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

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine($"wallet for {walletToInsert.Holder} added successully");
}
else
{
    Console.WriteLine($"ERROR: wallet for {walletToInsert.Holder} was not added");
}

conn.Close();
