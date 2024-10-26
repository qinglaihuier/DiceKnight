using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance { get; private set; }
    public Image mask;

    public Text text;

    float longth;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        longth = mask.rectTransform.rect.width;
    }

    private void Update() 
    {
        longthChange(PlayerController.GetInstance().currentHealth / (float)PlayerController.GetInstance().maxHealth);
        text.text = PlayerController.GetInstance().currentHealth + "/" + PlayerController.GetInstance().maxHealth;      
    }

    public void longthChange(float health)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, longth * health); 
    }
}
