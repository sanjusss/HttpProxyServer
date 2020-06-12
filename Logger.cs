using System;
using System.Collections.Generic;
using System.Text;

namespace HttpProxyServer
{
    public static class Logger
    {
        public static void Print(this object o, string msg)
        {
            Console.WriteLine($"[{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }][{ o.GetType().Name }]{ msg }");
        }
    }
}
