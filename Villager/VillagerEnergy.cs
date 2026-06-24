// Steven Hankinson 21129647

using TMPro;
using UnityEngine;

public class VillagerEnergy : MonoBehaviour
{
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
        if (VillagerData.villagerActions[villagerNumber] != VillagerData.WaitingAction)
        {
            lostEnergy = false;

            if(!VillagerData.waitEnergy[villagerNumber])
            {
                loseEnergy += ((1f / (1f / Time.deltaTime)) * GameData.timeSpeed) / 2.4f;
            }
        }

        if (VillagerData.villagerActions[villagerNumber] == VillagerData.WaitingAction)
        {
            if (!lostEnergy)
            {
                VillagerData.villagerEnergy[villagerNumber] -= loseEnergy;
                lostEnergy = true;
                loseEnergy = 0f;
            }

            if (VillagerData.villagerEnergy[villagerNumber] <= 0)
            {
                VillagerData.villagerCafe[villagerNumber] = true;
            }

            if (VillagerData.villagerEnergy[villagerNumber] > 0)
            {
                VillagerData.villagerCafe[villagerNumber] = false;
            }
        }

        villagerEnergyText.sortingOrder = VillagerData.villagerSortingOrder[villagerNumber];
        villagerEnergyText.transform.position = VillagerData.villagerPosition[villagerNumber];
        villagerEnergyText.transform.position += new Vector3(0, 1f, 0);
        energyPercentage[villagerNumber] = Mathf.Round(VillagerData.villagerEnergy[villagerNumber] / VillagerData.maxFood * 100);
        string energyText = energyPercentage[villagerNumber].ToString();

        villagerEnergyText.text = "Energy: " + energyText + "%";
    }
}
