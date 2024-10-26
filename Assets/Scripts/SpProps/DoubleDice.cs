using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDice : NoHand
{
    public GameObject dice;

    public Transform weaponPos;

    public override void Fun()
    {
        AudioManager.Instance.playVFX(audioData);
        panel = SpPropManager.GetInstance().gameObject;
        weaponPos = GameObject.Find("WeaponPos").transform;
        GameObject obj = Instantiate(dice);
        PlayerController.GetInstance().dices.Add(obj.GetComponent<DiceController>());
        obj.transform.SetParent(weaponPos);
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
        SpPropManager.GetInstance().canGet[1] = false;
        Time.timeScale = 1;
        PlayerController.GetInstance().canInput = true;
        panel.SetActive(false);
    }
}
