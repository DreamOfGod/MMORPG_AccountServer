using MMORPG_AccountServer.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MMORPG_AccountServer.DBModel
{
    public class AccountDBModel
    {
        #region 单例
        private static object lock_object = new object();
        private static AccountDBModel instance;
        private AccountDBModel() { }

        public static AccountDBModel Instance
        {
            get
            {
                if(instance == null)
                {
                    //加锁，防止重复实例化
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

        private const string CONN_STR = "Data Source=.;Initial Catalog=MMORPG_Account;Integrated Security=True";

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountEntity Get(int id)
        {
            //建立连接
            using (SqlConnection conn = new SqlConnection(CONN_STR))
            {
                //打开连接
                conn.Open();

                //建立执行对象
                SqlCommand cmd = new SqlCommand("Account_Get", conn);

                //使用存储过程
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
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

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int Register(AccountEntity account)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(CONN_STR))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Account_Register", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Username", account.Username));
                    cmd.Parameters.Add(new SqlParameter("@Pwd", account.Pwd));
                    object result = cmd.ExecuteScalar();
                    if(result == null)
                    {
                        return -1;
                    }
                    else
                    {
                        return Convert.ToInt32(result.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                return -1;
            }
        }
    }
}