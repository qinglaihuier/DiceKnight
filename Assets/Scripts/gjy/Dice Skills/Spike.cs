using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public class Spike : MonoBehaviour
{
    //记得设置layer
    float speed;

    Vector2 direction = Vector2.zero;

    float damageMultiplier;

    float size = 1;

    ValType type;

    Rigidbody2D rb;

    Collider2D coll;
    private void Awake()
    {
        type = ValType.physics;

        rb = GetComponent<Rigidbody2D>();

        coll = GetComponent<Collider2D>();

        rb.gravityScale = 0;

        coll.isTrigger = true;

        Debug.Log("Awake");
    }

    // Start is called before the first frame update
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    public void Initialize(float _speed,  float angle, float _size, float _damageMultiplier,Vector3 pos)
    {
        speed = _speed;

        size = _size;

        damageMultiplier = _damageMultiplier;

        transform.position = pos;

        transform.rotation = Quaternion.Euler(0 , 0, angle);

        rb.velocity = speed * transform.up.normalized;


        Debug.Log("hhhhh");

        transform.localScale = Vector3.one * size;

        StartCoroutine(nameof(lifeCycleCoroutine));
    }
    IEnumerator lifeCycleCoroutine()
    {
        float interval = 0.02f;

        WaitForSeconds wait = new WaitForSeconds(interval);

     
        while(CameraManager.GetInstance().IsPositionInsideCamera(transform.position))
        {
            yield return wait;
        }
       
        ObjectPool.GetInstance().PushGameObject(gameObject);

     
        //关闭对象 返回对象池
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<EnemyBase>().Hurt((int)(damageMultiplier * PlayerController.GetInstance().pAtk), type);
    }

}
