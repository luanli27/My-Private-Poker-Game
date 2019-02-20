using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Google.Protobuf;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace MyPokerGameServer
{
    class NetworkManager : Singleton<NetworkManager>
    {
        private bool _isConnect = false;
        private byte[] _receiveMsg = new byte[256];
        static Socket ReceiveSocket;
        private static List<Socket> socketList = new List<Socket>();

        public void Start()
        {
            ListenToPort();
        }

        public void SendMsg(Socket socket, int msgId, IMessage msg)
        {
            if (socket == null)
            {
                Console.WriteLine("socket连接错误,退出消息发送");
                return;
            }
            MemoryStream output = new MemoryStream();
            byte[] idBytes = BitConverter.GetBytes(msgId);
            //id占4个字节
            output.Write(idBytes, 0, 4);
            msg.WriteTo(output);
            output.Position = 0;
            byte[] bytes = new byte[output.Length];
            output.Read(bytes, 0, bytes.Length);
            socket.Send(bytes);

            Console.WriteLine("向客户端发送信息,id为：" + msgId);
        }

        public void ReceiveMsg()
        {

        }

        private void ListenToPort()
        {
            int port = 2700;
            IPAddress ip = IPAddress.Any;  // 侦听所有网络客户接口的客活动
            ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ReceiveSocket.Bind(new IPEndPoint(ip, port)); //绑定IP地址和端口号
            ReceiveSocket.Listen(10);  //设定最多有10个排队连接请求
            Console.WriteLine("开始侦听" + port + "端口");
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
                    if (!socket.Connected)
                        break;
                }
                lastReceiveTime = DateTime.Now;
                //前4个字节为消息id
                int msgId = BitConverter.ToInt32(receive.Take(4).ToArray(), 0);
                Console.WriteLine("接收到消息, id为：" + msgId);
                byte[] msgBody = receive.Skip(4).Take(length - 4).ToArray();
                //如果是登陆,则处理账号信息初始化逻辑,否则执行相关网络事件的响应
                if (msgId == MessageDefine.C2G_REQ_LOGIN)
                {
                    Singleton<LoginManager>.Instance.OnReqLogin(msgBody, socket);
                }
                else
                {
                    Singleton<AccountConnectionManager>.Instance.SendMsgToGameRoom(socket, msgId, msgBody);
                }
            }
        }
    }
}
