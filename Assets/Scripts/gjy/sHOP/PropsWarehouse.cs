using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DiceFacePropsWarehouse", menuName = "Data/DiceFacePropsWarehouse")]
public class PropsWarehouse : ScriptableObject
{
    public List<DiceFacePropsData> diceFacePropsWarehouse = new List<DiceFacePropsData>();

    public List<AttributePropsData> attributePropsWarehouse = new List<AttributePropsData>();

    public DiceFacePropsData getDiceFacePropsData(DiceFacePropsName name)
    {
        foreach(var v in diceFacePropsWarehouse)
        {
            if (v.propsName == name)
            {
                return v;
            }
        }
        Debug.LogWarning("未找到目标道具：" + name.ToString());

        return null;
    }
    public AttributePropsData getAttributePropsData(AttributePropsName name)
    {
        foreach (var v in attributePropsWarehouse)
        {
            if (v.propsName == name)
            {
                return v;
            }
        }
        Debug.LogWarning("未找到目标道具：" + name.ToString());

        return null;
    }
    public int getDiceFacePropsGrade(DiceFacePropsName name)
    {
        foreach (var v in diceFacePropsWarehouse)
        {
            if (v.propsName == name)
            {
                return v.grade;
            }
        }
        Debug.LogWarning("未找到目标道具：" + name.ToString());

        return -1;
    }
    public int getAttributePropsGrade(AttributePropsName name)
    {
        foreach (var v in attributePropsWarehouse)
        {
            if (v.propsName == name)
            {
                return v.grade;
            }
        }
        Debug.LogWarning("未找到目标道具：" + name.ToString());

        return -1;
    }
}