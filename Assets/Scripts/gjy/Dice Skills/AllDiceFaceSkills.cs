
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplosionInPlace : DiceFaceSkill
{
    int rangeMultiplier = 3;

    double damageMultiplier = 1;

    ValType damageType;

    int rangeSize = 0;

    GameObject booom;
    public ExplosionInPlace(int grade , DiceFacePropsData data)
        :base(data)
    {
        damageType = ValType.physics;

        rangeSize = PropsSkillManager.Instance.rangeSize;

        sprite = data.sprite;

        index = data.index;

        salePrice = data.salePrice;

        booom = Resources.Load<GameObject>("Prefab/Booom");

        audioData = AudioManager.Instance.getAudioData(AudioName.骰子爆炸);
        
        switch (grade)
        {
            case 1:
                rangeMultiplier = 3;

                damageMultiplier = 0.4;
                break;
            case 2:
                rangeMultiplier = 3;

                damageMultiplier = 0.8;
                break;
            case 3:
                rangeMultiplier = 4;

                damageMultiplier = 1.2;
                break;
            case 4:
                rangeMultiplier = 4;

                damageMultiplier = 1.6;
                break;
            case 5:
                rangeMultiplier = 5;

                damageMultiplier = 2;
                break;
        }
    }
    public override void excute(DiceController dice)
    {
        //����
        Debug.Log("excute");

        AudioManager.Instance.playVFX(audioData);

        ObjectPool.GetInstance().GetGameObject(booom).GetComponent<Booom>().Initialize(rangeMultiplier, damageMultiplier, dice.transform.position);

        //var enemys = Physics2D.OverlapCircleAll(dice.transform.position, rangeSize * rangeMultiplier, LayerMask.GetMask("Enemy"));

        //foreach(var enemy in enemys)
        //{
        //    int hurt =(int)(PlayerController.GetInstance().pAtk * damageMultiplier);
        //    Debug.Log("gongji!!!");
        //    enemy.GetComponent<EnemyBase>().Hurt(hurt, damageType);
        //}
    }
}
public class PreciseSlash : DiceFaceSkill
{
    int grade;

    int rangeMultiplier = 3;

    ValType damageType;

    GameObject slash;

    public PreciseSlash(int grade, DiceFacePropsData data)
        : base(data)
    {
        damageType = ValType.physics;

        sprite = data.sprite;

        index = data.index;

        salePrice = data.salePrice;

        slash = Resources.Load<GameObject>("Prefab/Slash");

        audioData = AudioManager.Instance.getAudioData(AudioName.补刀骰子);

        switch (grade)
        {
            case 1:
                rangeMultiplier = 2;

                break;
            case 2:
                rangeMultiplier = 2;

                break;
            case 3:
                rangeMultiplier = 2;

                break;
            case 4:
                rangeMultiplier = 2;

                break;
            case 5:
                rangeMultiplier = 3;

                break;
        }

        this.grade = grade;

    }
    public override void excute(DiceController dice)
    {
        damageType = ValType.physics;

        //����
        GameObject g = ObjectPool.GetInstance().GetGameObject(slash);
        
        g.GetComponent<SlashSkill>().Initialize(rangeMultiplier, 1, dice.transform.position, grade);

        g.GetComponent<SlashSkill>().preciseSlash();

        AudioManager.Instance.playVFX(audioData);
    }
}
public class StunBlow : DiceFaceSkill
{
    int stunTime = 1;

    double damageMultiplier = 0;

    int rangeMultiplier = 1;

    int rangeSize = 1;

    GameObject stun;

