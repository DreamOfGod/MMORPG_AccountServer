using Mmcoy.Framework.AbstractBase;
using MMORPG_AccountServer.Bean.GameServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

public partial class GameServerDBModel
{
    public async Task<List<GameServerGroupBean>> GetGameServerGroupList()
    {
        var groups = new List<GameServerGroupBean>();
        var ids = new List<int>();
        using (var conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            await conn.OpenAsync();
            var sql = $"select Id from GameServer where Status = { (byte)EnumEntityStatus.Released } order by Id";
            var command = new SqlCommand(sql, conn);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    ids.Add(reader.GetInt32(0));
                }
            }
        }

        //10个一组
        int i = 0, end = ids.Count - 9;
        while (i < end)
        {
            var groupEntity = new GameServerGroupBean();
            groupEntity.FirstId = ids[i];
            groupEntity.LastId = ids[i + 9];
            groups.Add(groupEntity);
            i += 10;
        }
        if (i < groups.Count)
        {
            var groupEntity = new GameServerGroupBean();
            groupEntity.FirstId = ids[i];
            groupEntity.LastId = ids[ids.Count - 1];
            groups.Add(groupEntity);
        }

        return groups;
    }

    private async Task<List<GameServerBean>> GetGameServerList(string sql)
    {
        var gameServerList = new List<GameServerBean>();
        using (var conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            await conn.OpenAsync();
            SqlCommand command = new SqlCommand(sql, conn);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var gameServer = new GameServerBean();
                    gameServer.Id = reader.GetInt32(0);
                    gameServer.RunStatus = reader.GetByte(1);
                    gameServer.IsCommand = reader.GetBoolean(2);
                    gameServer.IsNew = reader.GetBoolean(3);
                    gameServer.Name = reader.GetString(4);
                    gameServer.Ip = reader.GetString(5);
                    gameServer.Port = reader.GetInt32(6);
                    gameServerList.Add(gameServer);
                }
            }
        }

        return gameServerList;
    }

    public async Task<List<GameServerBean>> GetGameServerList(int firstId, int lastId)
    {
        string sql = $"select Id, RunStatus, IsCommand, IsNew, Name, Ip, Port from GameServer where Status = { (byte)EnumEntityStatus.Released } and { firstId } <= Id and Id <= { lastId }";
        return await GetGameServerList(sql);
    }

    public async Task<List<GameServerBean>> GetRecommendGameServerList()
    {
        string sql = $"select top(3) Id, RunStatus, IsCommand, IsNew, Name, Ip, Port from GameServer where Status = { (byte)EnumEntityStatus.Released } order by Id desc";
        return await GetGameServerList(sql);
    }

    public async Task<bool> EnterGameServer(int accountId, int gameServerId)
    {
        //Account.LastLogonServerId具有与GameServer.Id的外键约束
        string sql = $"update Account set LastLogonServerId = { gameServerId }, UpdateTime = '{ DateTime.Now }' where Id = { accountId }";
        using(var conn = new SqlConnection(DBConn.MMORPG_Account))
        {
            await conn.OpenAsync();
            var command = new SqlCommand(sql, conn);
            int count = await command.ExecuteNonQueryAsync();//gameServerId在GameServer.Id列中不存在时将引发异常
            return count == 1;
        }
    }
}