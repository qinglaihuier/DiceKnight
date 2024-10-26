using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager :MonoBehaviour
{
    DiceController diceController;

    static ShopManager instance;

    public static ShopManager Instance { get { return instance; } }

    RefreshType[] refreshType = new RefreshType[3];

    float[] gradeProbability = new float[5];

    public PropsWarehouse propsWarehouse;

    public List<DiceFacePropsName> diceFacePropsList = new List<DiceFacePropsName>();

    public List<AttributePropsName> attributePropsList = new List<AttributePropsName>();

    public GameObject productPrototype; //��Ʒԭ��

    Dictionary<int, List<DiceFacePropsName>> diceFacePropsDictionary = new Dictionary<int, List<DiceFacePropsName>>();

    Dictionary<int, List<AttributePropsName>> attributePropsDictionary = new Dictionary<int, List<AttributePropsName>>();

    Dictionary<DiceFacePropsName, int> diceFacePropsPurchaseNumber = new Dictionary<DiceFacePropsName, int>();

    GameObject shoppingBoardGObj;

    GameObject informationBarGObj;

    GameObject diceSidesBarGObj;

    Button returnButton;

    Button refreshButton;

    Button nextDiceButton;

    Button lastDiceButton;

    TextMeshProUGUI balanceCountText;

    TextMeshProUGUI buyingPriceText;

    TextMeshProUGUI sellingPriceText;

    TextMeshProUGUI productDescriptionText;

    TextMeshProUGUI refreshPriceText;

    AudioData closeShopAudio;

    AudioData bugAndSellAudio;

    public int balanceCount = 0;

    int waveNumber = -1;

    public int refreshPrice = 0;

    string productDescription;

    bool freeRefresh = false;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            DestroyImmediate(this);

            Debug.LogWarning("�ж�� shopmanager������: " + gameObject.name);

            return;
        }

        shoppingBoardGObj = transform.Find("Shopping Board").gameObject;

        informationBarGObj = transform.Find("Information Bar").gameObject;

        diceSidesBarGObj = transform.Find("Dice Sides Bar").gameObject;

        returnButton = transform.Find("Return Button").GetComponent<Button>();

        refreshButton = transform.Find("Refresh Button").GetComponent<Button>();

        nextDiceButton = diceSidesBarGObj.transform.Find("Next Dice Button").GetComponent<Button>();

        lastDiceButton = diceSidesBarGObj.transform.Find("Last Dice Button").GetComponent<Button>();

        balanceCountText = transform.Find("Balance").GetChild(0).GetComponent<TextMeshProUGUI>();

        productDescriptionText = informationBarGObj.transform.Find("Text Box").GetChild(0).GetComponent<TextMeshProUGUI>();

        buyingPriceText = informationBarGObj.transform.Find("Buy Price").GetChild(0).GetComponent<TextMeshProUGUI>();

        sellingPriceText = informationBarGObj.transform.Find("Sell Price").GetChild(0).GetComponent<TextMeshProUGUI>();

        refreshPriceText = refreshButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        refreshPriceText.text = refreshPrice.ToString();

        nextDiceButton.onClick.AddListener(nextDice);

        lastDiceButton.onClick.AddListener(lastDice);

        refreshButton.onClick.AddListener(refreshAllProduct);

        returnButton.onClick.AddListener(returnGame);

        //balanceCountText.text = balanceCount.ToString();    

        refreshStoreInventory();
    }
    private void OnEnable()
    {
        updateAttribute();
    }
    private void Start()
    {
        ShopStart();
    }
    void refreshStoreInventory()
    {
        diceFacePropsDictionary.Clear();

        attributePropsDictionary.Clear();

        foreach (var v in diceFacePropsList)
        {
            int k = propsWarehouse.getDiceFacePropsGrade(v);
            if (diceFacePropsDictionary.ContainsKey(k) == false)
            {
                diceFacePropsDictionary.Add(k, new List<DiceFacePropsName>());
            }
            diceFacePropsDictionary[k].Add(v);
        }
        foreach (var v in attributePropsList)
        {
            int k = propsWarehouse.getAttributePropsGrade(v);
            if (attributePropsDictionary.ContainsKey(k) == false)
            {
                attributePropsDictionary.Add(k, new List<AttributePropsName>());
            }
            attributePropsDictionary[k].Add(v);
        }
    }
    void setFreeRefresh()
    {
        if (this.waveNumber == EnemyManager.GetInstance().wave) return;
        freeRefresh = true;

        refreshPriceText.text = "免费";
    }
    public void buyDiceFaceProps(int price, GameObject product, DiceFaceSkill skill, DiceFacePropsName name)
    {
        if (price > balanceCount)
        {
            setProductInformationBar("余额不足", 0, 0);

            return;
        }
        if(diceController.GetDiceFaceNum() >= 6)
        {

            //����
            setProductInformationBar("没有足够骰面位置", 0, 0);

            return;
        }

        lessMoney(price); //��Ҽ���
        if (diceFacePropsPurchaseNumber.ContainsKey(name) == false)
        {
            diceFacePropsPurchaseNumber[name] = 0;
        }

        diceFacePropsPurchaseNumber[name] = diceFacePropsPurchaseNumber[name] + 1;

        if (diceFacePropsPurchaseNumber[name] ==2)
        {
            removePurchasableProduct(name);
        }
        Transform parent = product.transform.parent;

        for(int i = 0; i < 6; ++i)
        {
            if(diceController.GetDiceFace(i) == null)
            {
                diceController.ChangeDiceFace(skill, i);

                break;
            }
        }

        GameObject g = new GameObject();   //���Ӽ������UI����
        for(int i = 0; i < 6; ++i)
        {
            if(diceSidesBarGObj.transform.GetChild(i).childCount == 0)
            {
                g.transform.parent = diceSidesBarGObj.transform.GetChild(i);
                g.transform.localPosition = Vector3.zero;
                
                break;
            }
        }

        g.AddComponent<DiceSkillInShop>().Initialize(product.GetComponent<DiceFaceProps>().Data);

        removeProps(product);

        AudioManager.Instance.playVFX(bugAndSellAudio);

        int random = Random.Range(0, 2);

        if (random == 0)//ˢ���µ���Ʒ
        {
            refreshProps(RefreshType.DieFaceProps, parent);//ˢ���µ���Ʒ
        }
        else
        {
            refreshProps(RefreshType.AttributeProps, parent);//ˢ���µ���Ʒ
        }
    }
    public void buyAttributeProps(int price, GameObject product)
    {
        if (price > balanceCount)
        {
            return;
        }
        Transform parent = product.transform.parent;

        lessMoney(price);

        removeProps(product);

        AudioManager.Instance.playVFX(bugAndSellAudio);

        int random = Random.Range(0, 2);

        if(random == 0)
        {
            refreshProps(RefreshType.DieFaceProps, parent);
        }
        else
        {
            refreshProps(RefreshType.AttributeProps, parent);
        }

        product.GetComponent<AttributeProps>().excute(); 
    }
    void removeProps(GameObject g)
    {
        Destroy(g);
    }
    void refreshProps(RefreshType type, Transform parent)
    {
        float random = Random.Range(0, 100);

        float[] probability = new float[5];

        for (int i = 0; i < gradeProbability.Length; ++i)
        {
            probability[i] = gradeProbability[i] * 100f;
        }
        switch (type)
        {
            case RefreshType.DieFaceProps:

                if (random < probability[0])
                {
                    //refresh grade 1
                    getRandomDiceFaceProps(1, parent);
                }
                else if (random >= probability[0] && random < probability[1] + probability[0])
                {
                    //refresh grade 2
                    getRandomDiceFaceProps(2, parent);
                }
                else if (random >= probability[1] + probability[0] && random < probability[1] + probability[0] + probability[2])
                {
                    //refresh grade 3
                    getRandomDiceFaceProps(3, parent);
                }
                else if (random >= probability[1] + probability[0] + probability[2] && random < probability[1] + probability[0] + probability[2] + probability[3])
                {
                    //refresh grade 4
                    getRandomDiceFaceProps(4, parent);
                }
                else
                {
                    //refresh grade 5
                    getRandomDiceFaceProps(5, parent);
                }
                break;
            case RefreshType.AttributeProps:
                if (random < probability[0])
                {
                    //refresh grade 1
                    getRandomAttributeProps(1, parent);

                }
                else if (random >= probability[0] && random < probability[1])
                {
                    //refresh grade 2
                    getRandomAttributeProps(2, parent);
                }
                else if (random >= probability[1] && random < probability[2])
                {
                    //refresh grade 3
                    getRandomAttributeProps(3, parent);
                }
                else if (random >= probability[2] && random < probability[3])
                {
                    //refresh grade 4
                    getRandomAttributeProps(4, parent);
                }
                else
                {
                    //refresh grade 5
                    getRandomAttributeProps(5, parent);
                }
                break;
            case RefreshType.Random:
                if (random < probability[0])
                {
                    getRandomProps(1, parent);
                    //refresh grade 1
                }
                else if (random >= probability[0] && random < probability[1])
                {
                    getRandomProps(2, parent);
                    //refresh grade 2
                }
                else if (random >= probability[1] && random < probability[2])
                {
                    getRandomProps(3, parent);
                    //refresh grade 3
                }
                else if (random >= probability[2] && random < probability[3])
                {
                    getRandomProps(4, parent);
                    //refresh grade 4
                }
                else
                {
                    getRandomProps(5, parent); 
                    //refresh grade 5
                }
                break;
        }
    }
    public void adjustGradeProbility()
    {
        if (this.waveNumber == EnemyManager.GetInstance().wave) return;

        int waveNumber = EnemyManager.GetInstance().wave;

        gradeProbability[0] = Mathf.Max(0.04f, (0.656f - (0.056f * waveNumber)));

        gradeProbability[1] = Mathf.Max(0.06f, (0.258f - (0.018f * waveNumber)));

        gradeProbability[2] = Mathf.Min(0.2f, (0.09f + waveNumber * 0.01f));

        gradeProbability[3] = Mathf.Min(0.4f, (0.4f + 3.6f * waveNumber) * 0.01f);

        gradeProbability[4] = Mathf.Min(0.3f, (0.2f + 2.8f * waveNumber) * 0.01f);

        
    }
    public void getMoney(int count)
    {
        balanceCount += count;

        balanceCountText.text = balanceCount.ToString();   
    }
    public void lessMoney(int count)
    {
        balanceCount -= count;

        balanceCountText.text = balanceCount.ToString();
    }

    public void setMoney()
    {
        balanceCount = PlayerController.GetInstance().money;

        balanceCountText.text = balanceCount.ToString();
    }

    public void setProductInformationBar(string description, int buyPrice, int salePrice)
    {
        productDescriptionText.text = description;

        buyingPriceText.text = buyPrice.ToString();

        sellingPriceText.text = salePrice.ToString();
    }

    public void setProductDescription(string s)
    {
        productDescriptionText.text = s;
    }
    public void sellDiceSkill(DiceSkillInShop skill)
    {
        getMoney(skill.SalePrice);

        diceController.RemoveDiceFaceByIndex(skill.Index);

        diceFacePropsPurchaseNumber[skill.propsName] -= 1;

        if(diceFacePropsPurchaseNumber[skill.propsName] == 1)
        {
            addPurchasableProduct(skill.propsName);
        }

        AudioManager.Instance.playVFX(bugAndSellAudio);

        Destroy(skill.gameObject);
    }
    void addPurchasableProduct(DiceFacePropsName name)
    {
        diceFacePropsList.Add(name);

        refreshStoreInventory();
    }
    void removePurchasableProduct(DiceFacePropsName name)
    {
        diceFacePropsList.Remove(name);

        refreshStoreInventory();
    }
    void getRandomDiceFaceProps(int grade, Transform parent)
    {
        int i = Random.Range(0, diceFacePropsDictionary[grade].Count);

        DiceFacePropsData data = propsWarehouse.getDiceFacePropsData(diceFacePropsDictionary[grade][i]);

        GameObject newProduct = Instantiate(productPrototype, parent);

        newProduct.AddComponent<DiceFaceProps>().Initialize(data);
    }
    void getRandomAttributeProps(int grade, Transform parent)
    {
        int i = Random.Range(0, attributePropsDictionary[grade].Count);

        AttributePropsData data = propsWarehouse.getAttributePropsData(attributePropsDictionary[grade][i]);

        GameObject newProduct = Instantiate(productPrototype, parent);

        newProduct.AddComponent<AttributeProps>().Initialize(data);
    }
    void getRandomProps(int grade, Transform parent)
    {
        int i = Random.Range(0, diceFacePropsDictionary.Count + attributePropsDictionary.Count);

        if(i < diceFacePropsDictionary.Count)
        {
            getRandomDiceFaceProps(grade, parent);
        }
        else
        {
            getRandomAttributeProps(grade, parent);
        }
    }
    void refreshDiceFaceSkillUI()
    {
        Transform tf = diceSidesBarGObj.transform;
        for (int i = 0; i < tf.childCount; i++)
        {
            if(tf.GetChild(i).TryGetComponent<DiceSkillInShop>(out DiceSkillInShop d))
            {
                diceFacePropsPurchaseNumber[d.propsName] -= 1;

                if (diceFacePropsPurchaseNumber[d.propsName] == 1)
                {
                    addPurchasableProduct(d.propsName);
                }

                Destroy(tf.GetChild(i).gameObject);
            }
        }
        for(int i = 0; i < 6; ++i)
        {
            DiceFaceSkill skill = diceController.GetDiceFace(i);
            if (skill != null)
            {
                GameObject obj = new GameObject();

                obj.transform.parent = diceSidesBarGObj.transform;

                obj.AddComponent<DiceSkillInShop>().Initialize(skill);

                diceFacePropsPurchaseNumber[skill.propsName] += 1;

                if (diceFacePropsPurchaseNumber[skill.propsName] == 2)
                {
                    removePurchasableProduct(skill.propsName);
                }
            }
        }
    }
    void nextDice()
    {
        diceController = PlayerController.GetInstance().GetAfterDice();

        refreshDiceFaceSkillUI();
    }
    void lastDice()
    {
        diceController = PlayerController.GetInstance().GetForDice();

        refreshDiceFaceSkillUI();
    }
    void refreshAllProduct() //ˢ��������Ʒ
    {
        if(freeRefresh == false)
        {
            if (balanceCount >= refreshPrice)
            {
                lessMoney(refreshPrice);

                if(EnemyManager.GetInstance().wave <= 3)
                {
                    for (int i = 0; i < shoppingBoardGObj.transform.childCount; ++i)
                    {
                        if (shoppingBoardGObj.transform.GetChild(i).childCount != 0)
                            Destroy(shoppingBoardGObj.transform.GetChild(i).GetChild(0).gameObject);

                        switch (i)
                        {
                            case 0:
                                refreshProps(RefreshType.DieFaceProps, shoppingBoardGObj.transform.GetChild(i));
                                break;
                            case 1:
                                refreshProps(RefreshType.DieFaceProps, shoppingBoardGObj.transform.GetChild(i));
                                break;
                            default:
                                refreshProps(RefreshType.AttributeProps, shoppingBoardGObj.transform.GetChild(i));
                                break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < shoppingBoardGObj.transform.childCount; ++i)
                    {
                        if (shoppingBoardGObj.transform.GetChild(i).childCount != 0)
                            Destroy(shoppingBoardGObj.transform.GetChild(i).GetChild(0).gameObject);

                        switch (i)
                        {
                            case 0:
                                refreshProps(RefreshType.DieFaceProps, shoppingBoardGObj.transform.GetChild(i));
                                break;
                            case 1:
                                refreshProps(RefreshType.AttributeProps, shoppingBoardGObj.transform.GetChild(i));
                                break;
                            default:
                                refreshProps(RefreshType.Random, shoppingBoardGObj.transform.GetChild(i));
                                break;
                        }
                    }
                }
              
            }
            else
            {
                return;
            }
        }
        else
        {
            //ˢ����Ʒ
            for (int i = 0; i < shoppingBoardGObj.transform.childCount; ++i)
            {
                Destroy(shoppingBoardGObj.transform.GetChild(i).GetChild(0).gameObject);

                switch (i)
                {
                    case 0:
                        refreshProps(RefreshType.DieFaceProps, shoppingBoardGObj.transform.GetChild(i));
                        break;
                    case 1:
                        refreshProps(RefreshType.AttributeProps, shoppingBoardGObj.transform.GetChild(i));
                        break;
                    default:
                        refreshProps(RefreshType.Random, shoppingBoardGObj.transform.GetChild(i));
                        break;
                }
            }

            freeRefresh = false;

            refreshPriceText.text = refreshPrice.ToString();
        }
    }
    void updateAttribute()
    {
        diceController = PlayerController.GetInstance().GetDice();

        refreshDiceFaceSkillUI();  

        adjustGradeProbility();

        setMoney(); 

        setFreeRefresh();

        waveNumber = EnemyManager.GetInstance().wave;

        buyingPriceText.text = "";
        sellingPriceText.text = "";
        productDescriptionText.text = "";
    }
    void ShopStart()
    {

            for (int i = 0; i < shoppingBoardGObj.transform.childCount; ++i)
            {
                if (shoppingBoardGObj.transform.GetChild(i).childCount != 0)
                    Destroy(shoppingBoardGObj.transform.GetChild(i).GetChild(0).gameObject);

                switch (i)
                {
                    case 0:
                        refreshProps(RefreshType.DieFaceProps, shoppingBoardGObj.transform.GetChild(i));
                        break;
                    case 1:
                        refreshProps(RefreshType.DieFaceProps, shoppingBoardGObj.transform.GetChild(i));
                        break;
                    default:
                        refreshProps(RefreshType.AttributeProps, shoppingBoardGObj.transform.GetChild(i));
                        break;
                }
            }

      
        bugAndSellAudio = AudioManager.Instance.getAudioData(AudioName.购买和出售);

        closeShopAudio = AudioManager.Instance.getAudioData(AudioName.关闭菜单商店);
            //refres
    }
    void returnGame()
    {
        gameObject.SetActive(false);

        AudioManager.Instance.playVFX(closeShopAudio);

        PlayerController.GetInstance().canInput = true;

        Time.timeScale = 1.0f;
    }
    void adjustRefreshableProduct()
    {

    }
}
