using System.Data;
using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();


SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);

var sql = $"UPDATE Wallets SET Holder = @Holder, Balance = @Balance " +
    $"WHERE Id = @Id";

SqlParameter idParameter = new SqlParameter
{
    ParameterName = "@Id",
    SqlDbType = SqlDbType.Int,
    Direction = ParameterDirection.Input,
    Value = 1,
};

SqlParameter holderParameter = new SqlParameter
{
    ParameterName = "@Holder",
    SqlDbType = SqlDbType.VarChar,
    Direction = ParameterDirection.Input,
    Value = "Ahmed",
};

SqlParameter balanceParameter = new SqlParameter
{
    ParameterName = "@Balance",
    SqlDbType = SqlDbType.Decimal,
    Direction = ParameterDirection.Input,
    Value = 6000,
};


SqlCommand command = new SqlCommand(sql, conn);

command.Parameters.Add(idParameter);
command.Parameters.Add(holderParameter);
command.Parameters.Add(balanceParameter);

command.CommandType = CommandType.Text;

conn.Open();

if (command.ExecuteNonQuery() > 0)
{
    Console.WriteLine($"wallet for Updated successully");
}

conn.Close();
