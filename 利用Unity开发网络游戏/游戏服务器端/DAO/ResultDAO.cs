using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    class ResultDAO
    {
        public Result GetResultByUserid(MySqlConnection conn, int userid)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from result where userid = @userid ", conn);
                cmd.Parameters.AddWithValue("userid", userid);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
               
                    int winCount = reader.GetInt32("wincount");
                    int totalCount = reader.GetInt32("totalcount");
                   Result result = new Result(id,userid,totalCount,winCount);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userid, 0, 0);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在GetResultByUserid的时候出现异常：" + e);
            }
            finally
            {
                if (reader != null) reader.Close();
            }
            return null;
        }

        public void UpdateOrAddResult(Result res, MySqlConnection conn)
        {
            try
            {
                MySqlCommand cmd = null;

                if (res.Id <= -1)
                {
                    cmd = new MySqlCommand("insert into result set totalcount=@totalcount,wincount=@wincount,userid=@userid", conn);
                }
                else
                {
                    cmd = new MySqlCommand("update result set totalcount=@totalcount,wincount=@wincount where userid=@userid ", conn);
                }
                cmd.Parameters.AddWithValue("totalcount", res.TotalCount);
                cmd.Parameters.AddWithValue("wincount", res.WinCount);
                cmd.Parameters.AddWithValue("userid", res.UserId);
                cmd.ExecuteNonQuery();
                if (res.Id <= -1)
                {
                    Result tempRes = GetResultByUserid(conn, res.UserId);
                    res.Id = tempRes.Id;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("在UpdateOrAddResult的时候出现异常：" + e);
            }

        }
    }
}
