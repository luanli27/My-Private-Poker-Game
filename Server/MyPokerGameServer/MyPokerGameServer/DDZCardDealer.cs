using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokerGameServer
{
    class DDZCardDealer
    {
        private List<CardId> GetCardPool()
        {
            List<CardId> result = new List<CardId>();
            for (int id = 0; id < 54; id++)
            {
                CardId cardId = new CardId();
                cardId.Id = id;
                result.Add(cardId);
            }

            return result;
        }

        class CardId
        {
            public int Id = -1;
        }

        public Dictionary<int, List<int>> Deal(int playerNum)
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            List<CardId> cardPool = GetCardPool();
            Random random = new Random();
            int startPlayerIndex = 0;
            for (int i = 0; i < playerNum; i++)
            {
                result[i] = new List<int>();
            }

            int totalCardsNum = cardPool.Count;
            for (int i = 0; i < totalCardsNum; i++)
            {
                int val = random.Next(cardPool.Count);
                result[startPlayerIndex].Add(cardPool[val].Id);
                startPlayerIndex = startPlayerIndex == playerNum - 1 ? 0 : startPlayerIndex + 1;
                cardPool.Remove(cardPool[val]);
            }

            foreach (var kv in result)
            {
                string cardListStr = "";          
                foreach (int card in kv.Value)
                {
                    cardListStr = cardListStr + ", " + card;
                }

                Console.WriteLine("玩家"+kv.Key+"的手牌池为："+cardListStr);
            }

            return result;
        }
    }
}
