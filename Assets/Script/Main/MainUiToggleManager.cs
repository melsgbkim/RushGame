using UnityEngine;
using System.Collections;

public class MainUiToggleManager : MonoBehaviour
{
    bool UIStat = false;
    bool UISkill = false;
    bool UIInven = false;
    bool UIUpgrade = false;
    bool UIQuest = false;
    bool UIOption = false;

    public void ToggleUIStat() { UIStat = !UIStat; }
    public void ToggleUISkill() { UISkill = !UISkill; }
    public void ToggleUIInven() { UIInven = !UIInven; }
    public void ToggleUIUpgrade() { UIUpgrade = !UIUpgrade; }
    public void ToggleUIQuest() { UIQuest = !UIQuest; }
    public void ToggleUIOption() { UIOption = !UIOption; }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(UIStat + " " +
             UISkill + " " +
             UIInven + " " +
              UIUpgrade + " " +
              UIQuest + " " +
              UIOption + " ");
    }
}