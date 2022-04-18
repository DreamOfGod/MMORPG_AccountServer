using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;
using MMORPG_AccountServer.Bean.GameServer;
using MMORPG_AccountServer.Bean.Logon;
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
(Status, Username, Pwd, Money, ChannelId, CreateTime, UpdateTime, DeviceIdentifier, DeviceModel)
values
({ ((byte)EnumEntityStatus.Released) }, '{ username }', '{ MFEncryptUtil.Md5(pwd) }', 0, { channelId }, '{ DateTime.Now }', '{ DateTime.Now }', '{ deviceIdentifier }', '{ deviceModel }');
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

    public async Task<AccountBean> Logon(string username, string pwd, string deviceIdentifier, string deviceModel)
    {
        using(var conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            await conn.OpenAsync();
            string sql =
$@"select Account.Id, Mobile, Mail, Money, ChannelId, GameServer.Id, RunStatus, IsCommand, IsNew, Name, Ip, Port, LastLogonServerTime, LastLogonRoleId
    from Account left join GameServer on Account.LastLogonServerId = GameServer.Id
    where Account.Status = { ((byte)EnumEntityStatus.Released) } and Username = '{  username }' and Pwd = '{ MFEncryptUtil.Md5(pwd) }'";
            var selectCommand = new SqlCommand(sql, conn);
            var reader = await selectCommand.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                //登录成功
                AccountBean account = new AccountBean();
                account.Id = reader.GetInt32(0);
                account.Mobile = reader[1] is DBNull ? null : reader.GetString(1);
                account.Mail = reader[2] is DBNull ? null : reader.GetString(2);
                account.Money = reader.GetInt32(3);
                account.ChannelId = reader.GetInt16(4);
                if (!(reader[5] is DBNull))
                {
                    //最后登陆的区服
                    GameServerBean lastLogonGameServer = new GameServerBean();
                    lastLogonGameServer.Id = reader.GetInt32(5);
                    lastLogonGameServer.RunStatus = reader.GetByte(6);
                    lastLogonGameServer.IsCommand = reader.GetBoolean(7);
                    lastLogonGameServer.IsNew = reader.GetBoolean(8);
                    lastLogonGameServer.Name = reader.GetString(9);
                    lastLogonGameServer.Ip = reader.GetString(10);
                    lastLogonGameServer.Port = reader.GetInt32(11);
                    account.LastLogonGameServer = lastLogonGameServer;
                }

                account.LastLogonServerTime = reader[12] is DBNull ? (DateTime?)null : reader.GetDateTime(12);
                account.LastLogonRoleId = reader[13] is DBNull ? (int?)null : reader.GetInt32(13);
                account.DeviceIdentifier = deviceIdentifier;
                account.DeviceModel = deviceModel;

                reader.Close();
                sql = $@"update account set DeviceIdentifier = '{ deviceIdentifier }', DeviceModel = '{ deviceModel }' where Id = { account.Id }";
                var updateCommand = new SqlCommand(sql, conn);
                updateCommand.ExecuteNonQuery();

                return account;
            }
            else
            {
                return null;
            }
        }
    }
}