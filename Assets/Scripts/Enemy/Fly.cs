using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : EnemyBase
{
    protected override void OnEnable() 
    {
        rb = GetComponent<Rigidbody2D>();
        health = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 16) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) * 24 + n));
        atk = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave + 1, 4) * (0.9 + 0.1 * EnemyManager.GetInstance().wave) + n));
        pDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.2 * EnemyManager.GetInstance().wave) + n));
        mDef = (int)Mathf.Round((float)(Mathf.Log(EnemyManager.GetInstance().wave, 4) * (0.8 + 0.2 * EnemyManager.GetInstance().wave) + n));
        dropMoney = (int)(1 * (EnemyManager.GetInstance().wave / 6 + 1));
        currentHealth = health;
        audioData = AudioManager.Instance.getAudioData(AudioName.飞行普通恶魔死亡);
    }
}
