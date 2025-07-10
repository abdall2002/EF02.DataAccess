using System.Data;
using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
//Console.WriteLine(configuration.GetSection("constr").Value);

SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);

conn.Open();

var sql = "SELECT * FROM WALLETS";

SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);

DataTable dt = new DataTable();

adapter.Fill(dt);

conn.Close();

foreach (DataRow dr in dt.Rows)
{
    var wallet = new Wallet
    {
        Id = Convert.ToInt32(dr["Id"]),
        Holder = Convert.ToString(dr["Holder"]),
        Balance = Convert.ToDecimal(dr["Balance"])
    };
    Console.WriteLine(wallet);
}