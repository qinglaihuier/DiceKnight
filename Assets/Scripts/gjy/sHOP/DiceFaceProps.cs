using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DiceFaceProps : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler
{
    DiceFacePropsData data;
    public DiceFacePropsData Data
    {
        get { return data; }
    }

    int priceNumber = 0;

    int salePriceNumber = 0;

    public DiceFacePropsName propsName;

    [Range(1, 6)] int grade; //Æ·½×

    string description;
       
    DiceFaceSkill skill;

    RectTransform rectTf;

    Image image;

    TextMeshProUGUI priceText; 
    public void Initialize(DiceFacePropsData _data) 
    {
        rectTf = GetComponent<RectTransform>();

        image = transform.Find("Icon").GetComponent<Image>();

        priceText = transform.Find("Price").GetComponent<TextMeshProUGUI>();

        rectTf.sizeDelta = image.GetComponent<RectTransform>().sizeDelta;

        rectTf.localPosition = Vector3.zero; //Î»ÖÃ

        priceNumber = _data.price;

        salePriceNumber = _data.salePrice;

        priceText.text = priceNumber.ToString();

        image.sprite = _data.sprite;

        description = _data.description;

        grade = _data.grade;

        skill = PropsSkillManager.Instance.getDiceSkill(_data.skillName, grade, _data);

        propsName = _data.propsName;

        data = _data;
    }
     public void OnPointerClick(PointerEventData eventData)
     {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShopManager.Instance.buyDiceFaceProps(priceNumber, gameObject, skill, propsName);
        }
     }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopManager.Instance.setProductInformationBar(description, priceNumber, salePriceNumber);
    }
}
