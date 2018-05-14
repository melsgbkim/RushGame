using UnityEngine;
using System.Collections;

public class UISkillManager : MonoBehaviour
{
    public static UISkillManager Get = null;
    public UISkillManager()
    {
        if (Get == null)
            Get = this;
    }

    public GameObject SkillPrefab;

    public Transform root;
    public RectTransform Contents;

    public void SetContentsHeight(float h)
    {
        Contents.sizeDelta = new Vector2(Contents.rect.width, h);
    }
}
