using System;
using System.Net.Http;

namespace Victoria {
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions {
        internal static readonly Random Random = new Random();
        internal static readonly HttpClient HttpClient = new HttpClient();
    }
}