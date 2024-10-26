using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : EnemyBase
{
    public float range1, range2;//追击范围

    public float bulletSpeed;

    [SerializeField] private int sAtk;

    public float shootInterval;//射击间隔

    [SerializeField] private GameObject bulletPrefab;//子弹预制体

    protected override void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        health = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 16) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 12 + n));
        atk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) + n));
        pDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.2 * EnemyManager.GetInstance().wave) + n));
        mDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.6 * EnemyManager.GetInstance().wave) + n));
        dropMoney = (int)(2 * (EnemyManager.GetInstance().wave / 6 + 1));
        sAtk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 2 + n));
        currentHealth = health;
        StartCoroutine(DoShoot());
        audioData = AudioManager.Instance.getAudioData(AudioName.法师恶魔死亡);
    }

    protected override void Update()
    {
        GetDirection();
        if(Vector2.Distance(target.transform.position, this.transform.position) > range1)
        {
            Move();
        }
        else if(Vector2.Distance(target.transform.position, this.transform.position) < range2)
        {
            rb.velocity = Vector2.zero;
        }

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    IEnumerator DoShoot()
    {
        while(true)
        {
            if(!isVertigo)
            {
                yield return new WaitForSeconds(shootInterval);
                GetDirection();
                Shoot(dirction);
            }
        }
    }

    private void Shoot(Vector2 direction)
    {
        GameObject bullet = ObjectPool.GetInstance().GetGameObject(bulletPrefab);
        bullet.GetComponent<EnemyBullet>().val = sAtk;
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<EnemyBullet>().SetSpeed(dirction, bulletSpeed);
    }

    protected override void Death()
    {
        StopCoroutine(DoShoot());
        base.Death();
    }
}
