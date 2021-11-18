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
            var endpoint = new IPEndPoint(IPAddress.Any, 19564);
            //サーバーソケットの作成
            var server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int bufferSize = 1024;
            
            //エンドポイントを関連づける
            server.Bind(endpoint);
            //接続待ち状態にする
            //引数は接続待ちの最大数
            server.Listen(10);
            Console.WriteLine("Starting server...");
            
            while (true)
            {
                //接続要求があったら受け付ける
                Socket handler = server.Accept();
                Console.WriteLine("Connection");
                var bytes = new byte[bufferSize];
                while (true)
                {
                    //データを受け取る
                    var size = handler.Receive(bytes);
                    var str = Encoding.UTF8.GetString(bytes, 0, size);
                    //データがなかったら接続を閉じる
                    if (size == 0)
                    {
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                        Console.WriteLine("Connection Close");
                        break;
                    }
                    //データを表示
                    Console.WriteLine(str);
                    //大文字に変換
                    str = str.ToUpper();
                    //データを送信する
                    handler.Send(Encoding.UTF8.GetBytes(str));
                }
                
            }
        }
    }
}