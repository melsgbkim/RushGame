using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillUILine : MonoBehaviour
{
    public Image Icon;
    public Text before;
    public Text after;
    public Button plusButton;

    public void SkillPointAdd(int val)
    {
        print("SkillPointAdd " + val);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
