using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 特殊道具的管理器
/// </summary>
public class SpPropManager : SingletonMono<SpPropManager>
{
    public List<bool> canGet = new List<bool> { true, true, true, true };

    public List<GameObject> gameObjects;

    private void OnEnable() 
    {
        for(int i = 0;i < gameObjects.Count;i++)
        {
            if(canGet[i])
            {
                GameObject obj = Instantiate(gameObjects[i]);
                obj.transform.SetParent(this.transform);
            }
        }
    }

    private void OnDisable() 
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
}
