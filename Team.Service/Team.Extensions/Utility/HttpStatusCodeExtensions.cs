﻿using System.Net;

namespace Team.Extensions.Utility
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccess(this HttpStatusCode code)
        {
            return code >= HttpStatusCode.OK && code < HttpStatusCode.Ambiguous;
        }
    }
}