using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using DG.Tweening;

public class PlayerController : SingletonMono<PlayerController>
{
    AudioData audioData1, audioData2;

    public GameObject panel, deathPanel;

    public int moneyMultiple = 1;//金钱倍数

    public float invincibilityTime;

    private float invincibilityTimer;

    public List<DiceController> dices;//所有持有的骰子

    public DiceController currentDice;//当前可以发射的骰子

    private int diceID = 0;

    public int money;//金钱

    public float speed;//玩家移动速度

    public int maxHealth;//最大生命值

    public int pAtk;//物理伤害

    public int mAtk;//魔法伤害

    public int currentHealth;//当前生命值

    private Rigidbody2D rb;

    private float horizontal, vertical;

    private Vector2 mousePos;//鼠标位置

    public bool canAttackted = true;

    public bool canPickDice, canPickMoney, canRevive;

    public bool canInput = true;//能否检测玩家输入

    public SpriteRenderer sr;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();    

        currentHealth = maxHealth;

        invincibilityTimer = invincibilityTime;

        StartCoroutine(AutoHeal());

        audioData1 = AudioManager.Instance.getAudioData(AudioName.打开菜单商店);
        audioData2 = AudioManager.Instance.getAudioData(AudioName.玩家死亡);
    }

    private void Update() 
    {
        JugeDice();
        Move(); 

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Instance.playVFX(audioData1);
            canInput = false;
            Time.timeScale = 0;
            panel.SetActive(true);
        }

        if(canPickDice)
        {
            AutoPickDice();
        }
        
        if(canPickMoney)
        {
            AutoPickMoney();
        }

        if(Input.GetMouseButtonDown(0) && currentDice != null && currentDice.state == DiceState.hold && canInput)
        {
            currentDice.ShootDice();
        }

        if(!canAttackted)
        {
            invincibilityTimer -= Time.deltaTime;
            if(invincibilityTimer <= 0)
            {
                canAttackted = true;
                invincibilityTimer = invincibilityTime;
            }
        }

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    //判断骰子是否在手上
    private void JugeDice()
    {
        for(int i = 0;i < dices.Count;i++)
        {
            if(dices[i].state == DiceState.hold && currentDice == null)
            {
                currentDice = dices[i];
                currentDice.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(horizontal, vertical).normalized * speed;//向量归一化

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePos.x > this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    //受到伤害
    public void Hurt(int val)
    {
        if(canAttackted)
        {
            StartCoroutine(HurtAnim());
            Camera.main.DOShakePosition(0.5f, 0.2f);
            if(currentHealth - val < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= val;
            }
            canAttackted = false;
        }
    }

    //玩家死亡
    private void Death()
    {
        if(canRevive)
        {
            currentHealth = 10 * (EnemyManager.GetInstance().wave / 3 + 1);
            canAttackted = false;
            invincibilityTimer = 5;
            canRevive = false;
            return;
        }
        else
        {
            AudioManager.Instance.playVFX(audioData2);
            deathPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //获取当前骰子
    public DiceController GetDice()
    {
        return dices[diceID];
    }

    //获取前一个骰子
    public DiceController GetForDice()
    {
        if(diceID > 0)
        {
            diceID--;
        }
        else
        {
            diceID = dices.Count - 1;
        }
        return dices[diceID];
    }

    //获取后一个骰子
    public DiceController GetAfterDice()
    {
        if(diceID < dices.Count - 1)
        {
            diceID++;
        }
        else
        {
            diceID = 0;
        }
        return dices[diceID];
    }

    public void AutoPickDice()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Dice");
        for(int i = 0;i < obj.Length;i++)
        {
            if(obj[i].GetComponent<DiceController>().state == DiceState.ground && obj[i].GetComponent<DiceController>().canAutoPick)
            {
                obj[i].GetComponent<DiceController>().AutoPick();
            }
        }
    }

    public void AutoPickMoney()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Money");
        for(int i = 0;i < obj.Length;i++)
        {
            obj[i].GetComponent<Money>().AutoPick();
        }
    }

    //金钱获取倍率翻倍
    public void DoDoubleMoney(float time, int val)
    {
        StartCoroutine(DoubleMoney(time, val));
    }

    IEnumerator DoubleMoney(float time, int val)
    {
        moneyMultiple *= val;
        yield return new WaitForSeconds(time);
        moneyMultiple /= val;
        yield break;
    }

    //自动恢复生命值
    IEnumerator AutoHeal()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            if(currentHealth < maxHealth)
            {
                currentHealth += (int)Mathf.Round((float)(maxHealth * 0.05));
            }
        }
    }

    IEnumerator HurtAnim()
    {
        sr.DOColor(Color.red, 0.5f);
        yield return new WaitForSeconds(0.5f);
        sr.DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(0.5f);
        yield break;
    }
}
