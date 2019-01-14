using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokerGameServer
{
    class DDZGameData : Singleton<DDZGameData>
    {
        public List<AccountInfo> Players;
        public Dictionary<int, PlayerGameInfo> PlayerInfoDic = new Dictionary<int, PlayerGameInfo>();

        public void InitPlayerInfoDic(List<AccountInfo> players)
        {
            Players = players;
            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfoDic[i] = new PlayerGameInfo();
            }
        }
    }
}
