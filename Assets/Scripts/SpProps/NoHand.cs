using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoHand : MonoBehaviour
{
    public GameObject panel;

    protected AudioData audioData;

    protected virtual void OnEnable() 
    {
        audioData = AudioManager.Instance.getAudioData(AudioName.特殊道具选择);
    }

    public virtual void Fun()
    {
        AudioManager.Instance.playVFX(audioData);
        panel = SpPropManager.GetInstance().gameObject;
        PlayerController.GetInstance().canPickDice = true;
        SpPropManager.GetInstance().canGet[0] = false;
        Time.timeScale = 1;
        PlayerController.GetInstance().canInput = true;
        panel.SetActive(false);
    }
}
