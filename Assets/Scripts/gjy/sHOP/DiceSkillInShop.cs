using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DiceSkillInShop : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler
{
    public DiceFacePropsName propsName;

    const int width = 80;

    const int height = 80;
    // Start is called before the first frame update
    Sprite _sprite;

    int salePrice;

    int buyPrice;
    public int SalePrice { get { return salePrice; } }

    int index;

    public int Index { get { return index; } }

    string description;

    public void Initialize(DiceFacePropsData data)
    {
        gameObject.AddComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        gameObject.AddComponent<Image>().sprite = data.sprite;

        salePrice = data.salePrice;

        buyPrice = data.price;

        index = data.index;

        description = data.description;

        propsName = data.propsName;
    }
    public void Initialize(DiceFaceSkill skill)
    {
        gameObject.AddComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        gameObject.AddComponent<Image>().sprite = skill.sprite;

        salePrice = skill.salePrice;

        index = skill.index;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        ShopManager.Instance.sellDiceSkill(this);

        //ShopManager.Instance.getMoney(salePrice);

            //Íæ¼Ò

       // Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        ShopManager.Instance.setProductInformationBar(description, buyPrice,salePrice);
    }
}
