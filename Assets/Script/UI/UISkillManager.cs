using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UISkillManager : MonoBehaviour
{
    public static UISkillManager Get = null;
    public UISkillManager()
    {
        if (Get == null)
            Get = this;
    }

    public GameObject Prefab;

    public Transform root;
    public RectTransform Contents;
    public List<SkillUILine> ObjectList = new List<SkillUILine>();

    public GameObject NewPrefab(string id)
    {
        GameObject result = Instantiate(Prefab);
        result.transform.SetParent(root);
        result.transform.localPosition = Vector3.zero;

        SkillUILine line = result.GetComponent<SkillUILine>();
        line.SetSkill(id);
        line.SetPosY(ObjectList.Count * Prefab.GetComponent<RectTransform>().rect.height * -1);
        ObjectList.Add(line);

        float height = ObjectList.Count * Prefab.GetComponent<RectTransform>().rect.height;
        if (height < 1020f) height = 1020f;
        SetContentsHeight(height);
        return result;
    }

    
    

    public void SetContentsHeight(float h)
    {
        Contents.sizeDelta = new Vector2(Contents.rect.width, h);
    }

    void Start()
    {
        NewPrefab("Skill_0001");
        NewPrefab("Skill_0002");
        NewPrefab("Skill_0003");
        NewPrefab("Skill_0004");
        NewPrefab("Skill_0005");
        NewPrefab("Skill_0006");
    }
    int tmp = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NewPrefab("Skill_000" + tmp);
            tmp++;
            if (tmp >= 7) tmp = 1;
        }
    }
}
