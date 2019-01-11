using System;
using System.Net.Sockets;
using Google.Protobuf;
using System.IO;

namespace MyPokerGameServer
{
    class NetworkManager : Singleton<NetworkManager>
    {

        private bool _isConnect = false;
        private byte[] _receiveMsg = new byte[256];

        public void SendMsg(Socket socket, int msgId, IMessage msg)
        {
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
    }
}
