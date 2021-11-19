using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace akatsuki_lt_server
{
    public class Server
    {
        public static void Main(string[] args)
        {
            //エンドポイントの設定
            var endpooint = new IPEndPoint(IPAddress.Any, 19564);
            //サーバーの作成
            var server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            int bufferSize = 1024;
            
            //エンドポイントの設定
            server.Bind(endpooint);
            
            server.Listen(10);
            Console.WriteLine("接続待ち");

            while (true)
            {
                Socket handler = server.Accept();
                Console.WriteLine("接続完了");
                var bytes = new byte[bufferSize];
                while (true)
                {
                    var size = handler.Receive(bytes);
                    if (size ==0)
                    {
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        Console.WriteLine("切断");
                        break;
                    }

                    var str = Encoding.UTF8.GetString(bytes, 0, size);
                    Console.WriteLine(str);
                    str = str.ToUpper();
                    handler.Send(Encoding.UTF8.GetBytes(str));
                }
            }
        }
    }
}