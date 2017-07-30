using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace mysql数据库操作
{
    class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Database=test007;Data Source=127.0.0.1;port=3306;User Id=root;Password=19950728;";
           MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            #region 查询
            //MySqlCommand cmd = new MySqlCommand("Select * from user", conn);
            //MySqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    reader.Read();
            //    string username = reader.GetString("username");
            //    string password = reader.GetString("password");
            //    Console.WriteLine(username + ":" + password);
            //    reader.Close();
            //}


            #endregion

            string username = "cwer";
            string password = "lcker";
            MySqlCommand cmd = new MySqlCommand("insert into user set username ='" + username + "'" + ",password='" +
                                            password+"'",conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Console.ReadKey();
        }
    }
}
