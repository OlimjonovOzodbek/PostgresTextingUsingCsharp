using Npgsql;
using System.Security.Cryptography;
using System.Text;
namespace ConsoleApp3
{
    public class Message
    {
        private readonly string connectionString = "Server=127.0.0.1;Port=5432;Database=MyData;User Id=postgres;" +
            "Password=admin;";
        public string TableName = "Data1";
        public void GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from {TableName};";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]}\n{reader["password"]}\n");
                }
            }
        }
        public string username;
        string password;
        public void Insertion()
        {
            check:
            
            Console.Write("Username -> ");
            username = Console.ReadLine();
            Console.Write("Password -> ");
            password = Console.ReadLine();
            var mypas = HashPasword(password, out var salt);
            Console.WriteLine(mypas);
            Console.WriteLine($"salt: {Convert.ToHexString(salt)}");
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"insert into Data1(name, password ,salt) values('{username}', '{mypas}','{Convert.ToHexString(salt)}');";
                    NpgsqlCommand command = new NpgsqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex) 
            {
                Console.Clear();
                Console.WriteLine("USERNAME UJE MAVJUD AKA\n");
            goto check;
            }
        }
        public void Insertion2()
        {
            string message;
            Console.Write("Message -> ");
            message = Console.ReadLine();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = $"insert into messages(name,msg) values('{username}', '{message}');";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
            Console.WriteLine("Yozildi");
        }

        public const int keySize = 64;
        public const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            hashAlgorithm,
            keySize);

            return Convert.ToHexString(hash);
        }
       
        public void GetMsg()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = $"select * from messages;";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["name"]} -> {reader["msg"]}\n");
                }
            }
        }
        
    }
}
