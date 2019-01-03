using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventName
{
    ASK_LOGIN,
}

public class AskLoginEventArg
{
    public AskLoginEventArg(string accountName, string passWord)
    {
        AccountName = accountName;
        PassWord = passWord;
    }

    public string AccountName;
    public string PassWord;
}
