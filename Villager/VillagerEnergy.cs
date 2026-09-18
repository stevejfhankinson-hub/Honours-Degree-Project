// Steven Hankinson 21129647

using TMPro;
using UnityEngine;

public class VillagerEnergy : MonoBehaviour
{
    // Used to track the villager across different scripts
    public int villagerNumber;

    public TextMeshPro villagerEnergyText;
    bool lostEnergy = false;

    Vector2 cafeTarget;
    Vector2[] cafeLocations;
    float loseEnergy = 0f;

    float[] energyPercentage = new float[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cafeLocations = new Vector2[2];
        cafeLocations[0] = new Vector2(14f, -2.5f);
        cafeLocations[1] = new Vector2(14f, -2.5f);

        VillagerData.villagerEnergy[villagerNumber] = VillagerData.maxFood;
    }

    // Update is called once per frame
    void Update()
    {
        // While the villager is walking to its next spot, it works out how much energy they should lose
        if (VillagerData.villagerActions[villagerNumber] != VillagerData.WaitingAction)
        {
            lostEnergy = false;

            if(!VillagerData.waitEnergy[villagerNumber])
            {
                loseEnergy += ((1f / (1f / Time.deltaTime)) * GameData.timeSpeed) / 2.4f;
            }
        }

        // When the villager stops at the house it will lose the energy which was calculated
        if (VillagerData.villagerActions[villagerNumber] == VillagerData.WaitingAction)
        {
            if (!lostEnergy)
            {
                VillagerData.villagerEnergy[villagerNumber] -= loseEnergy;
                lostEnergy = true;
                loseEnergy = 0f;
            }

            // Once the villager's energy is below 0 it will want to go to a cafe
            if (VillagerData.villagerEnergy[villagerNumber] <= 0)
            {
                VillagerData.villagerCafe[villagerNumber] = true;
            }

            if (VillagerData.villagerEnergy[villagerNumber] > 0)
            {
                VillagerData.villagerCafe[villagerNumber] = false;
            }
        }

        // Works out the percentage  a villager's energy is on and displays it avove its head
        villagerEnergyText.sortingOrder = VillagerData.villagerSortingOrder[villagerNumber];
        villagerEnergyText.transform.position = VillagerData.villagerPosition[villagerNumber];
        villagerEnergyText.transform.position += new Vector3(0, 1f, 0);
        energyPercentage[villagerNumber] = Mathf.Round(VillagerData.villagerEnergy[villagerNumber] / VillagerData.maxFood * 100);
        string energyText = energyPercentage[villagerNumber].ToString();

        villagerEnergyText.text = "Energy: " + energyText + "%";
    }
}
