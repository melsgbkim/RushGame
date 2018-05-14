using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUILine : MonoBehaviour
{
    public SkillData skillInfo = null;
    public Image Icon;
    public Text before;
    public Text after;
    public Button plusButton;

    public void SetSkill(string id)
    {
        SetSkill(new SkillData(id));
    }
    public void SetSkill(SkillData s)
    {
        skillInfo = s;

        Icon.sprite = skillInfo.SpriteWithIndex;
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
