using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : EnemyBase
{
    public float assaultSpeed;//冲锋速度

    public float forTime;//冲锋前摇

    public float assaultTime;//冲锋时间

    public float dis;//距离范围

    public float assaultCD;//冲锋cd

    private float timer;

    private int sAtk;

    private bool isAssault = false;//判断当前是否处于冲锋状态

    protected override void OnEnable() 
    {
        rb = GetComponent<Rigidbody2D>();
        health = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 16) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 60 + n));
        atk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave + n)));
        pDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.6 * EnemyManager.GetInstance().wave + n)));
        mDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.2 * EnemyManager.GetInstance().wave + n)));
        dropMoney = (int)(3 * (EnemyManager.GetInstance().wave / 6 + 1));
        sAtk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 3 + n));
        currentHealth = health;
        timer = 0;
        audioData = AudioManager.Instance.getAudioData(AudioName.冲锋恶魔死亡);
    }

    protected override void Update()
    {
        if(!isAssault)
        {
            timer -= Time.deltaTime;
            GetDirection();
            Move();
        }

        if(timer <= 0 && !isAssault && Vector2.Distance(this.transform.position, target.transform.position) < dis && !isVertigo)
        {
            StartCoroutine(DoAssault());
        }

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    IEnumerator DoAssault()
    {
        isAssault = true;
        timer = assaultCD;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(forTime);
        GetDirection();
        rb.velocity = dirction * assaultSpeed;
        yield return new WaitForSeconds(assaultTime);
        isAssault = false;
        yield break;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && !isAssault)
        {
            PlayerController.GetInstance().Hurt(atk);
        }    
        else if(other.gameObject.name == "Player" && isAssault)
        {
            PlayerController.GetInstance().Hurt(sAtk);
        }
    }

    protected override void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Player" && !isAssault)
        {
            PlayerController.GetInstance().Hurt(atk);
        }    
        else if(other.gameObject.name == "Player" && isAssault)
        {
            PlayerController.GetInstance().Hurt(sAtk);
        }
    }
}
