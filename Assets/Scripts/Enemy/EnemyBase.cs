using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject damagePrefab;

    public GameObject obj;//眩晕标记

    protected AudioData audioData;

    public int dropMoney;//掉落金钱数

    public float speed;//速度

    public int health;//生命值

    public int currentHealth;//当前生命值

    public int pDef;//物抗

    public int mDef;//魔抗

    public int atk;//伤害

    public int weight;//权值

    protected Rigidbody2D rb;

    protected Vector2 dirction;//移动方向

    protected GameObject target;//目标物体

    public GameObject moneyObj;

    protected bool isVertigo = false; 

    protected float n = 0.00001f;

    protected virtual void OnEnable() 
    {
        rb = GetComponent<Rigidbody2D>();
        health = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 16) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 40 + n));
        atk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) + n));
        pDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.2 * EnemyManager.GetInstance().wave) + n));
        mDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.2 * EnemyManager.GetInstance().wave) + n));
        dropMoney = (int)(1 * (EnemyManager.GetInstance().wave / 6 + 1));
        currentHealth = health;
        audioData = AudioManager.Instance.getAudioData(AudioName.飞行普通恶魔死亡);
    }

    protected virtual void Update() 
    {
        GetDirection();
        Move();
        if(currentHealth <= 0)
        {
            Death();
        }
    }

    //获取追击方向
    protected virtual void GetDirection()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Attacked");
        int id = 0;//存储当前最近的可攻击物体的id
        float dis = 0;//物体距离
        for(int i = 0;i < objs.Length;i++)
        {
            if(dis > Vector2.Distance(objs[i].transform.position, this.gameObject.transform.position))
            {
                dis = Vector2.Distance(objs[i].transform.position, this.gameObject.transform.position);
                id = i;
            }
        }
        target = objs[id];
        dirction = (target.transform.position - this.gameObject.transform.position).normalized;
    }

    protected virtual void Move()
    {
        if(!isVertigo)
        {
            rb.velocity = dirction * speed;
            if(target.transform.position.x > this.transform.position.x)
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    //死亡
    protected virtual void Death()
    {
        AudioManager.Instance.playVFX(audioData);
        GameObject money = ObjectPool.GetInstance().GetGameObject(moneyObj);
        money.GetComponent<Money>().val = dropMoney;
        money.transform.position = this.gameObject.transform.position;
        ObjectPool.GetInstance().PushGameObject(this.gameObject);
    }

    //受伤
    public virtual void Hurt(int val, ValType valType)
    {
        if(valType == ValType.physics)
        {
            if(val - pDef <= 0)
            {
                currentHealth -= 1;
                GameObject obj = ObjectPool.GetInstance().GetGameObject(damagePrefab);
                obj.transform.position  = gameObject.transform.position;
                obj.GetComponent<Demage>().Init(1.ToString());
            }
            else
            {
                currentHealth -= (val - pDef);
                GameObject obj = ObjectPool.GetInstance().GetGameObject(damagePrefab);
                obj.transform.position  = gameObject.transform.position;
                obj.GetComponent<Demage>().Init((val - pDef).ToString());
            }
        }
        else
        {
            if(val - mDef <= 0)
            {
                currentHealth -= 1;
                GameObject obj = ObjectPool.GetInstance().GetGameObject(damagePrefab);
                obj.transform.position  = gameObject.transform.position;
                obj.GetComponent<Demage>().Init(1.ToString());
            }
            else
            {
                currentHealth -= (val - mDef);
                GameObject obj = ObjectPool.GetInstance().GetGameObject(damagePrefab);
                obj.transform.position  = gameObject.transform.position;
                obj.GetComponent<Demage>().Init((val - mDef).ToString());
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            PlayerController.GetInstance().Hurt(atk);
        }   
    }
    
    protected virtual void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            PlayerController.GetInstance().Hurt(atk);
        }  
    }

    //眩晕该敌人
    public void DoVertigo(float time)
    {
        StartCoroutine(Vertigo(time));
    }

    IEnumerator Vertigo(float time)
    {
        obj.SetActive(true);
        isVertigo = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
        isVertigo = false;
        yield break;
    }

    //判断其生命值是否小于固定百分比
    public bool JudgeHealth(int val)
    {
        if(currentHealth <= health * (val / 100))
        {
            return true;
        }
        return false;
    }
}

/// <summary>
/// 伤害类型
/// </summary>
public enum ValType
{
    physics,//物理
    magic,//魔法
}