using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBlackHole : NoHand
{
    public override void Fun()
    {
        AudioManager.Instance.playVFX(audioData);
        panel = SpPropManager.GetInstance().gameObject;
        PlayerController.GetInstance().canPickMoney = true;
        SpPropManager.GetInstance().canGet[2] = false;
        Time.timeScale = 1;
        PlayerController.GetInstance().canInput = true;
        panel.SetActive(false);
    }
}
