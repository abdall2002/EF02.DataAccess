using Microsoft.Extensions.Configuration;

var congfiguration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Console.WriteLine(congfiguration.GetSection("constr").Value);

