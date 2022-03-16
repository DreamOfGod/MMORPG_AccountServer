using MMORPG_AccountServer.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MMORPG_AccountServer.DBModel
{
    public class AccountDBModel
    {
        #region 单例
        private static object lock_object = new object();
        private static AccountDBModel instance;

        public static AccountDBModel Instance
        {
            get
            {
                if(instance == null)
                {
                    lock(lock_object)
                    {
                        if(instance == null)
                        {
                            instance = new AccountDBModel();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        private const string connStr = "server=127.0.0.1;port=3306;user=root;password=123456; database=MMORPG_Account;";

        public AccountEntity Get(int id)
        {
            //建立连接
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                //打开连接
                conn.Open();

                //建立执行对象
                MySqlCommand cmd = new MySqlCommand("Account_Get", conn);

                //使用存储过程
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("id", id));
                using (MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            AccountEntity entity = new AccountEntity();

                            entity.Id = reader["Id"] is DBNull ? 0 : Convert.ToInt32(reader["Id"]);
                            entity.Username = reader["Username"] is DBNull ? string.Empty : Convert.ToString(reader["Username"]);
                            entity.Pwd = reader["Pwd"] is DBNull ? string.Empty : Convert.ToString(reader["Pwd"]);
                            entity.YuanBao = reader["YuanBao"] is DBNull ? 0 : Convert.ToInt32(reader["YuanBao"]);
                            entity.LastServerId = reader["LastServerId"] is DBNull ? 0 : Convert.ToInt32(reader["LastServerId"]);
                            entity.LastServerName = reader["LastServerName"] is DBNull ? string.Empty : Convert.ToString(reader["LastServerName"]);
                            entity.CreateTime = reader["CreateTime"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["CreateTime"]);
                            entity.UpdateTime = reader["UpdateTime"] is DBNull ? DateTime.MinValue : Convert.ToDateTime(reader["UpdateTime"]);

                            return entity;
                        }
                    }
                }
            }

            return null;
        }
    }
}