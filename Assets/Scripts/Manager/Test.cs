using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject obj;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            for(int i = 0;i < 4;i++)
            {
                GameObject gameObject = Instantiate(obj);
                gameObject.transform.SetParent(this.transform);
            }
        }
    }
}
