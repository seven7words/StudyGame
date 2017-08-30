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

            #region 插入

            //string username = "cwer";
            //string password = "lcker;delete from user";
            ////MySqlCommand cmd = new MySqlCommand("insert into user set username ='" + username + "'" + ",password='" +
            ////password+"'",conn);
            ////可以屏蔽sql注入
            //MySqlCommand cmd = new MySqlCommand("insert into user set username=@un,password=@pwd", conn);
            //cmd.Parameters.AddWithValue("un", username);
            //cmd.Parameters.AddWithValue("pwd", password);
            //cmd.ExecuteNonQuery();

            #endregion

            #region 删除

            //MySqlCommand cmd = new MySqlCommand("delete from user where iduser = @iduser", conn);
            //cmd.Parameters.AddWithValue("iduser", 18);
            //cmd.ExecuteNonQuery();

            #endregion
            #region 更新
            //MySqlCommand cmd = new MySqlCommand("update user set password = @pwd where iduser =11",conn);
            //cmd.Parameters.AddWithValue("pwd", "sevenbbbb");
            //cmd.ExecuteNonQuery();
#endregion
            conn.Close();
            Console.ReadKey();
        }
    }
}
