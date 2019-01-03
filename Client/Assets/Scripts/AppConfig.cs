class Appconfig : Singleton<Appconfig>
{
    public string appWebSocket_IP = "127.0.0.1";
    public int appWebSocket_PORT = 2700;
    public string gameWebSocket_IP ="testcyapp.moigame.cn";
    public int gameWebSocket_PORT = 22203;

    public string guestLoginUrl ="http://testcyapp.moigame.cn:7089/guest";
    public string soundMsgUpUrl ="http://testcyapp.moigame.cn:7087/upload";
    // this.soundMsgDownUrl ="http://testcyapp.moigame.cn:7087/load?file=";//
    public string soundMsgDownUrl ="http://testcyapp.moigame.cn:7087/myrecord/";
    public string iventFriendPage ="http://cy.moigame.cn/nimizi/gameDownload.do?dt=cy";
    public string appDownloadPage ="http://cy.moigame.cn/nimizi/gameDownload.do?dt=cy";
    public string iventFriendPage2 ="http://cy.moigame.cn/nimizi/gameDownload2.do?dt=cy";
    public string appDownloadPage2 ="http://cy.moigame.cn/nimizi/gameDownload2.do?dt=cy";
    public string wechatLogin = "http://testcyapp.moigame.cn:7089/login";
}