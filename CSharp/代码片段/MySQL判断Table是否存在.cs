using MySql.Data.MySqlClient;
using System;

public class DatabaseHelper
{
    public static bool TableExists(string connectionString, string tableName)
    {
        bool tableExists = false;
        string query = $"SHOW TABLES LIKE '{tableName}';";
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        tableExists = reader.HasRows;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        return tableExists;
    }
}

// 使用示例
// 确保你的数据库连接字符串是正确的
string connectionString = "server=localhost;database=your_database;uid=your_username;pwd=your_password";
string tableName = "your_table_name";

if (DatabaseHelper.TableExists(connectionString, tableName))
{
    Console.WriteLine($"Table {tableName} exists in the database.");
}
else
{
    Console.WriteLine($"Table {tableName} does not exist in the database.");
}