using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    public Transform canvas;

    public GameObject panel;//道具界面

    private float timer = 0;

    public float stopTime;

    public float shootInterval;//射击间隔

    public int bulletNum;//子弹数量

    public float bulletAngle;//子弹偏转角度

    public GameObject bulletPrefab;

    public float bulletSpeed;

    private int sAtk;

    private bool canMove = false;

    private int shootTimes = 1;

    protected override void OnEnable()
    {
        canvas = GameObject.Find("Canvas").transform;
        panel = canvas.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        health = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 16) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 240 + n));
        atk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) + n)) * 3;
        pDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.6 * EnemyManager.GetInstance().wave + n)));
        mDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.6 * EnemyManager.GetInstance().wave + n)));
        dropMoney = (int)(60 * (EnemyManager.GetInstance().wave / 6 + 1));
        sAtk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 2 + n));
        currentHealth = health;
        StartCoroutine(DoShoot());
        audioData = AudioManager.Instance.getAudioData(AudioName.骑士官死亡);
    }

    protected override void Update()
    {
        GetDirection();

        timer += Time.deltaTime;
        if(timer >= stopTime)
        {
            canMove = !canMove;
            timer = 0;
        }

        if(canMove)
        {
            Move();
        }
        else
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
            yield return new WaitForSeconds(shootInterval);
            if(shootTimes % 3 == 0 && !isVertigo)
            {
                yield return new WaitForSeconds(5);
            }

            if(shootTimes % 2 != 0 && !isVertigo)
            {
                GetDirection();
                Shoot1(dirction);
                yield return new WaitForSeconds(0.5f);
                Shoot1(dirction);
                yield return new WaitForSeconds(0.5f);
                Shoot1(dirction);
                yield return new WaitForSeconds(0.5f);
                shootTimes++;
            }
            else if(!isVertigo)
            {
                Shoot2();
                shootTimes++;
            }
        }
    }

    private void Shoot1(Vector2 direction)
    {
        GameObject bullet = ObjectPool.GetInstance().GetGameObject(bulletPrefab);
        bullet.GetComponent<EnemyBullet>().val = sAtk;
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<EnemyBullet>().SetSpeed(dirction, bulletSpeed);
    }

    private void Shoot2()
    {
        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = ObjectPool.GetInstance().GetGameObject(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<EnemyBullet>().val = sAtk;
            //子弹数目为奇数时，当前子弹的与中间子弹的编号差值乘上间隔角度就得到了当前子弹的偏转角度
            if (bulletNum % 2 == 1)
            {
                bullet.GetComponent<EnemyBullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * Vector2.up, bulletSpeed);
            }
            //偶数子弹时，在奇数子弹的基础上要加上偏转角度的一半
            else
            {
                bullet.GetComponent<EnemyBullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2, Vector3.forward) * Vector2.up, bulletSpeed);
            }
        }                           
    }

    protected override void Death()
    {
        StopCoroutine(DoShoot());
        panel.SetActive(true);
        Time.timeScale = 0;
        PlayerController.GetInstance().canInput = false;
        base.Death();
    }
}
