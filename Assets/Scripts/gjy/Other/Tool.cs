using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public interface ISkillExcute
{
    void excute();
}
public interface IChangeAttribute
{
    void changeAttribute();
}

public enum RefreshType
{
    DieFaceProps,
    AttributeProps,
    Buy,
    Random
}
public enum DiceFacePropsName
{原地起爆,
    再次起爆,
    连环爆炸,
    零零起爆器,
    艺术就是爆炸,
    迅速补刀,
    再来一刀,
    精准一刀,
    斩草除根,
    挫骨扬灰,
    硬化骰子,
    钢制甩骰,
    合金弹骰,
    骰铁,
    天星陨落,
    十字尖刺,
    米字尖刺,
    十二方尖刺,
    刺猬先生,
    尖刺玫瑰,
    圣光 ,
    啊圣光,
    圣光照耀,
    圣骑之光,
    你相信光吗,
    天使的光环,
    天使的守护,
    天使的庇佑,
    天使可移动光源,
    天使降临我身边,
    天使虚影,
    天使幻象,
    高天投影,
    全息碎影,
    替身使者,
    弱效生命药水,
    普通生命药水,
    高级生命药水,
    超级生命药水,
    奶妈快奶我,
    纳米斩杀小子,
    钱来,
}
public enum AttributePropsName
{
    弱化生命水晶,
    普通生命水晶,
    中级生命水晶,
    高级生命水晶,
    生命水晶ProMax,
    草鞋,
    布鞋,
    战靴,
    高级战靴,
    黄金切尔西,
    弱化物理技艺,
    普通物理技艺,
    中级物理技艺,
    高级物理技艺,
    奇妙の物理技艺,
    弱化魔法水晶,
    普通魔法水晶,
    中级魔法水晶,
    高级魔法水晶,
    魔法水晶ProMax,
}
public enum DiceSkillName
{
    ExplosionInPlace,
    PreciseSlash,
    StunBlow,
    ShootSpikes,
    HolySkill,
    AuraSkill,
    AngelShadowSkill,
    LifePotion,
    NanoKill,
    DoubleTheGold
}
public enum AttributeChangeName
{
    LifeCrystal,
    Footwear,
    PhysicalSkill,
    MagicCrystal
}
public enum DamageType
{
    physics, magic
}
[Serializable]
public class DiceFacePropsData
{
    public DiceFacePropsName propsName;

    public DiceSkillName skillName;

    public int price;

    public int salePrice;

    [NonSerialized]public int index;

    [Range(1, 5)] public int grade; //Ʒ��

    public Sprite sprite;

    public string description;
}
[Serializable]
public class AttributePropsData
{
    public AttributePropsName propsName;

    public AttributeChangeName changeName;

    public int price;

    [Range(1, 5)] public int grade;

    public Sprite sprite;

    [NonSerialized]public Text priceText;

    public string description;
}
public abstract class DiceFaceSkill
{
    public DiceFacePropsName propsName;

    public Sprite sprite;

    public int salePrice;

    public int index;

    public GameObject range;

    public AudioData audioData;

    public DiceFaceSkill(DiceFacePropsData data)
    {
        propsName = data.propsName;

        range = Resources.Load("Prefab/Range") as GameObject;

    }
    abstract public void excute(DiceController dice);
}
[Serializable]
public class AudioData
{
    public AudioName audioName;

    public AudioClip audioClip;

    public float volume = 1;

    public float pitch = 1;
}
public enum AudioName
{
    关闭菜单商店,
    冲锋恶魔死亡,
    钱来,
    圣光骰子,
    天使光环骰,
    天使虚影骰面,
    尖刺音效,
    打开菜单商店,
    斩杀,
    法师恶魔死亡,
    爆炸恶魔爆炸,
    特殊道具选择,
    玩家拾取金币,
    玩家死亡,
    玩家装配卸下骰子,
    生命药水骰,
    硬化骰子,
    补刀骰子,
    购买和出售,
    飞行普通恶魔死亡,
    骑士官死亡,
    骰子投掷,
    骰子撞击,
    骰子爆炸,
}
public abstract class AttributeChange
{
    public abstract void changeAttribute();
}