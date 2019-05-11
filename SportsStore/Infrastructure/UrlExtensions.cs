using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Web;

namespace SportsStore.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            return request.QueryString.HasValue ? $"{HttpUtility.UrlDecode(UrlFix(request.QueryString.ToString()))}" : request.Path.ToString();
        }
        static string UrlFix(string _str)
        {
            string Buffer = string.Empty;
            for (int i = _str.Length - 1; i >= 0; --i)
            {
                if (_str[i] == '=')
                    break;
                Buffer += _str[i];
            }
            char[] charArray = Buffer.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
