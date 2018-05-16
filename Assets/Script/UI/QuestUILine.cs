using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestUILine : MonoBehaviour
{
    public QuestData questInfo = null;
    public Image NPCIcon;
    public Text Name;
    public UIItemScrollList need;
    public UIItemScrollList reward;

    public void SetQuest(string id)
    {
        SetQuest(new QuestData(id));
    }
    public void SetQuest(QuestData s)
    {
        Name.text = s.Name;
        need.NewItemIconList(s.NeedItemList);
        reward.NewItemIconList(s.RewardItemList);
    }

    public void SetPosY(float y)
    {
        RectTransform v = gameObject.GetComponent<RectTransform>();
        v.anchoredPosition = new Vector2(v.anchoredPosition.x, y);
    }
}
