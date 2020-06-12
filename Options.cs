using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HttpProxyServer
{
    public class Options
    {
        [Option('e', "endpoints", Separator = ';', HelpText = "监听IP和端口的组合，每个组合间用“;”分隔，IP和端口间用“:”分隔。")]
        public IEnumerable<string> Endpoints { get; set; } = null;

        [Option('u', "user", HelpText = "用户名。")]
        public string User { get; set; } = null;

        [Option('p', "password", HelpText = "密码。")]
        public string Password { get; set; } = null;

        public IEnumerable<IPEndPoint> GetIPEndPoints()
        {
            List<IPEndPoint> eps = new List<IPEndPoint>();
            foreach (var i in Endpoints)
            {
                try
                {
                    var words = i.Split(':');
                    var ep = new IPEndPoint(IPAddress.Parse(words[0]), int.Parse(words[1]));
                    eps.Add(ep);
                }
                catch
                {
                    this.Print($"无法解析Endpoint：{ i }");
                }
            }

            if (eps.Count == 0)
            {
                this.Print($"没有任何合法的监听IP和端口的组合，退出。");
                Environment.Exit(-1);
            }

            return eps;
        }

        public void InitFromEnvironment()
        {
            if (Endpoints == null || Endpoints.Count() == 0)
            {
                string eps = Environment.GetEnvironmentVariable("ENDPOINTS");
                if (string.IsNullOrWhiteSpace(eps) == false)
                {
                    Endpoints = eps.Split(";");
                }
            }

            if (string.IsNullOrWhiteSpace(User))
            {
                User = Environment.GetEnvironmentVariable("USER");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                Password = Environment.GetEnvironmentVariable("Password");
            }
        }
    }
}
