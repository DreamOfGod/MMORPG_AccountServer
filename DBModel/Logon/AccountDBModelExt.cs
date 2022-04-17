using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

public partial class AccountDBModel
{
    public async Task<int> Register(string username, string pwd, short channelId, string deviceIdentifier, string deviceModel)
    {
        using (var conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            await conn.OpenAsync();
            var trans = conn.BeginTransaction();
            var sql = $"select count(*) from Account where Username = '{ username }'";
            var queryCountCommond = new SqlCommand(sql, conn, trans);
            var count = (int)await queryCountCommond.ExecuteScalarAsync();
            if (count > 0)
            {
                //用户名重复
                trans.Commit();
                return -1;
            }

            sql =
$@"insert into Account
(Status, Username, Pwd, ChannelId, LastLogonServerTime, CreateTime, UpdateTime, DeviceIdentifier, DeviceModel)
values
({ ((byte)EnumEntityStatus.Released) }, '{ username }', '{ MFEncryptUtil.Md5(pwd) }', { channelId }, '{ DateTime.Now }', '{ DateTime.Now }', '{ DateTime.Now }', '{ deviceIdentifier }', '{ deviceModel }');
select scope_identity()";

            var insertCommand = new SqlCommand(sql, conn, trans);
            var id = int.Parse((await insertCommand.ExecuteScalarAsync()).ToString());
            if(id >= 0)
            {
                //注册成功
                trans.Commit();
                return id;
            }
            else
            {
                //注册失败
                trans.Rollback();
                return -2;
            }
        }
    }

    public async Task<int> Logon(string username, string pwd, string deviceIdentifier, string deviceModel)
    {
        using(var conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            await conn.OpenAsync();
            string sql = $@"select Id from Account where Status = { ((byte)EnumEntityStatus.Released) } and Username = '{  username }' and Pwd = '{ MFEncryptUtil.Md5(pwd) }'";
            var selectCommand = new SqlCommand(sql, conn);
            var result = await selectCommand.ExecuteScalarAsync();
            if(result == null)
            {
                //用户名或密码错误
                return -1;
            }
            else
            {
                //登录成功
                int id = int.Parse(result.ToString());
                sql = $@"update account set DeviceIdentifier = '{ deviceIdentifier }', DeviceModel = '{ deviceModel }' where Id = { id }";
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();
                return id;
            }
        }
    }
}