using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokerGameServer
{
    class DDZCardDealer
    {
        private List<int> _lordCards = new List<int>();
        private int _normalCardsNum = 51;
        private List<int> GetCardPool()
        {
            List<int> result = new List<int>();
            for (int id = 0; id < 54; id++)
                result.Add(id);

            return result;
        }

        public List<int> LordCards => _lordCards;

        public Dictionary<int, List<int>> Deal(int playerNum)
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            List<int> cardPool = GetCardPool();
            Random random = new Random();
            int startPlayerIndex = 0;
            for (int i = 0; i < playerNum; i++)
                result[i] = new List<int>();

            for (int i = 0; i < _normalCardsNum; i++)
            {
                int val = random.Next(cardPool.Count);
                result[startPlayerIndex].Add(cardPool[val]);
                startPlayerIndex = startPlayerIndex == playerNum - 1 ? 0 : startPlayerIndex + 1;
                cardPool.Remove(cardPool[val]);
            }

            _lordCards = cardPool;

            foreach (var kv in result)
            {
                string cardListStr = "";          
                foreach (int card in kv.Value)
                    cardListStr = cardListStr + ", " + card;

                Console.WriteLine("玩家"+kv.Key+"的手牌池为："+cardListStr);
            }

            return result;
        }
    }
}
