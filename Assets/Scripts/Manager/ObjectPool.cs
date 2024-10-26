using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : SingletonMono<ObjectPool>
{
    private Dictionary<string, List<GameObject>> poolDic = new Dictionary<string, List<GameObject>>();
    private GameObject pool;

    public GameObject GetGameObject(GameObject prefab)
    {
        GameObject obj;
        //该类对象不存在或对象池为空
        if (!(poolDic.ContainsKey(prefab.name)) || poolDic[prefab.name].Count == 0)
        {
            obj = GameObject.Instantiate(prefab);
            if(!pool)
            {
                pool = new GameObject("ObjectPool");
            }
            GameObject child = GameObject.Find(prefab.name + "Pool");
            if(!child)
            {
                child = new GameObject(prefab.name + "Pool");
                child.transform.SetParent(pool.transform);
            }
            obj.transform.SetParent(child.transform);
        }
        else
        {
            obj = poolDic[prefab.name][0];
            poolDic[prefab.name].RemoveAt(0);
        }
        obj.SetActive(true);
        return obj;
    }

    public void PushGameObject(GameObject obj)
    {
        string objName = obj.name.Replace("(Clone)", string.Empty);
        if (!(poolDic.ContainsKey(objName)))
        {
            poolDic.Add(objName, new List<GameObject>());
        }
        poolDic[objName].Add(obj);
        obj.SetActive(false);
    }
}
