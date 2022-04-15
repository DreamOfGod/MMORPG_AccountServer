using Mmcoy.Framework;
using System.Collections.Generic;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("game_server")]
    public class GameServerController : ApiController
    {
        public ResponseData<List<RetGameServerPageEntity>> Get()
        {
            List<RetGameServerPageEntity> list = GameServerCacheModel.Instance.GetGameServerPageList();
            var responseData = new ResponseData<List<RetGameServerPageEntity>>(0, list, null);
            return responseData;
        }

        public ResponseData<List<RetGameServerEntity>> Get(int pageIndex)
        {
            var responseData = new ResponseData<List<RetGameServerEntity>>();
            List<RetGameServerEntity> retGameServerList = GameServerCacheModel.Instance.GetGameServerList(pageIndex);
            if(retGameServerList == null)
            {
                responseData.Code = 1;
            }
            else
            {
                responseData.Code = 0;
                responseData.Data = retGameServerList;
            }
            return responseData;
        }
    }
}