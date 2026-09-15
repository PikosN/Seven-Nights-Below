using System;
using UnityEngine;

public static class GameMath
{
    public static int GetPlantCost(int baseCost, float plantRate, int currentPlants)
    {
        int plantCost = Mathf.RoundToInt(baseCost * Mathf.Pow(plantRate, currentPlants));
        return plantCost;
    }
    public static float GetGrowthTime(float baseTime, float growthRate, int level)
    {
        float plantGrowthTime = baseTime * Mathf.Pow(growthRate, level);
        return plantGrowthTime;
    }
    public static int GetMoneyPerPlant(int baseMoney, float moneyRate, int level)
    {
        int moneyPerPlant = Mathf.RoundToInt(baseMoney * Mathf.Pow(moneyRate, level));
        return moneyPerPlant;
    }
    public static int GetUpgradeCost(int baseCost, float costGrowthRate, int level)
    {
        return Mathf.RoundToInt(baseCost * Mathf.Pow(costGrowthRate, level));
    }
    public static int GetDayGoal(int baseDayGoal, int day)
    {
        return Mathf.RoundToInt(baseDayGoal * Mathf.Pow(2f, day - 1));
    }
    public static float GetBonusChance(float baseChance, float bonusChancePerLevel, int level)
    {
        return (baseChance + bonusChancePerLevel * level);
    }
    public static int GetBonusMoney(float baseBonus, float bonusPerLevel, int level)
    {
        return Mathf.RoundToInt(baseBonus + bonusPerLevel * level);
    }

    public static float GetPlantLossChance(float basePlantLossChance, float plantLossChancePerLevel, int level)
    {
        
        return basePlantLossChance - plantLossChancePerLevel * level;
    } 
}
