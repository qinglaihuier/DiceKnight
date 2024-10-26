using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttributeProps : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    int priceNumber;

    int grade;

    Image image;

    TextMeshProUGUI priceText;

    string description;

    RectTransform rectTf;

    AttributeChange attributeChange;

    public void Initialize(AttributePropsData data)
    {
        rectTf = GetComponent<RectTransform>();

        image = transform.Find("Icon").GetComponent<Image>();

        priceText = transform.Find("Price").GetComponent<TextMeshProUGUI>();

        rectTf.sizeDelta = image.GetComponent<RectTransform>().sizeDelta;

        rectTf.localPosition = Vector3.zero; //λ��

        priceNumber = data.price;

        priceText.text = priceNumber.ToString();

        image.sprite = data.sprite;

        description = data.description;

        grade = data.grade;


        attributeChange = PropsSkillManager.Instance.getAttributeChange(data.changeName, data.grade);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            ShopManager.Instance.buyAttributeProps(priceNumber, gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShopManager.Instance.setProductInformationBar(description, priceNumber, 0);
    }
    public void excute()
    {
        attributeChange.changeAttribute();
    }

}
