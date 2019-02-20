using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyPokerGameServer
{
   
    class AccountConnectionManager : Singleton<AccountConnectionManager>
    {
        private Dictionary<string, AccountConnectionInfo> _accountsInfoDic = new Dictionary<string, AccountConnectionInfo>();
        private Dictionary<Socket, string> _socketDic = new Dictionary<Socket, string>();  //key : socket, value : account

        public void AddNewConnectionInfo(string account, Socket socket)
        {
            if (!_accountsInfoDic.ContainsKey(account))
            {
                _accountsInfoDic[account] = new AccountConnectionInfo{ PhaseState = AccountPhaseState.LOBBY, OrigSocket = socket};
                _socketDic[socket] = account;
            }
        }

        public AccountConnectionInfo GetAccountConnectionInfo(string account)
        {
            AccountConnectionInfo result = null;
            if (_accountsInfoDic.ContainsKey(account))
            {
                result = _accountsInfoDic[account];
            }

            return result;
        }

        public void SendMsgToGameRoom(Socket socket, int msgId, byte[] msgBody)
        {
            if (_socketDic.ContainsKey(socket) && _accountsInfoDic.ContainsKey(_socketDic[socket]))
            {
                int roomId = _accountsInfoDic[_socketDic[socket]].IdOfRoomEntered;
                if (roomId != -1)
                {
                    Singleton<RoomManager>.Instance.GetRoom(roomId).OnReceiveMsg(_socketDic[socket], msgId, msgBody);
                }
            }
        }

        public void OnEnterRoom(string account, int roomId)
        {
            if (_accountsInfoDic.ContainsKey(account))
            {
                _accountsInfoDic[account].IdOfRoomEntered = roomId;
                _accountsInfoDic[account].PhaseState = AccountPhaseState.GAME;
            }
        }
    }
}
