using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSkillManager
{
    // Start is called before the first frame update
    public static PropsSkillManager Instance = new PropsSkillManager();

    public int rangeSize = 2;

    public float speedSize = 1;

    int index = 0;

    public DiceFaceSkill getDiceSkill(DiceSkillName name, int grade, DiceFacePropsData data)
    {
        index = index + 1;
        data.index = index;
        switch (name)
        {
            case DiceSkillName.ExplosionInPlace:
                return new ExplosionInPlace(grade, data);
            case DiceSkillName.PreciseSlash:
                return new PreciseSlash(grade, data);
            case DiceSkillName.StunBlow:
                return new StunBlow(grade, data);
            case DiceSkillName.ShootSpikes:
                return new ShootSpikes(grade, data);
            case DiceSkillName.HolySkill:
                return new HolySkill(grade, data);
            case DiceSkillName.AuraSkill:
                return new AuraSkill(grade, data);
            case DiceSkillName.AngelShadowSkill:
                return new AngelShadowSkill(grade, data);
            case DiceSkillName.LifePotion:
                return new LifePotion(grade, data);
            case DiceSkillName.NanoKill:
                return new NanoKill(grade, data);
            case DiceSkillName.DoubleTheGold:
                return new DoubleTheGold(grade, data);
        }
        return null;
    }
    public AttributeChange getAttributeChange(AttributeChangeName name, int grade)
    {
        switch (name)
        {
            case AttributeChangeName.LifeCrystal: 
                return new LifeCrystal(grade);
          
            case AttributeChangeName.Footwear:
                return new Footwear(grade);
          
            case AttributeChangeName.PhysicalSkill:
                return new PhysicalSkill(grade);
             
            case AttributeChangeName.MagicCrystal:
                return new MagicCrystal(grade);
       
            default: return null;
        }
    }
}
