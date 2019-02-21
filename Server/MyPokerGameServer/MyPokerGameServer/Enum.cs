using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPokerGameServer
{
    public enum AccountPhaseState
    {
        UNKNOW,
        LOBBY,
        GAME
    }

    public enum CallLordState
    {
        CALL_LORD,
        GRAP_LORD,
    }

    public enum CallLordResultState
    {
        CALL_LORD,
        GIVE_UP_CALL_LORD,
        GRAP_LORD,
    }
}
