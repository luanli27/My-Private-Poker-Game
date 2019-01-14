using System;
using System.Collections.Generic;

namespace MyPokerGameServer
{
    class RoomManager : Singleton<RoomManager>
    {
        private Dictionary<int, PokerRoom> _roomDic = new Dictionary<int, PokerRoom>();
        public void EnterRoom(int roomId, string account)
        {
            if (_roomDic.ContainsKey(roomId) && _roomDic[roomId].CanEnter(account))
            {
                _roomDic[roomId].EnterNewPlayer(account);
            }
            else
            {
                PokerRoom newRoom = new PokerRoom(roomId);
                _roomDic[roomId] = newRoom;
                newRoom.EnterNewPlayer(account);
            }
        }

        public PokerRoom GetRoom(int roomId)
        {
            return _roomDic[roomId];
        }
    }
}
