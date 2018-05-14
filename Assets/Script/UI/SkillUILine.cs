using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUILine : MonoBehaviour
{
    public SkillInfo skillInfo = null;
    public Image Icon;
    public Text before;
    public Text after;
    public Button plusButton;

    public void SetSkill(string id)
    {
        SetSkill(new SkillInfo(id));
    }
    public void SetSkill(SkillInfo s)
    {
        skillInfo = s;

        Icon.sprite = skillInfo.Sprite;
        before.text = skillInfo.Info + "_B";
        after.text  = skillInfo.Info + "_A";
    }

    public void SetPosY(float y)
    {
        RectTransform v = gameObject.GetComponent<RectTransform>();
        v.anchoredPosition = new Vector2(v.anchoredPosition.x, y);
        
    }
    public void SkillPointAdd(int val)
    {
        print("SkillPointAdd " + val);
    }
}
