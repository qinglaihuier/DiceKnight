using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeCrystal : AttributeChange
{
    int lifeGrowthValue = 0;
    public LifeCrystal(int grade) 
    { 
        switch (grade)
        {
            case 1:
                lifeGrowthValue = 1;
                break;
            case 2:
                lifeGrowthValue = 3;
                break;
            case 3:
                lifeGrowthValue = 8;
                break;
            case 4:
                lifeGrowthValue = 16;
                break;
            case 5:
                lifeGrowthValue = 24;
                break;
        }
    }
    public override void changeAttribute()
    {
        PlayerController.GetInstance().currentHealth += lifeGrowthValue;

        PlayerController.GetInstance().maxHealth += lifeGrowthValue;
    }
}
public class Footwear : AttributeChange
{
    double spGrowthValue = 0;
    public Footwear(int grade)
    {
        switch (grade)
        {
            case 1:
                spGrowthValue = 0.2;
                break;
            case 2:
                spGrowthValue = 0.5;
                break;
            case 3:
                spGrowthValue = 0.8;
                break;
            case 4:
                spGrowthValue = 1;
                break;
            case 5:
                spGrowthValue = 1.5;
                break;
        }
    }
    public override void changeAttribute()
    {
        PlayerController.GetInstance().speed += (float)spGrowthValue;
    }
}
public class PhysicalSkill : AttributeChange
{
    int pAtkGrowthValue = 0;
    public PhysicalSkill(int grade)
    {
        switch (grade)
        {
            case 1:
                pAtkGrowthValue = 3;
                break;
            case 2:
                pAtkGrowthValue = 7;
                break;
            case 3:
                pAtkGrowthValue = 15;
                break;
            case 4:
                pAtkGrowthValue = 24;
                break;
            case 5:
                pAtkGrowthValue = 36;
                break;
        }
    }
    public override void changeAttribute()
    {
        PlayerController.GetInstance().pAtk += pAtkGrowthValue;
    }
}
public class MagicCrystal : AttributeChange
{
    int mAtkGrowthValue = 0;
    public MagicCrystal(int grade)
    {
        switch (grade)
        {
            case 1:
                mAtkGrowthValue = 4;
                break;
            case 2:
                mAtkGrowthValue = 9;
                break;
            case 3:
                mAtkGrowthValue = 18;
                break;
            case 4:
                mAtkGrowthValue = 28;
                break;
            case 5:
                mAtkGrowthValue = 42;
                break;
        }
    }
    public override void changeAttribute()
    {
       PlayerController.GetInstance().mAtk += mAtkGrowthValue;  
    }
}