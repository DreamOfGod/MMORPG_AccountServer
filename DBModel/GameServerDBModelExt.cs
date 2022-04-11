using Mmcoy.Framework;
using System.Collections.Generic;

public partial class GameServerDBModel
{
    public List<RetGameServerPageEntity> GetGameServerPageList()
    {
        List<RetGameServerPageEntity> gameServerPageList = new List<RetGameServerPageEntity>();

        List<GameServerEntity> gameServerList = GetList(isDesc: false);
        //10个一组作为一个页签
        int i = 0, end = gameServerList.Count - 9;
        while(i < end)
        {
            RetGameServerPageEntity retGameServerPageEntity = new RetGameServerPageEntity();
            retGameServerPageEntity.PageIndex = i / 10 + 1;
            retGameServerPageEntity.BeginId = (int)gameServerList[i].Id;
            retGameServerPageEntity.EndId = (int)gameServerList[i + 9].Id;
            gameServerPageList.Add(retGameServerPageEntity);
            i += 10;
        }
        if(i < gameServerList.Count)
        {
            RetGameServerPageEntity retGameServerPageEntity = new RetGameServerPageEntity();
            retGameServerPageEntity.PageIndex = i / 10 + 1;
            retGameServerPageEntity.BeginId = (int)gameServerList[i].Id;
            retGameServerPageEntity.EndId = (int)gameServerList[gameServerList.Count - 1].Id;
            gameServerPageList.Add(retGameServerPageEntity);
        }

        return gameServerPageList;
    }

    public List<RetGameServerEntity> GetGameServerList(int pageIndex)
    {
        MFReturnValue<List<GameServerEntity>> retPageList = GetPageList(pageSize: 10, pageIndex: pageIndex, isDesc: false);
        if(retPageList.HasError)
        {
            return null;
        }
        List<GameServerEntity> gameServerList = retPageList.Value;
        List<RetGameServerEntity> retGameServerEntityList = new List<RetGameServerEntity>();
        for (int i = 0; i < gameServerList.Count; ++i)
        {
            GameServerEntity entity = gameServerList[i];
            RetGameServerEntity retEntity = new RetGameServerEntity();
            retEntity.Id = entity.Id.Value;
            retEntity.RunStatus = entity.RunStatus;
            retEntity.IsCommand = entity.IsCommand;
            retEntity.IsNew = entity.IsNew;
            retEntity.Name = entity.Name;
            retEntity.Ip = entity.Ip;
            retEntity.Port = entity.Port;

            retGameServerEntityList.Add(retEntity);
        }

        return retGameServerEntityList;
    }
}