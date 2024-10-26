using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : NoHand
{
    public override void Fun()
    {
        AudioManager.Instance.playVFX(audioData);
        panel = SpPropManager.GetInstance().gameObject;
        PlayerController.GetInstance().canRevive = true;
        Time.timeScale = 1;
        PlayerController.GetInstance().canInput = true;
        panel.SetActive(false);
    }
}
