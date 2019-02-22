using System;
using System.Collections.Generic;

namespace MyPokerGameServer
{
    class RoomManager : Singleton<RoomManager>
    {
        private Dictionary<int, DDZPokerRoom> _roomDic = new Dictionary<int, DDZPokerRoom>();
        public void EnterRoom(int roomId, string account)
        {
            if (_roomDic.ContainsKey(roomId) && _roomDic[roomId].CanEnter(account))
            {
                _roomDic[roomId].EnterNewPlayer(account);
            }
            else
            {
                DDZPokerRoom newRoom = new DDZPokerRoom(roomId);
                _roomDic[roomId] = newRoom;
                newRoom.EnterNewPlayer(account);
            }
        }

        public DDZPokerRoom GetRoom(int roomId)
        {
            return _roomDic[roomId];
        }
    }
}
