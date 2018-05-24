using UnityEngine;
using System.Collections;

public class UIItemEquipmentSlot : MonoBehaviour
{
    Item equipment;
    public enum EquipType
    {
        Armor,
        Helm,
        Other,
        Error
    }

    public UIItemInfoUpdater icon;
    public GameObject selectSlotImage;
    public GameObject EmptyImage;
    public EquipType type = EquipType.Error;

    
    public UIItemEquipmentSlot()
    {
        if(EquipTypeTable == null)
        {
            EquipTypeTable = new Hashtable();
            EquipTypeTable.Add("Armor",EquipType.Armor);
            EquipTypeTable.Add("Helm" ,EquipType.Helm);
            EquipTypeTable.Add("Other",EquipType.Other);
        }
    }

    public void Clicked()
    {
        UIItemEquipmentSlotManager.Get.Click(this);
        Select(true);
    }

    public void SetData(Item item)
    {
        if(item == null)
        {
            icon.gameObject.SetActive(false);
        }
        else
        {
            icon.SetData(item);
            icon.gameObject.SetActive(true);
        }
    }

    public void Select(bool val)
    {
        selectSlotImage.SetActive(val);
    }

    static Hashtable EquipTypeTable = null;
    public static EquipType GetEquipType(string key)
    {
        EquipType? result = EquipTypeTable[key] as EquipType?;
        if (result.HasValue) return result.Value;
        return EquipType.Error;
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
