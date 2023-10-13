using System.Net;

namespace Team.Extensions.Infrastrcture.Enums
{
    public enum ApiResultCode
    {
        Ok = HttpStatusCode.OK,
        Error = HttpStatusCode.BadRequest,
        NotFound = HttpStatusCode.NotFound
    }
}