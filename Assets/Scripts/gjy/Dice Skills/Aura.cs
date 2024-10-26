using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Aura : MonoBehaviour
{
    float damageMultiplier = 1;

    float rangeMultiplier = 1;

    float lifeCycleSize = 1;

    float interval = 1f;

    Collider2D coll;

    Rigidbody2D rb;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();

        Debug.Log("Aura Awake");

        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Debug.Log("Aura OnEnable");
    }
    private void OnDisable()
    {
        StopAllCoroutines();    
    }

    private void Start()
    {
        Debug.Log("Aura Start");
    }
    private void Update()
    {
        Debug.Log("Aura Update");
    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.parent.position);
    }
    public void Initialize(float damageMultiplier, float rangeMultiplier, float lifeCycleSize, Transform parent)
    {
        this.damageMultiplier = damageMultiplier;

        this.rangeMultiplier = rangeMultiplier;

        this.lifeCycleSize = lifeCycleSize;

        transform.parent = parent;

        transform.localPosition = Vector3.zero;

        transform.localScale = Vector3.one * rangeMultiplier;

        transform.localPosition = Vector3.zero;

        StartCoroutine(nameof(AttackCoroutine));
    }
    public void Initialize(float damageMultiplier, float rangeMultiplier, float lifeCycleSize)
    {
        this.damageMultiplier = damageMultiplier;

        this.rangeMultiplier = rangeMultiplier;

        this.lifeCycleSize = lifeCycleSize;

        transform.localScale = Vector3.one * rangeMultiplier;
    }
    IEnumerator AttackCoroutine()
    {
        float t = 0;

        WaitForSeconds wait = new WaitForSeconds(interval);

        while(t < lifeCycleSize)
        {
            Attack();

            t += interval;

            yield return wait;
        }

        ObjectPool.GetInstance().PushGameObject(gameObject); 
    }
    void Attack()
    {
        //ÆäËû
        Collider2D[] enemys = new Collider2D[1000];

        int count = Physics2D.GetContacts(coll, enemys);

        foreach (var enemy in enemys)
        {
            if(enemy != null) 
                enemy.GetComponent<EnemyBase>().Hurt((int)(damageMultiplier * PlayerController.GetInstance().mAtk), ValType.magic);
        }
        //var enemys = Physics2D.OverlapCircleAll(transform.position, rangeMultiplier * PropsSkillManager.Instance.rangeSize, LayerMask.GetMask("Enemy"));

            //foreach(var enemy in enemys)
            //{
            //    enemy.GetComponent<EnemyBase>().Hurt((int)(damageMultiplier * PlayerController.GetInstance().mAtk), ValType.magic);
            //}
    }
}
