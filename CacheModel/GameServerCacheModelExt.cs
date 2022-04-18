using MMORPG_AccountServer.Bean.GameServer;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class GameServerCacheModel
{
    public async Task<List<GameServerGroupBean>> GetGameServerGroupList()
    {
        return await DBModel.GetGameServerGroupList();
    }

    public async Task<List<GameServerBean>> GetGameServerList(int firstId, int lastId)
    {
        return await DBModel.GetGameServerList(firstId, lastId);
    }

    public async Task<List<GameServerBean>> GetRecommendGameServerList()
    {
        return await DBModel.GetRecommendGameServerList();
    }

    public async Task<bool> EnterGameServer(int accountId, int gameServerId)
    {
        return await DBModel.EnterGameServer(accountId, gameServerId);
    }
}