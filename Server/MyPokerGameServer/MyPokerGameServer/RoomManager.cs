using System;
using System.Collections.Generic;

namespace MyPokerGameServer
{
    class RoomManager : Singleton<RoomManager>
    {
        private Dictionary<int, PokerRoom> _roomDic = new Dictionary<int, PokerRoom>();
        public void EnterRoom(int roomId, Player player)
        {
            if (_roomDic.ContainsKey(roomId) && _roomDic[roomId].CanEnter(player))
            {
                _roomDic[roomId].EnterNewPlayer(player);
            }
            else
            {
                PokerRoom newRoom = new PokerRoom(roomId);
                _roomDic[roomId] = newRoom;
                newRoom.EnterNewPlayer(player);
            }
        }
    }
}
