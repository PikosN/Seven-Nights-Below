using System.Collections.Generic;

public class PlantData
{
    public string id;
    public string name;
    public string description;
    public int baseCost;
    public int baseReward;
    public float baseGrowthTime;

    public float costGrowthRate;

    public float bonusChance;
    public int bonusMoney;
}

public static class AllPlants
{
    public static List<PlantData> all = new List<PlantData>
    {
        new PlantData()
        {
            id = "unknown_plant",
            name = "Unknown plant",
            description = "What is it?",
            baseCost = 10,
            baseReward = 10,
            baseGrowthTime = 10f,
            costGrowthRate = 1.2f,
            bonusChance = 0.05f,
            bonusMoney = 1,
        },
        new PlantData()
        {
            id = "unknown_mushroom",
            name = "Unknown mushroom",
            description = "Big mushroom",
            baseCost = 14,
            baseReward = 20,
            baseGrowthTime = 12f,
            costGrowthRate = 1.2f,
        },
        //new PlantData()
        //{
        //    id = "",  
        //    name = "",
        //    description = "",
        //    baseCost = 0,
        //    baseReward = 0,
        //    baseGrowthTime = 0,
        //    costGrowthRate = 0,
        //},
    };
    
    public static PlantData GetPlantData(string plantId)
    {
        return AllPlants.all.Find(plant => plant.id == plantId);
    }

}