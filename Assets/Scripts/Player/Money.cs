using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Money : MonoBehaviour
{
    Rigidbody2D rb;

    public int val;

    public float speed;

    AudioData audioData;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        audioData = AudioManager.Instance.getAudioData(AudioName.玩家拾取金币);    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Player")
        {
            AudioManager.Instance.playVFX(audioData);
            PlayerController.GetInstance().money += val * PlayerController.GetInstance().moneyMultiple;
            ObjectPool.GetInstance().PushGameObject(this.gameObject);
        }    
    }

    public void AutoPick()
    {
        Vector2 direction = (PlayerController.GetInstance().gameObject.transform.position - this.gameObject.transform.position).normalized;
        rb.velocity = direction * speed;
    }
}
