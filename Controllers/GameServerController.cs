using Mmcoy.Framework;
using System.Collections.Generic;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    [Route("game_server")]
    public class GameServerController : ApiController
    {
        public enum GameServerPageCodeType
        {
            Success,

        }

        public ResponseValue<GameServerPageCodeType, List<RetGameServerPageEntity>> Get()
        {
            var responseValue = new ResponseValue<GameServerPageCodeType, List<RetGameServerPageEntity>>();
            responseValue.Code = GameServerPageCodeType.Success;
            responseValue.Value = GameServerCacheModel.Instance.GetGameServerPageList();
            return responseValue;
        }

        public enum GameServerCodeType
        {
            Success,
            WrongPageIndex
        }

        public ResponseValue<GameServerCodeType, List<RetGameServerEntity>> Get(int pageIndex)
        {
            var responseValue = new ResponseValue<GameServerCodeType, List<RetGameServerEntity>>();
            List<RetGameServerEntity> retGameServerList = GameServerCacheModel.Instance.GetGameServerList(pageIndex);
            if(retGameServerList == null)
            {
                responseValue.Code = GameServerCodeType.WrongPageIndex;
            }
            else
            {
                responseValue.Code = GameServerCodeType.Success;
                responseValue.Value = retGameServerList;
            }
            return responseValue;
        }
    }
}