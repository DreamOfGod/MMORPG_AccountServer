using System.Collections.Generic;

public partial class GameServerCacheModel
{
    public List<RetGameServerPageEntity> GetGameServerPageList()
    {
        return DBModel.GetGameServerPageList();
    }

    public List<RetGameServerEntity> GetGameServerList(int pageIndex)
    {
        return DBModel.GetGameServerList(pageIndex);
    }
}