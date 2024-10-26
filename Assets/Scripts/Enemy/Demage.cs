using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Demage : MonoBehaviour
{
    [SerializeField] Text damageNum;//伤害数值

    public void Init(string num)
    {
        damageNum.text = num;
        damageNum.color = new Color(damageNum.color.r, damageNum.color.g, damageNum.color.b, 1);
        damageNum.DOFade(0, 2);
    }

    void Update()
    {
        if (damageNum.color.a == 0)
        {
            ObjectPool.GetInstance().PushGameObject(gameObject);
        }
    }
}
