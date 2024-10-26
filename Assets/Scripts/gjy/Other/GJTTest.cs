using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GJTTest : MonoBehaviour
{
    BoxCollider2D coll;

    bool tr = true;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();

        Rigidbody2D rb;

      

     
    }

    // Update is called once per frame
    void Update()
    {
        if (tr)
        {
            GameObject g = Resources.Load<GameObject>("Prefab/Aura");

            Instantiate(g);

            Debug.Log("teststesttsts");

            tr = false;
        }
        
        if ((int)Time.time %3 == 0)
        {
            coll.size = coll.size + Vector2.one;
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("dasdasdasdasdasdas");
    //}
}
