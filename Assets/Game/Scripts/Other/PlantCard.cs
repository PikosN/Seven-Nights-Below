using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantCard : MonoBehaviour
{
    public TMP_Text plantName;
    public Image plantImage;
    public TMP_Text plantDescription;
    public TMP_Text plantCost;
    public Button plantButton;

    private PlantData plant;

    public void Setup(PlantData plantData)
    {
        plant = plantData;

        plantName.text = plantData.name;
        plantDescription.text = plantData.description;
        
        RefreshCost();

        plantButton.onClick.AddListener(BuyAndPlant);
    }

    void BuyAndPlant()
    {
        int plantCost = GameMath.GetPlantCost(
                plant.baseCost,
                plant.costGrowthRate,
                G.plantManager.currentPlants
            );
        if (G.economyManager.TrySpentMoney(plantCost))
        {
            G.plantSelectionUI.currentSpot.Plant(plant);
            G.plantSelectionUI.CloseUI();
            G.plantSelectionUI.RefreshPlantsCost();
        }
    }

    public void RefreshCost()
    {
        int cost = GameMath.GetPlantCost(
                plant.baseCost,
                plant.costGrowthRate,
                G.plantManager.currentPlants
            );
        plantCost.text = "$" + cost;
    }

    public void Delete()
    {
        G.plantSelectionUI.activePlantCards.Remove(this);
        Destroy(gameObject);
    }
}