    ValType type;
    public StunBlow(int grade, DiceFacePropsData data)
        : base( data)
    {
        rangeSize = PropsSkillManager.Instance.rangeSize;

        type = ValType.physics;

        sprite = data.sprite;

        index = data.index;

        salePrice = data.salePrice;

        audioData = AudioManager.Instance.getAudioData(AudioName.骰子投掷);

        stun = Resources.Load<GameObject>("Prefab/Stun");

        switch (grade)
        {
            case 1:
                rangeMultiplier = 1;

                stunTime = 1;

                damageMultiplier = 0;
                break; 
            case 2:
                rangeMultiplier = 1;

                damageMultiplier = 0;

                stunTime = 1;
                break;
            case 3:
                rangeMultiplier = 2;

                stunTime = 1;

                damageMultiplier = 1;
                break;
            case 4:
                rangeMultiplier = 3;

                stunTime = 2;

                damageMultiplier = 1.5;


                break;
            case 5:
                rangeMultiplier = 3;

                stunTime = 3;

                damageMultiplier = 2;
                break;
        }
    }
    public override void excute(DiceController dice)
    {
        Debug.Log("excute stun");

        AudioManager.Instance.playVFX(audioData);

        StunSkill s = ObjectPool.GetInstance().GetGameObject(stun).GetComponent<StunSkill>();

        s.Initialie(stunTime, damageMultiplier, rangeMultiplier, dice.transform.position);

        s.excute();

        //var enemys = Physics2D.OverlapCircleAll(dice.transform.position, rangeSize * rangeMultiplier, LayerMask.GetMask("Enemy"));

        //for(int i = 0; i < enemys.Length; ++i)
        //{
        //    enemys[i].GetComponent<EnemyBase>().DoVertigo(stunTime);

        //    enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * damageMultiplier), type);
        //}
    }
}
public class ShootSpikes : DiceFaceSkill
{
    float size = 1;

    float speedMultiplier = 1;

    float damageMultiplier = 1;

    float angleInterval = 0;

    int grade;

    GameObject prefab;
    public ShootSpikes(int grade, DiceFacePropsData data)
        : base(data)
    {
        sprite = data.sprite;

        index = data.index;

        salePrice = data.salePrice;

        this.grade = grade;

        prefab = Resources.Load<GameObject>("Prefab/Spike");

        audioData = AudioManager.Instance.getAudioData(AudioName.尖刺音效);

        switch(grade)
        {
            case 1:
                speedMultiplier = 6;

                angleInterval = 90;

                damageMultiplier = 1;

                size = 1;
                break;
            case 2:
                speedMultiplier = 6;

                angleInterval = 360 / 8;

                damageMultiplier = 1;

                size = 1;
                break;
            case 3:
                speedMultiplier = 5;

                angleInterval = 360 / 12;

                damageMultiplier = 1.5f;

                size = 1;
                break;
            case 4:
                speedMultiplier = 4;

                angleInterval = 360 / 12;

                damageMultiplier = 1.2f;

                size = 1;
                break;
            case 5:
                speedMultiplier = 3;

                angleInterval = 360 / 12;

                damageMultiplier = 1.5f;

                size = 1;
                break;
        }

    }
    public override void excute(DiceController dice)
    {
        AudioManager.Instance.playVFX(audioData);
        if (grade < 4)
        {
            float angle = 0;

            Vector2 direction = new Vector2(1, 0);

            Quaternion q = Quaternion.Euler(0, 0, angleInterval);



            while (angle < 360f)
            {
                GameObject spike = ObjectPool.GetInstance().GetGameObject(prefab);

                spike.GetComponent<Spike>().Initialize(speedMultiplier * PropsSkillManager.Instance.speedSize
                                                     , angle
                                                     , size
                                                     , damageMultiplier
                                                     , dice.transform.position
                                                     );

                angle += angleInterval;

                Debug.Log(angle);
            }
        }
        else
        {
            float angle = 0;

            Vector2 direction = new Vector2(1, 0);

            Quaternion q = Quaternion.Euler(0, 0, angleInterval);

            while (angle < 360)
            {
                GameObject spike = ObjectPool.GetInstance().GetGameObject(prefab);

                spike.GetComponent<Spike>().Initialize(
                                                       speedMultiplier * PropsSkillManager.Instance.speedSize
                                                     , angle
                                                     , size
                                                     , damageMultiplier
                                                     , dice.transform.position
                                                     );

                spike.GetComponent<Spike>().Initialize(
                                                     speedMultiplier * PropsSkillManager.Instance.speedSize
                                                   , angle
                                                   , size
                                                   , damageMultiplier
                                                   , dice.transform.position
                                                   );
                angle += angleInterval;

                direction = q * direction;
            }
        }
    }
}
public class HolySkill : DiceFaceSkill
{
    int rangeMultiplier = 1;

    double damageMultiplier = 1;

    ValType type;

    GameObject holy;

