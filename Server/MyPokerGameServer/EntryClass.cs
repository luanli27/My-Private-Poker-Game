using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace MyPokerGameServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Net;
    using System.Threading;

    namespace Server
    {
        class EntryClass
        {
            static Socket ReceiveSocket;
            private static List<Socket> socketList = new List<Socket>();
            static void Main(string[] args)
            {
                int port = 2700;
                IPAddress ip = IPAddress.Any;  // 侦听所有网络客户接口的客活动
                ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ReceiveSocket.Bind(new IPEndPoint(ip, port)); //绑定IP地址和端口号
                ReceiveSocket.Listen(10);  //设定最多有10个排队连接请求
                Console.WriteLine("开始侦听"+port+"端口");
                while (true)
                {
                    Socket socket = ReceiveSocket.Accept();
                    socketList.Add(socket);
                    Thread newThread = new Thread(OnSocketConnect);
                    newThread.Start();
                }
            }

            static void OnSocketConnect()
            {
                Socket socket = socketList.ElementAt(socketList.Count - 1);
                DateTime lastReceiveTime = DateTime.Now;
                Thread thread = Thread.CurrentThread;
                //暂时停止心跳包检测
//                Thread newThread = new Thread(() =>
//                {
//                    while (true)
//                    {
//                        double timeInterval = (DateTime.Now - lastReceiveTime).TotalSeconds;
//                        if (timeInterval > 5)
//                        {
//                            socket.Shutdown(SocketShutdown.Both);
//                            socket.Close();
//                            socketList.Remove(socket);
//                            try
//                            {
//                                Thread.CurrentThread.Abort();
//                                thread.Abort();
//                            }
//                            catch (Exception e)
//                            {
//                                break;
//                            }
//                        }
//
//                        Thread.Sleep(1000);
//                    }
//                });
//                newThread.Start();
                while (true)
                {
                    byte[] receive = new byte[1024];
                    int length = 0;
                    try
                    {
                        length = socket.Receive(receive);
                    }
                    catch (Exception e)
                    {
                        if(!socket.Connected)
                            break;
                    }
                    lastReceiveTime = DateTime.Now;
                    //前4个字节为消息id
                    int msgId = BitConverter.ToInt32(receive.Take(4).ToArray(), 0);
                    ReqLogin reqLogin = ReqLogin.Parser.ParseFrom(receive, 4, length - 4);
                    Console.WriteLine("接收到消息, id为：" + msgId + "  登陆账号为: " + reqLogin.UserName);

                    //domo版,取消中间流程,直接进入游戏(随便建个房间)
                    Singleton<RoomManager>.Instance.EnterRoom(100027, new Player(socket, reqLogin.UserName, 6666));
                }
            }
        }
    }
}
