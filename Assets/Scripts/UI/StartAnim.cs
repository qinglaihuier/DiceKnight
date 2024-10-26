using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAnim : MonoBehaviour
{
    Image image;

    public Sprite[] sprites; 

    private void Start() 
    {
        image = GetComponent<Image>();    
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        int i = 0;
        while(true)
        {
            image.sprite = sprites[i];
            if(i < sprites.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
