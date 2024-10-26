using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill : MonoBehaviour
{
    // Start is called before the first frame update
    int rangeMultipler = 1;

    double damageMultipler = 1;

    int grade;

    Collider2D coll;

    ValType damageType;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();

        damageType = ValType.physics;
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(lifeCoroutine));
    }
    public void Initialize(int rangeMultipler, double damageMultipler, Vector3 pos, int grade)
    {
        transform.localScale = Vector3.one * rangeMultipler;

        this.damageMultipler = damageMultipler;

        transform.position = pos;

        this.grade = grade;
    }
    
    public void preciseSlash()
    {
        Collider2D[] enemys = new Collider2D[1000];
        int count = Physics2D.GetContacts(coll, enemys);

        if (count == 0)
        {
            return;
        }

        switch (grade)
        {
            case 1:
                int index = 0;

                int minHealth = enemys[0].GetComponent<EnemyBase>().currentHealth;

                for (int i = 0; i < enemys.Length; ++i)
                {
                    if (enemys[i] != null)
                    {
                        int currentHealth = enemys[i].GetComponent<EnemyBase>().currentHealth;
                        if (currentHealth < minHealth)
                        {
                            minHealth = currentHealth;

                            index = i;
                        }
                    }
                    
                }

                enemys[index].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 1.5), damageType);
                Debug.Log(grade + " ’∂…± " + enemys[index].gameObject.name + "‘Ï≥……À∫¶ " + ((int)(PlayerController.GetInstance().pAtk * 1.5)).ToString());
                break;
            case 2:
                for (int i = 0; i < enemys.Length; ++i)
                {
                    if (enemys[i] != null)
                    {
                        if (enemys[i].GetComponent<EnemyBase>().JudgeHealth(30))
                        {
                            enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 2), damageType);

                            Debug.Log(grade + " ’∂…± " + enemys[i].gameObject.name + "‘Ï≥……À∫¶ " + ((int)(PlayerController.GetInstance().pAtk * 2)).ToString());
                        }
                    }
                }
                break;
            case 3:
                for (int i = 0; i < enemys.Length; ++i)
                {
                    if (enemys[i] != null)
                    {
                        enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 0.7), damageType);

                        if (enemys[i].GetComponent<EnemyBase>().JudgeHealth(30))
                        {
                            enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 1.8), damageType);

                            Debug.Log(grade + " ’∂…± " + enemys[i].gameObject.name + "‘Ï≥……À∫¶ " + ((int)(PlayerController.GetInstance().pAtk * 1.8)).ToString());
                        }
                    }
                  
                }
                break;
            case 4:
                for (int i = 0; i < enemys.Length; ++i)
                {
                    if(enemys[i] != null)
                    {
                        enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 1.5), damageType);

                        if (enemys[i].GetComponent<EnemyBase>().JudgeHealth(35))
                        {
                            enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 1.5), damageType);

                            Debug.Log(grade + " ’∂…± " + enemys[i].gameObject.name + "‘Ï≥……À∫¶ " + ((int)(PlayerController.GetInstance().pAtk * 1.5)).ToString());
                        }
                    }
                   
                }
                break;
            case 5:
                for (int i = 0; i < enemys.Length; ++i)
                {
                    if (enemys[i] != null)
                    {
                        enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 2), damageType);

                        if (enemys[i].GetComponent<EnemyBase>().JudgeHealth(40))
                        {
                            enemys[i].GetComponent<EnemyBase>().Hurt((int)(PlayerController.GetInstance().pAtk * 2.5), damageType);

                            Debug.Log(grade + " ’∂…± " + enemys[i].gameObject.name + "‘Ï≥……À∫¶ " + ((int)(PlayerController.GetInstance().pAtk * 2.5)).ToString());
                        }
                    }
                  
                }
                break;
        }
   
    }
    IEnumerator lifeCoroutine()
    {
        yield return new WaitForSeconds(1f);

        ObjectPool.GetInstance().PushGameObject(gameObject);
    }
}
