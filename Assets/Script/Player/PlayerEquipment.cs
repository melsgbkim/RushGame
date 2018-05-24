using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEquipment : MonoBehaviour
{
    public const int EquipArrayMax = 10;
    UIItemEquipmentSlot.EquipType[] TypeArray = null;
    Item[] EquipArray = new Item[EquipArrayMax];
    public Item[] ItemArr{ get {return EquipArray; } }

    public StatusValues StatInt;
    public StatusValues StatPer;


    public void EquipItem(Item i,int index)
    {
        if (CheckSameEquipType(i.data, index) == false) return;
        int Contains = GetItemIndex(i);
        if (Contains != -1)
            EquipArray[Contains] = null;
        EquipArray[index] = i;

        UpdateBonusStat();
    }

    bool CheckSameEquipType(ItemData data,int index)
    {
        if (TypeArray == null)
        {
            TypeArray = new UIItemEquipmentSlot.EquipType[10];
            TypeArray[0] = UIItemEquipmentSlot.EquipType.Helm;
            TypeArray[1] = UIItemEquipmentSlot.EquipType.Armor;
            for(int i=2;i< EquipArray.Length; i++) TypeArray[i] = UIItemEquipmentSlot.EquipType.Other;
        }

        return TypeArray[index] == UIItemEquipmentSlot.GetEquipType(data.EquipType);
    }

    public int GetItemIndex(Item item)
    {
        for (int i=0;i< EquipArray.Length;i++)
        {
            if (EquipArray[i] == item)
                return i;
        }
        return -1;
    }


    public StatusValues GetStatInt()
    {
        int level = GetComponent<PlayerLevel>().level.level;
        StatusValues result = new StatusValues(StatusValues.TYPE.integer);
        for (int i = 0; i < EquipArray.Length; i++)
        {
            if (EquipArray[i] != null)
                result += EquipArray[i].OptionValues(level);
        }
        return result;
    }

    public StatusValues GetStatPer()
    {
        int level = GetComponent<PlayerLevel>().level.level;
        StatusValues result = new StatusValues(StatusValues.TYPE.percent);
        for (int i = 0; i < EquipArray.Length; i++)
        {
            if (EquipArray[i] != null)
                result += EquipArray[i].OptionValuesPer(level);
        }
        return result;
    }



    void UpdateBonusStat()
    {
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
