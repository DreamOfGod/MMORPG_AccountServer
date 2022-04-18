using MMORPG_AccountServer.Bean.GameServer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers
{
    public class GameServerController : ApiController
    {
        [HttpGet] [Route("game_server_group")]
        public async Task<ResponseData<List<GameServerGroupBean>>> GetGameServerGroup(string deviceIdentifier, string time, string sign)
        {
            var responseData = new ResponseData<List<GameServerGroupBean>>();
            if(!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseData.Code = 2;
                responseData.Error = "请求错误";
                return responseData;
            }
            try
            {
                responseData.Data = await GameServerCacheModel.Instance.GetGameServerGroupList();
                responseData.Code = 0;
            }
            catch (Exception ex)
            {
                responseData.Code = 1;
                responseData.Error = ex.Message;
            }
            return responseData;
        }

        [HttpGet] [Route("game_server_list")]
        public async Task<ResponseData<List<GameServerBean>>> GetGameServerList(int firstId, int lastId, string deviceIdentifier, string time, string sign)
        {
            var responseData = new ResponseData<List<GameServerBean>>();
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseData.Code = 3;
                responseData.Error = "请求错误";
                return responseData;
            }
            if (firstId < 0 || lastId < 0 || firstId > lastId)
            {
                responseData.Code = 1;
                responseData.Error = "参数错误";
                return responseData;
            }
            try
            {
                responseData.Data = await GameServerCacheModel.Instance.GetGameServerList(firstId, lastId);
                responseData.Code = 0;
            }
            catch (Exception ex)
            {
                responseData.Code = 2;
                responseData.Error = ex.Message;
            }
            return responseData;
        }

        [HttpGet] [Route("recommend_game_server_list")]
        public async Task<ResponseData<List<GameServerBean>>> GetRecommendGameServerList(string deviceIdentifier, string time, string sign)
        {
            var responseData = new ResponseData<List<GameServerBean>>();
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseData.Code = 2;
                responseData.Error = "请求错误";
                return responseData;
            }
            try
            {
                responseData.Data = await GameServerCacheModel.Instance.GetRecommendGameServerList();
                responseData.Code = 0;
            }
            catch (Exception ex)
            {
                responseData.Code = 1;
                responseData.Error = ex.Message;
            }
            return responseData;
        }

        [HttpGet] [Route("enter_game_server")]
        public async Task<ResponseData<object>> EnterGameServer(int gameServerId, string deviceIdentifier, string time, string sign)
        {
            var responseData = new ResponseData<object>();
            if (!ControllerHelper.CheckSign(deviceIdentifier, time, sign))
            {
                responseData.Code = 4;
                responseData.Error = "请求错误";
                return responseData;
            }
            object accountIDObj = HttpContext.Current.Session[SessionKey.AccountID];
            if (accountIDObj == null)
            {
                responseData.Code = 1;
                responseData.Error = "未登录";
                return responseData;
            }
            try
            {
                if(await GameServerCacheModel.Instance.EnterGameServer((int)accountIDObj, gameServerId))
                {
                    responseData.Code = 0;
                }
                else
                {
                    responseData.Code = 2;
                    responseData.Error = "未知错误";
                }
            }
            catch (Exception ex)
            {
                responseData.Code = 3;
                responseData.Error = ex.Message;
            }
            return responseData;
        }
    }
}