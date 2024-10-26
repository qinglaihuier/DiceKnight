using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StunSkill : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D coll;

    int stunTime = 1;

    double damageMultiplier = 0;

    int rangeMultiplier = 1;

    public void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(lifeCoroutine));
    }

    public void Initialie(int stunTime, double damageMultiplier, int rangeMultiplier, Vector2 pos)
    {
        this.stunTime = stunTime;

        this.damageMultiplier = damageMultiplier;

        transform.position = pos;

        transform.localScale = Vector3.one * rangeMultiplier;
    }
    public void excute()
    {
        Collider2D[] enemys = new Collider2D[1000];

        int count = Physics2D.GetContacts(coll, enemys);

        for (int i = 0; i < count; ++i)
        {
            enemys[i].GetComponent<EnemyBase>().DoVertigo(stunTime);

            enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * damageMultiplier), ValType.physics);
        }
    }
    IEnumerator lifeCoroutine()
    {
        yield return new WaitForSeconds(1f);

        ObjectPool.GetInstance().PushGameObject(gameObject);
    }
}
