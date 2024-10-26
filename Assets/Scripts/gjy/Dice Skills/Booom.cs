using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booom : MonoBehaviour
{
    int rangeMultipler = 1;

    double damageMultipler = 1;
    public void Initialize(int rangeMultipler, double damageMultipler, Vector3 pos)
    {
        transform.localScale = Vector3.one * rangeMultipler;

        this.damageMultipler = damageMultipler;

        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int hurt = (int)(PlayerController.GetInstance().pAtk * damageMultipler);
        collision.GetComponent<EnemyBase>().Hurt(hurt, ValType.physics);
    }
    public void booomEnd()
    {
        ObjectPool.GetInstance().PushGameObject(gameObject);
    }
}
