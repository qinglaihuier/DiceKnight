using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : EnemyBase
{
    public LayerMask layer1, layer2;

    public float radius;//爆炸范围

    public float forTime;//爆炸前摇

    public float dis;//距离范围

    private int sAtk;

    protected override void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        health = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 16) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 50 + n));
        atk = 0;
        pDef = 0;
        mDef = 0;
        sAtk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 5 + n));
        dropMoney = (int)(1 * (EnemyManager.GetInstance().wave / 6 + 1));
        currentHealth = health;
        audioData = AudioManager.Instance.getAudioData(AudioName.骰子爆炸);
    }

    protected override void Update()
    {
        GetDirection();
        Move();
        if(currentHealth <= 0)
        {
            Death();
        }

        if(Vector2.Distance(this.transform.position, target.transform.position) < dis && !isVertigo)
        {
            StartCoroutine(DoExplode());
        }

    }

    IEnumerator DoExplode()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(forTime / 2);
        GetDirection();
        if(Vector2.Distance(this.transform.position, target.transform.position) > dis)
        {
            yield break;
        }
        yield return new WaitForSeconds(forTime / 2);
        ExplodeHurt();
    }

    private void ExplodeHurt()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, layer1);
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, layer2);
        for(int i = 0;i < enemies.Length;i++)
        {
            enemies[i].GetComponent<EnemyBase>().Hurt(sAtk * 3, ValType.physics);
        }
        if(player != null)
        {
            player.GetComponent<PlayerController>().Hurt(sAtk);
        }
        Death();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
    
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        
    }
}
