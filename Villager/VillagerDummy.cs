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

        if (PlayerData.insideBigHouse)
        {
            DummyRender.sortingOrder = GameData.layerInsideHouse;
        }

        if (!PlayerData.insideBigHouse)
        {
            DummyRender.sortingOrder = 0;
        }

        bool gotTarget = false;

        for(int i = 0; i < 3; i++)
        {
            if (VillagerData.villagerParcel[i])
            {
                gotTarget = true;
                DummyRender.sprite = villagerSprite[i];
            }
        }

        if(!gotTarget)
        {
            DummyRender.sortingOrder = 0;
        }
    }
}
