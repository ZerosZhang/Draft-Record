using MySql.Data.MySqlClient;
using System;

namespace CreateDatabaseIfNotExist
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置连接字符串（注意这里不包含Database参数，因为我们可能需要创建一个新的数据库）
            string connectionString = "Server=localhost;Uid=myUsername;Pwd=myPassword;";

            // 尝试连接到数据库
            bool databaseCreated = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the server successfully.");

                    // 检查数据库是否存在
                    string databaseName = "myDatabase";
                    string query = $"CREATE DATABASE IF NOT EXISTS {databaseName}"; 
                    MySqlCommand command = new MySqlCommand(query, connection); 
                    command.ExecuteNonQuery(); 
                    Console.WriteLine($"The database '{databaseName}' has been created and is now accessible.");
                    databaseCreated = true;
                }
            }
            catch (MySqlException ex)
            {
                // 捕获到异常，说明连接失败
                Console.WriteLine($"Error: {ex.Message}"); if (ex.Number == 1049 && !databaseCreated)
                {
                    Console.WriteLine("The database does not exist. Attempting to create it...");                    // 再次尝试连接，这次为了创建数据库                    try                    {                        using (MySqlConnection connection = new MySqlConnection(connectionString))                        {                            connection.Open();                            Console.WriteLine("Connected to the server successfully.");                            // 创建数据库                            string createDatabaseQuery = $"CREATE DATABASE {databaseName}";
                    MySqlCommand createCommand = new MySqlCommand(createDatabaseQuery, connection);
                    createCommand.ExecuteNonQuery();

                    Console.WriteLine($"The database '{databaseName}' has been created."); databaseCreated = true;
                }
            }
            catch (Exception) { Console.WriteLine("Failed to create the database. Check your permissions and try again."); throw; }
        }
    }            catch (Exception ex)            {                // 捕获其他类型的异常                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}