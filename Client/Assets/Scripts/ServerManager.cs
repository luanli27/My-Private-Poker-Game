using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    void Start()
    {
        NetworkManager.Instance.Start();
        SeverEventHandler.Instance.Start();
    }

    void Update()
    {
        SeverEventHandler.Instance.Update();
    }
}
