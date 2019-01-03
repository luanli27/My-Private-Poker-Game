using UnityEngine;
public class Singleton<T> where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
               Debug.LogError("未在场景中找到" + typeof(T).Name + "类的实例对象，请检查Scene的脚本挂载状态！！");
            }
            return _instance;
        }

        set { _instance = value; }
    }
}
