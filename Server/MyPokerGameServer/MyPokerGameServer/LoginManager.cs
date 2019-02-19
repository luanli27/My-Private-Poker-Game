using System.Net.Sockets;
namespace MyPokerGameServer
{
    class LoginManager : Singleton<LoginManager>
    {
        public void OnReqLogin(byte[] msg, Socket socket)
        {
            ReqLogin reqLogin = ReqLogin.Parser.ParseFrom(msg, 0, msg.Length);
            Singleton<AccountConnectionManager>.Instance.AddNewConnectionInfo(reqLogin.AccountName, socket);

            //domo版,取消中间流程,直接进入游戏(随便建个房间)
            Singleton<RoomManager>.Instance.EnterRoom(100027, reqLogin.AccountName);
        }
    }
}
