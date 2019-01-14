
namespace MyPokerGameServer
{
    class Program
    {
        private static NetworkManager _networkManager = new NetworkManager();
        static void Main(string[] args)
        {
            _networkManager.Start();
        }
    }
}
