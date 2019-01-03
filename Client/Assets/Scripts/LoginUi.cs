using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUi : MonoBehaviour {

    public Button LoginButton;
    public InputField Account;
    public InputField Password;
	// Use this for initialization
	void Start () {
        LoginButton.onClick.AddListener(OnRequeseLogin);
        //new strange.framework.api.();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnRequeseLogin()
    {
        EventManager.Instance.DispatchEvent(EventName.ASK_LOGIN, new AskLoginEventArg(Account.text, Password.text));
    }

    void OnLoginResponse()
    {

    }
}
