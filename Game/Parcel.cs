// Steven Hankinson 21129647

using UnityEngine;

public class Parcel : MonoBehaviour
{
    public GameObject player;
    public GameObject parcelUI;

    // Store the sprites for the parce
    public Sprite[] parcelSpritesUI;

    SpriteRenderer ParcelRender;
    SpriteRenderer ParcelUIRender;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParcelRender = GetComponent<SpriteRenderer>();
        ParcelUIRender = parcelUI.GetComponent<SpriteRenderer>();

        // Stops the code from progressing past this point if the game is paused
        if (GameData.isPaused)
        {
            return;
        }

        // Shows whether the player has the parcel or not in the UI
        if (!PlayerData.playerItem)
        {
            ParcelUIRender.sprite = parcelSpritesUI[1];
        }

        if (PlayerData.playerItem)
        {
            ParcelUIRender.sprite = parcelSpritesUI[0];
        }

        // Checks whether the player can pick up the parcel and if they can they get it and any enemies holding parcels will lose them
        if (Vector2.Distance(transform.position, player.transform.position) < 0.5f && !PlayerData.playerItem)
        {
            PlayerData.playerItem = true;

            for (int i = 0; i < EnemyData.gotParcel.Length; i++)
            {
                EnemyData.gotParcel[i] = false;
            }
            
            int targetVillager = Random.Range(0, 3);
            VillagerData.villagerParcel[targetVillager] = true;
        }

        // If the player is inside the big house it will appear in the big house
        if(!PlayerData.insideBigHouse)
        {
            ParcelRender.sortingOrder = 0;
        }

        // If the player is not inside the big house it will not appear in the big house
        if (PlayerData.insideBigHouse)
        {
            if(!PlayerData.playerItem)
            {
                ParcelRender.sortingOrder = GameData.layerInsideHouse;
            }

            if (PlayerData.playerItem)
            {
                ParcelRender.sortingOrder = 0;
            }
        }
    }
}
