using System;
using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using Google.Protobuf;
using System.IO;
using System.Linq;
using System.Threading;

class NetworkManager : MonoBehaviour {

    private bool _isConnect = false;
    private Socket _socket;
    private byte[] _receiveMsg = new byte[256];

    public SeverEventHandler SeverEventHandler;

    public void SendMsg(int msgId,  IMessage msg)
    {
        if (_socket == null)
            this.ConnectToServer();
        MemoryStream output = new MemoryStream();
        byte[] idBytes = BitConverter.GetBytes(msgId);
        //id占4个字节
        output.Write(idBytes, 0, 4);
        msg.WriteTo(output);
        output.Position = 0;
        byte[] bytes = new byte[output.Length];
        output.Read(bytes, 0, bytes.Length);
        _socket.Send(bytes);

        Debug.LogError("向服务器发送信息,id为：" + msgId);
    }

    private void Start()
    {
        ConnectToServer();
    }

    private void ConnectToServer()
    {
        if (!_isConnect)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = Appconfig.appWebSocket_PORT;
            _socket.BeginConnect(Appconfig.appWebSocket_IP, port, ConnectResult, _socket);
        }
    }

    private void ConnectResult(IAsyncResult ar)
    {
        _isConnect = ar.IsCompleted;
        if (_isConnect)
        {
            Debug.LogError("socket 连接成功！");
            Thread newThread = new Thread(ReceiveMsg);
            newThread.Start();
        }
        else
            Debug.LogError("socket 连接失败！");
    }

    private void ReceiveMsg()
    {
        while (true)
        {
            byte[] receive = new byte[1024];
            int length = 0;
            try
            {
                length = _socket.Receive(receive);
            }
            catch (Exception e)
            {
                if (!_socket.Connected)
                    break;
            }

            byte[] receiveMsg = receive.Take(length).ToArray();


            //前4个字节为消息id
            int msgId = BitConverter.ToInt32(receiveMsg.Take(4).ToArray(), 0);
            Debug.LogError("接收到服务器反馈消息, id为：" + msgId);
            byte[] msgBody = receiveMsg.Skip(4).Take(receiveMsg.Length - 4).ToArray();

            SeverEventHandler.OnReceiveMsgFromServer(msgId, msgBody);
        }
    }
}
