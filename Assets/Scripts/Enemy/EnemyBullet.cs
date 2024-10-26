using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public int val;//攻击伤害

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    public void SetSpeed(Vector2 direction, float speed)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            other.GetComponent<PlayerController>().Hurt(val);
            ObjectPool.GetInstance().PushGameObject(this.gameObject);
        }    
        else if(other.gameObject.name == "Wall")
        {
            ObjectPool.GetInstance().PushGameObject(this.gameObject);
        }
    }
}
