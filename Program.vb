Imports Npgsql

Module Program

    Sub Main()
        Dim dsb = New NpgsqlDataSourceBuilder($"Host=localhost;Username={Environment.UserName};Integrated Security=true")
        Using ds = dsb.Build()
            Using conn = ds.OpenConnection()
                Using cmd = New NpgsqlCommand("CREATE TABLE IF NOT EXISTS data (id integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY, value text NOT NULL)", conn)
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "INSERT INTO data (value) VALUES ('First row'), ('Second row'), ('Third row')"
                    cmd.ExecuteNonQuery()
                    cmd.CommandText = "SELECT value FROM data"
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            Console.WriteLine(reader.GetString(0))
                        End While
                    End Using
                End Using
            End Using
        End Using
        Console.ReadLine()
    End Sub

End Module
