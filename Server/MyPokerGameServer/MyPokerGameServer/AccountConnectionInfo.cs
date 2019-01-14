
using System.Net.Sockets;

namespace MyPokerGameServer
{
    class AccountConnectionInfo
    {
        public Socket OrigSocket;
        public AccountPhaseState PhaseState = AccountPhaseState.UNKNOW;
        public int IdOfRoomEntered = -1;
    }
}
