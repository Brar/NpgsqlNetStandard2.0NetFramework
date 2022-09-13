using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace NpgsqlNetStandard2_0NetFramework
{
    static class Program
    {
        static async Task Main()
        {
            var dsb = new NpgsqlDataSourceBuilder($"Host=localhost;Username={Environment.UserName};Integrated Security=true");
            using (var ds = dsb.Build())
            {
                using (var conn = await ds.OpenConnectionAsync())
                {
                    using (var cmd = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS data (id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY, value text NOT NULL)", conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                        cmd.CommandText = "INSERT INTO data (value) VALUES ('First row'), ('Second row'), ('Third row')";
                        await cmd.ExecuteNonQueryAsync();
                        cmd.CommandText = "SELECT value FROM data";
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader.GetString(0));
                            }
                        }
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
