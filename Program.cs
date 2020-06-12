using CommandLine;
using System;

namespace HttpProxyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(server.Exec);
        }
    }
}
