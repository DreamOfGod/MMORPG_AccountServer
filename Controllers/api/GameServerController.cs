using Mmcoy.Framework;
using System.Collections.Generic;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class GameServerController : ApiController
    {
        public MFReturnValue<List<RetGameServerPageEntity>> Get()
        {
            MFReturnValue<List<RetGameServerPageEntity>> ret = new MFReturnValue<List<RetGameServerPageEntity>>();
            ret.Value = GameServerCacheModel.Instance.GetGameServerPageList();
            return ret;
        }

        public MFReturnValue<List<RetGameServerEntity>> Get(int pageIndex)
        {
            MFReturnValue<List<RetGameServerEntity>> ret = new MFReturnValue<List<RetGameServerEntity>>();
            List<RetGameServerEntity> retGameServerList = GameServerCacheModel.Instance.GetGameServerList(pageIndex);
            if(retGameServerList == null)
            {
                ret.HasError = true;
            }
            else
            {
                ret.Value = retGameServerList;
            }
            return ret;
        }
    }
}