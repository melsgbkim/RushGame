using UnityEngine;
using System.Collections;

public class UIUpgradeItemPreview : MonoBehaviour
{
    public PopupItemInfo InfoMain;
    public PopupItemInfo InfoSub;
    //public PlayerLevel player;

    public Level mainLevel;
    public Level subLevel;

    public void SetActive(Item main, Item sub = null)
    {
        InfoMain.item = main;
        InfoSub.item = sub;

        mainLevel = new Level(InfoMain.item.level);
        subLevel = new Level(mainLevel);

        InfoMain.SetExpBar(mainLevel);
        InfoSub.SetExpBar(subLevel);
        gameObject.SetActive(true);
    }

    public Item item { get { return InfoMain.item; } }

    public int EXP
    {
        set
        {
            subLevel.SetExp(value + mainLevel.sumExp);
            InfoSub.SetExpBar(subLevel);
        }
    }

    public void UpgradeOK()
    {
        InfoMain.item.SetLevel(subLevel);
        SetActive(InfoMain.item, InfoMain.item);
    }
}
