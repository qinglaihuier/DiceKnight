using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    private Animator anim;

    private AudioData audioData1, audioData2;

    public DiceFaceSkill[] diceFace = new DiceFaceSkill[6];

    public Transform weaponPos;//武器位置

    public float speed;

    private Rigidbody2D rb;

    public DiceState state = DiceState.hold;//骰子状态

    public Vector2 mousePos;//鼠标位置

    private Vector2 direction;//飞行方向

    public bool canAutoPick;

    private void Awake() 
    {
        weaponPos = GameObject.Find("WeaponPos").transform;
        anim = GetComponent<Animator>();    
        audioData1 = AudioManager.Instance.getAudioData(AudioName.骰子投掷);
        audioData2 = AudioManager.Instance.getAudioData(AudioName.骰子撞击);
    }

    private void Update() 
    {
        if(Vector2.Distance(mousePos, this.gameObject.transform.position) < 0.1f && state == DiceState.flying)
        {
            StopDice();
        }
    }

    //发射骰子
    public void ShootDice()
    {
        anim.SetBool("IsRun", true);

        AudioManager.Instance.playVFX(audioData1);

        PlayerController.GetInstance().currentDice = null;

        state = DiceState.flying;

        rb = this.gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;

        this.transform.SetParent(null);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousePos - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;

        rb.velocity = direction * speed;
    }

    //骰子停止
    public void StopDice()
    {
        anim.SetBool("IsRun", false);
        state = DiceState.ground;
        rb.velocity = Vector2.zero;
        //执行骰面的效果
        int x = Random.Range(0, 6);
        Debug.Log(x);
        if(diceFace[x] != null)
        {
            diceFace[x].excute(this);
        }
        StartCoroutine(UpdatePick());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy" && state == DiceState.flying)
        {
            AudioManager.Instance.playVFX(audioData2);
            other.GetComponent<EnemyBase>().Hurt(PlayerController.GetInstance().pAtk, ValType.physics);
        }    
        else if(other.tag == "Wall" && state == DiceState.flying)
        {
            StopDice();
        }
        else if(other.name == "Player" && state == DiceState.ground)
        {
            PickDice();
        }
    }

    //添加骰面技能
    public void ChangeDiceFace(DiceFaceSkill skill, int id)
    {
        if(diceFace[id] == null)
        {
            diceFace[id] = skill;
        }
    }

    //获取骰面信息
    public DiceFaceSkill GetDiceFace(int id)
    {
        if(diceFace[id] != null)
        {
            return diceFace[id];
        } 
        return null;
    }

    //移除骰面技能
    public void RemoveDiceFace(int id)
    {
        diceFace[id] = null;
    }

    //根据骰面index移除技能
    public void RemoveDiceFaceByIndex(int index)
    {
        for(int i = 0;i < 6;i++)
        {
            if(diceFace[i] != null && diceFace[i].index == index)
            {
                diceFace[i] = null;
            }
        }
    }

    //获取技能数量
    public int GetDiceFaceNum()
    {
        int num = 0;
        for(int i = 0;i < 6;i++)
        {
            if(diceFace[i] != null)
            {
                num++;
            }
        }
        return num;
    }

    //捡起骰子
    public void PickDice()
    {
        canAutoPick = false;
        Destroy(rb);
        state = DiceState.hold;
        this.transform.SetParent(weaponPos);
        this.transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void AutoPick()
    {
        Vector2 direction2 = (PlayerController.GetInstance().gameObject.transform.position - this.gameObject.transform.position).normalized;
        rb.velocity = direction2 * speed;
    }

    IEnumerator UpdatePick()
    {
        yield return new WaitForSeconds(3);
        canAutoPick = true;
        yield break;
    }
}

/// <summary>
/// 骰子状态
/// </summary>
public enum DiceState
{
    hold,//在玩家手中
    flying,//飞行途中
    ground,//在地面上
}