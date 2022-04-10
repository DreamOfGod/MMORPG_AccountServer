using Mmcoy.Framework;
using System.Web.Http;

namespace MMORPG_AccountServer.Controllers.api
{
    public class TimeController : ApiController
    {
        public long Get()
        {
            return MFDSAUtil.GetTimestamp();
        }
    }
}