    public HolySkill(int grade, DiceFacePropsData data)
        :base(data)
    {
        type = ValType.magic;

        sprite = data.sprite;

        salePrice = data.salePrice;

        index = data.index;

        audioData = AudioManager.Instance.getAudioData(AudioName.圣光骰子);

        holy = Resources.Load<GameObject>("Prefab/Holy");

        switch(grade)
        {
            case 1:
                rangeMultiplier = 1;

                damageMultiplier = 1;

                break;
            case 2:
                rangeMultiplier = 2;

                damageMultiplier = 1;   

                break;
            case 3:
                rangeMultiplier = 3;

                damageMultiplier = 1.5;

                break;
            case 4:
                rangeMultiplier = 5;

                damageMultiplier = 1.8;
                break;
            case 5:
                rangeMultiplier = 7;

                damageMultiplier = 2;
                break;
        }
    }
    public override void excute(DiceController dice)
    {
        //var enemys = Physics2D.OverlapCircleAll(
        //                                      dice.transform.position
        //                                     , rangeMultiplier * PropsSkillManager.Instance.rangeSize
        //                                     , LayerMask.GetMask("Enemy"));

        //ObjectPool.GetInstance().GetGameObject(range).GetComponent<Range>().Initialize(rangeMultiplier * PropsSkillManager.Instance.rangeSize * rangeMultiplier, Color.red, dice.transform.position);

        //AudioManager.Instance.playVFX(audioData);

        //foreach ( var enemy in enemys )
        //{
        //    //���� ��Ч�ȵ�
        //    enemy.GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().mAtk * damageMultiplier), type);
        //}

        ObjectPool.GetInstance().GetGameObject(holy).GetComponent<Holy>().Initialize(rangeMultiplier, damageMultiplier, dice.transform.position);
    }
}

public class AuraSkill : DiceFaceSkill
{
    float damageMultilper = 1;

    int rangeMultiplier = 1;

    int lifeCycleSize = 1;

    GameObject aura;
    public AuraSkill(int grade, DiceFacePropsData data)
        :base(data)
    {
        sprite = data.sprite;

        salePrice = data.salePrice;

        index = data.index;

        aura = Resources.Load<GameObject>("Prefab/Aura");
        if(aura == null)
        {
            Debug.LogError("δ�ɹ�������ʹ�Ĺ⻷Ԥ����");
        }

        audioData = AudioManager.Instance.getAudioData(AudioName.天使光环骰);

        switch(grade)
        {
            case 1:
                rangeMultiplier = 1;

                damageMultilper = 0.3f;

                lifeCycleSize = 3;
                break;
            case 2:
                rangeMultiplier = 1;

                damageMultilper = 0.7f;

                lifeCycleSize = 5;
                break;
            case 3:
                rangeMultiplier = 2;

                damageMultilper = 0.7f;

                lifeCycleSize = 10;
                break;
            case 4:
                rangeMultiplier = 2;

                damageMultilper = 1f;

                lifeCycleSize = 20;
                break;
            case 5:
                rangeMultiplier = 3;

                damageMultilper = 1f;

                lifeCycleSize = 30;
                break;
        }
    }
    public override void excute(DiceController dice)
    {
        AudioManager.Instance.playVFX(audioData);

        //sObjectPool.GetInstance().GetGameObject(range).GetComponent<Range>().Initialize(rangeMultiplier * PropsSkillManager.Instance.rangeSize * rangeMultiplier, Color.yellow, dice.transform.position, (int)lifeCycleSize);

        ObjectPool.GetInstance().GetGameObject(aura).GetComponent<Aura>().Initialize(damageMultilper, rangeMultiplier, lifeCycleSize, PlayerController.GetInstance().transform); 
    }
}
public class AngelShadowSkill : DiceFaceSkill
{
    int lifeCycleSize = 1;

    float damageMultiplier = 1;

    int rangeMultiplier = 1;

    int grade = 0;

    GameObject angelShadow;

    GameObject aura;

