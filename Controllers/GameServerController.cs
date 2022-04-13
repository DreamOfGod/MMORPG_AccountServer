using Mmcoy.Framework;
using System.Collections.Generic;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("game_server")]
    public class GameServerController : ApiController
    {
        public ResponseValue<List<RetGameServerPageEntity>> Get()
        {
            List<RetGameServerPageEntity> list = GameServerCacheModel.Instance.GetGameServerPageList();
            var responseValue = new ResponseValue<List<RetGameServerPageEntity>>(0, list, null);
            return responseValue;
        }

        public ResponseValue<List<RetGameServerEntity>> Get(int pageIndex)
        {
            var responseValue = new ResponseValue<List<RetGameServerEntity>>();
            List<RetGameServerEntity> retGameServerList = GameServerCacheModel.Instance.GetGameServerList(pageIndex);
            if(retGameServerList == null)
            {
                responseValue.Code = 1;
            }
            else
            {
                responseValue.Code = 0;
                responseValue.Value = retGameServerList;
            }
            return responseValue;
        }
    }
}