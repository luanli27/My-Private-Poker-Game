using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokerGameServer
{
    class DDZGameData : Singleton<DDZGameData>
    {
        public int PlayerCount = 3;
        public List<AccountInfo> Players;
        public Dictionary<int, PlayerGameInfo> PlayerInfoDic = new Dictionary<int, PlayerGameInfo>();
        public Dictionary<int, PlayerDataInGame> PlayerInfoInGameDic = new Dictionary<int, PlayerDataInGame>();
        public int CurrentCallLordSeat = 0;
        public bool IsGrapLord;

        public void InitPlayerInfoDic(List<AccountInfo> players)
        {
            Players = players;
            for (int i = 0; i < players.Count; i++)
                PlayerInfoDic[i] = new PlayerGameInfo();
        }
    }

    class PlayerDataInGame
    {
        public CallLord CallLordResultState;
    }
}
