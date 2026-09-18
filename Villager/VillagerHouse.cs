// Steven Hankinson 21129647

using UnityEngine;

public class VillagerHouse : MonoBehaviour
{
    // Used to track the villager across different scripts
    public int villagerNumber;
    public Transform housesParent;

    private Transform[] housePositions;

    float maxX = 1.5f;
    float minX = 1.5f;
    float minY = 1.3f;
    float maxY = 1.5f;

    SpriteRenderer VillagerRender;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Gets all the house enterance positions
        housePositions = new Transform[housesParent.childCount];

        for (int i = 0; i < housesParent.childCount; i++)
        {
            housePositions[i] = housesParent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool inHouse = false;
        int houseNumber = -5;

        VillagerRender = GetComponent<SpriteRenderer>();

        // Checks whether the villager is in the house
        for (int i = 0; i < housesParent.childCount; i++)
        {
            if(transform.position.y <= housePositions[i].position.y + maxY && transform.position.y >= housePositions[i].position.y - minY
            && transform.position.x <= housePositions[i].position.x + maxX && transform.position.x >= housePositions[i].position.x - minX)
            {
                inHouse = true;
                houseNumber = i;
            }
        }

        // Makes the villager appear outside if it is outside
        if (!inHouse)
        {
            VillagerRender.sortingOrder = GameData.layerOutsideHouse;
        }

        // Makes the villager appear in the house, if the player is in the same house
        if (inHouse)
        {
            if (PlayerData.insideSmallHouse[houseNumber])
            {
                VillagerRender.sortingOrder = GameData.layerInsideHouse;
            }

            if (!PlayerData.insideSmallHouse[houseNumber])
            {
                VillagerRender.sortingOrder = GameData.layerOutsideHouse - 1;
            }
        }

        // Makes the villager face south when they stop in the house
        for(int i = 0; i < housesParent.childCount; i++)
        {
            if(Vector2.Distance(transform.position, housePositions[i].position) < 0.1f)
            {
                VillagerData.directionNorth[villagerNumber] = false;
                VillagerData.directionEast[villagerNumber] = false;
                VillagerData.directionWest[villagerNumber] = false;
                VillagerData.directionSouth[villagerNumber] = true;
            }    
        }

        VillagerData.villagerSortingOrder[villagerNumber] = VillagerRender.sortingOrder;
    }
}
