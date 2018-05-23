using UnityEngine;
using System.Collections;

public class PopupItemInfoManager : MonoBehaviour
{
    public static PopupItemInfoManager Get = null;
    public PopupItemInfoManager()
    {
        if (Get == null)
            Get = this;
    }

    public PopupItemInfo InfoMain;
    public PopupItemInfo InfoSub;
    public PlayerLevel player;

    public void SetActive(Item main,Item sub = null)
    {
        InfoMain.PlayerLevel = player.GetIntLv();
        InfoMain.item = main;
        if (InfoSub != null)
        {
            InfoSub.PlayerLevel = player.GetIntLv();
            InfoSub.item = sub;
        }
        gameObject.SetActive(true);
    }

    void Start()
    {

    }
}
