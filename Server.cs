using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.Models;

namespace HttpProxyServer
{
    public class Server
    {
        public void Exec(Options op)
        {
            op.InitFromEnvironment();
            while (true)
            {
                try
                {
                    var proxySever = new ProxyServer(false);
                    foreach (var ep in op.GetIPEndPoints())
                    {
                        proxySever.AddEndPoint(new ExplicitProxyEndPoint(ep.Address, ep.Port, false));
                        this.Print($"成功监听{ ep }。");
                    }

                    if (string.IsNullOrWhiteSpace(op.User) == false && string.IsNullOrWhiteSpace(op.Password) == false)
                    {
                        proxySever.ProxyBasicAuthenticateFunc += (e, user, pass) =>
                        {
                            return Task.FromResult(user == op.User && pass == op.Password);
                        };
                        this.Print("成功设置用户名密码。");
                    }

                    proxySever.Start();
                    this.Print("成功启动服务。");
                    Thread.Sleep(Timeout.InfiniteTimeSpan);
                }
                catch (Exception e)
                {
                    this.Print($"代理服务运行时出现异常：\n{ e }");
                }
            }
        }
    }
}
