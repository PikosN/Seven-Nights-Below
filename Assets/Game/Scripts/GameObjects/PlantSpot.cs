using UnityEngine;

public class PlantSpot : MonoBehaviour, IInteractable
{
    public AudioSource interactionAudioSource;
    public AudioClip[] plantSounds;

    public GameObject plantVisual;
    private enum State
    {
        Empty,
        Growing,
        Ready
    }
    private State state = State.Empty;
    private float growTimer;
    private PlantData plantData;

    public GameObject growthBar;
    public GameObject fillBar;

    public float growthTime;

    public void Interact()
    {
        //if (state == State.Empty) G.plantSelectionUI.OpenUI(this);
        if (state == State.Empty)
        {
            PlantData plant = AllPlants.GetPlantData("unknown_plant");
            int plantCost = GameMath.GetPlantCost(
                plant.baseCost,
                plant.costGrowthRate,
                G.plantManager.currentPlants
            );
            if (G.economyManager.TrySpentMoney(plantCost))
            {
                Plant(plant);
            }
            ;
        }
        if (state == State.Ready) Harvest();
    }
    public string GetInteractText()
    {
        if (state == State.Empty)
        {
            PlantData plant = AllPlants.GetPlantData("unknown_plant");
            int plantCost = GameMath.GetPlantCost(
                plant.baseCost,
                plant.costGrowthRate,
                G.plantManager.currentPlants
            );
            return $"Press E to plant for ${plantCost}";
        }
        if (state == State.Growing)
        {
            return "Plant is growing...";
        }
        if (state == State.Ready)
        {
            return "Press E to harvest";
        }
        
        return "";
    }

    public void Plant(PlantData plant)
    {
        interactionAudioSource.PlayOneShot(plantSounds[Random.Range(0, plantSounds.Length)]);

        plantData = plant;
        plantVisual.SetActive(true);
        state = State.Growing;
        growTimer = 0f;

        G.plantManager.BuyPlant();
        G.plantManager.plantSpots.Add(this);

        growthBar.SetActive(G.lightManager.isLightOn);

        SetGrowthTime();
    }

    void Harvest()
    {
        interactionAudioSource.PlayOneShot(plantSounds[Random.Range(0, plantSounds.Length)]);

        plantVisual.SetActive(false);
        plantVisual.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        int moneyPerPlant = GameMath.GetMoneyPerPlant(
                plantData.baseReward,
                AllUpgrades.GetUpgrade("money").effectRate,
                G.upgradeManager.GetUpgradeLevel("money")
            );
        float bonusChance = GameMath.GetBonusChance(
                plantData.bonusChance,
                AllUpgrades.GetUpgrade("bonus_chance").effectRate,
                G.upgradeManager.GetUpgradeLevel("bonus_chance")
            );
        int bonusMoney = 0;
        if ( Random.value < bonusChance)
        {
            bonusMoney = GameMath.GetBonusMoney(
                plantData.bonusMoney,
                AllUpgrades.GetUpgrade("bonus_money").effectRate,
                G.upgradeManager.GetUpgradeLevel("bonus_money")
            );
        }
        G.economyManager.AddMoney(moneyPerPlant + bonusMoney);

        plantVisual.SetActive(true);
        state = State.Growing;
        growTimer = 0f;

        SetGrowthTime();
    }



    void Update()
    {
        if (state != State.Growing) return;

        if (!G.lightManager.isLightOn) return;

        growTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(growTimer / growthTime);

        fillBar.transform.localScale = new Vector3(progress, 1f, 1f);

        plantVisual.transform.localScale = Vector3.one * progress * 2.5f;
        plantVisual.transform.localPosition = new Vector3(0f, 0.35f, 0f);

        if (growTimer >= growthTime)
        {
            if (G.upgradeManager.GetUpgradeLevel("autoharvest") == 1)
            {
                Harvest();
            }
            else
            {
                state = State.Ready;
            }
        }
    }

    public void SetGrowthTime()
    {
        growthTime = GameMath.GetGrowthTime(
                plantData.baseGrowthTime,
                AllUpgrades.GetUpgrade("growth").effectRate,
                G.upgradeManager.GetUpgradeLevel("growth")
            ) + Random.Range(-1.5f, 1.5f);
    }

    public void Reset()
    {
        state = State.Empty;

        plantVisual.SetActive(false);
        plantVisual.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        fillBar.transform.localScale = Vector3.zero;
        growthBar.SetActive(false);
    }
}
