using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject shop;

    GameObject mStopPanel;
    AudioData openShopAudio;

    private void Start()
    {
        shop = transform.Find("Shop").gameObject;

        mStopPanel = transform.Find("StopPanel").gameObject;

        openShopAudio = AudioManager.Instance.getAudioData(AudioName.打开菜单商店);
        
        if(shop != null)
        {
            Debug.Log("Shop");
        }

        //closeShopAudio = AllAudioData.Instance.getAudioData(AudioName.�رղ˵��̵�);
    }
    // Update is called once per frame
    void Update()
    {
        if(shop.activeInHierarchy == false && Input.GetMouseButtonDown(1))
        {
            shop.SetActive(true);

            PlayerController.GetInstance().canInput = false;

            AudioManager.Instance.playVFX(openShopAudio);

            Time.timeScale = 0;
        }
        if(shop.activeInHierarchy == false && Input.GetKeyDown(KeyCode.Escape))
        {
            mStopPanel.SetActive(true);
            
            PlayerController.GetInstance().canInput = false;

            AudioManager.Instance.playVFX(openShopAudio);

            Time.timeScale = 0;
        }
    }

}
