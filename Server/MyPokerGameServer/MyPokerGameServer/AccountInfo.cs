namespace MyPokerGameServer
{
    class AccountInfo
    {
        public string Name { set; get; }
        public int GoldNum { set; get; }

        public AccountInfo(string name, int goldNum)
        {
            Name = name;
            GoldNum = goldNum;
        }
    }
}
