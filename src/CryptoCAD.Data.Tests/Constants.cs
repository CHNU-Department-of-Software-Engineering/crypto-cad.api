using Microsoft.Extensions.Configuration;

namespace CryptoCAD.Data.Tests
{
    public static class Constants
    {
        public static string MongoDbConnectionUri => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build()
            .GetValue<string>("MongoUri");
    }
}