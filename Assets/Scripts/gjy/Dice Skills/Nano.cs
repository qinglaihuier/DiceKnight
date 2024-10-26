using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nano : MonoBehaviour
{
    Collider2D coll;
    private void OnEnable()
    {
        StartCoroutine(nameof(lifeCoroutine));
    }
    // Start is called before the first frame update
    public void Initialie(float rangeMultiplier, Vector2 pos)
    {
        transform.position = pos;

        transform.localScale = Vector3.one * rangeMultiplier;

        coll = GetComponent<Collider2D>();
    }


    // Update is called once per frame
   public void excute()
    {
        Collider2D[] enemys = new Collider2D[1000];

        int count = Physics2D.GetContacts(coll, enemys);
              
        foreach (var enemy in enemys)
        {
            if(enemy != null)
            {
                if (enemy.GetComponent<EnemyBase>().JudgeHealth(20))
                {
                    enemy.GetComponent<EnemyBase>().Hurt(999999, ValType.physics);
                }
            }
           
        }
    }
    IEnumerator lifeCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        ObjectPool.GetInstance().PushGameObject(gameObject);
    }
}