    public AngelShadowSkill(int grade, DiceFacePropsData data)
        : base( data)
    {
        this.grade = grade;

        angelShadow = Resources.Load<GameObject>("Prefab/AngelShadow");

        aura = Resources.Load<GameObject>("Prefab/Aura");

        sprite = data.sprite;

        salePrice = data.salePrice;

        audioData = AudioManager.Instance.getAudioData(AudioName.天使虚影骰面);

        index = data.index;

        switch (grade)
        {
            case 1:
                lifeCycleSize = 5;

                damageMultiplier = 0;

                rangeMultiplier = 0;

                break;
            case 2:
                lifeCycleSize = 5;

                damageMultiplier = 0.3f;

                rangeMultiplier = 1;

                break;
            case 3:
                lifeCycleSize = 7;

                damageMultiplier = 0.5f;

                rangeMultiplier = 1;

                break;
            case 4:
                lifeCycleSize = 10;

                damageMultiplier = 0.5f;

                rangeMultiplier = 2;

                break;
            case 5:
                lifeCycleSize = 10;

                damageMultiplier = 1;

                rangeMultiplier = 2;

                break;
        }
    }
    public override void excute(DiceController dice)
    {
        AudioManager.Instance.playVFX(audioData);
        if(grade == 1)
        {
            ObjectPool.GetInstance().GetGameObject(angelShadow).GetComponent<AngelShadow>()
                            .Initialize(lifeCycleSize, dice.transform.position);
        }
        else
        {
            ObjectPool.GetInstance().GetGameObject(angelShadow).GetComponent<AngelShadow>()
                          .Initialize(lifeCycleSize, dice.transform.position);

            ObjectPool.GetInstance().GetGameObject(aura).GetComponent<Aura>().Initialize(damageMultiplier, rangeMultiplier, lifeCycleSize);

            ObjectPool.GetInstance().GetGameObject(range).GetComponent<Range>().Initialize(rangeMultiplier * PropsSkillManager.Instance.rangeSize * rangeMultiplier, Color.yellow, dice.transform.position, (int)lifeCycleSize);

        }
    }
}
public class LifePotion : DiceFaceSkill
{
    float DosisCurativa;
    public LifePotion(int grade, DiceFacePropsData data)
        :base(data)
    {
        sprite = data.sprite;

        salePrice = data.salePrice;

        index = data.index;

        audioData = AudioManager.Instance.getAudioData(AudioName.生命药水骰);

        switch (grade)
        {
            case 1:
                DosisCurativa = 10;
                break;
            case 2:
                DosisCurativa = 15;
                break;
            case 3:
                DosisCurativa = 20;
                break;
            case 4:
                DosisCurativa = 25;
                break;
            case 5:
                DosisCurativa = 30;
                break;
        }
    }
    public override void excute(DiceController dice)
    {
        AudioManager.Instance.playVFX(audioData);

        int newHealth = PlayerController.GetInstance().currentHealth + (int)(PlayerController.GetInstance().maxHealth * DosisCurativa * 0.01f);

        if(newHealth > PlayerController.GetInstance().maxHealth)
        {
            newHealth = PlayerController.GetInstance().maxHealth;
        }
        PlayerController.GetInstance().currentHealth = newHealth;
    }
}
public class NanoKill : DiceFaceSkill
{
    int rangeMultiplier = 5;

    GameObject nano;
    public NanoKill(int grade, DiceFacePropsData data)
        :base(data)
    {
        audioData = AudioManager.Instance.getAudioData(AudioName.斩杀);

        sprite = data.sprite;

        salePrice = data.salePrice;

        index = data.index;

        nano = Resources.Load<GameObject>("Prefab/Nano");
    }
    public override void excute(DiceController dice)
    {
        Debug.Log("NanoKill Excute");

        AudioManager.Instance.playVFX(audioData);
      
        GameObject n = ObjectPool.GetInstance().GetGameObject(nano);

        n.GetComponent<Nano>().Initialie(rangeMultiplier, dice.transform.position);
        n.GetComponent<Nano>().excute();
       
    }
}
public class DoubleTheGold : DiceFaceSkill
{
    float time = 15;

    int multiplier = 2;

    public DoubleTheGold(int grade, DiceFacePropsData data)
        :base(data)
    {
        audioData = AudioManager.Instance.getAudioData(AudioName.钱来);

        sprite = data.sprite;

        salePrice = data.salePrice;

        index = data.index;
    }
    public override void excute(DiceController dice)
    {
        AudioManager.Instance.playVFX(audioData);

        PlayerController.GetInstance().DoDoubleMoney(time, multiplier);
    }
}