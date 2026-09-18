// Steven Hankinson 21129647

using UnityEngine;

public class VillagerDummy : MonoBehaviour
{
    public Sprite[] villagerSprite;

    SpriteRenderer DummyRender;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DummyRender = GetComponent<SpriteRenderer>();

        // Shows the dummy village the player is looking for while in the big house
        if (PlayerData.insideBigHouse)
        {
            DummyRender.sortingOrder = GameData.layerInsideHouse;
        }

        // Hides the dummy village the player is looking for while in the big house
        if (!PlayerData.insideBigHouse)
        {
            DummyRender.sortingOrder = 0;
        }

        // Works out which villager the player is looking for
        bool gotTarget = false;

        for(int i = 0; i < 3; i++)
        {
            if (VillagerData.villagerParcel[i])
            {
                gotTarget = true;
                DummyRender.sprite = villagerSprite[i];
            }
        }

        // Once the player has given the villager their parcel, the dummy villager will disappear
        if (!gotTarget)
        {
            DummyRender.sortingOrder = 0;
        }
    }
}
