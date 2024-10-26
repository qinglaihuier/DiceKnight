using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy : MonoBehaviour
{
    // Start is called before the first frame update
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
        int hurt = (int)(PlayerController.GetInstance().mAtk * damageMultipler);
        Debug.Log("สฅนโ");
        collision.GetComponent<EnemyBase>().Hurt(hurt, ValType.magic);
    }
    public void holyEnd()
    {
        ObjectPool.GetInstance().PushGameObject(gameObject);
    }
}
