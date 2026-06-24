// Steven Hankinson 21129647

using UnityEngine;

public class Parcel : MonoBehaviour
{
    public GameObject player;
    public GameObject parcelUI;
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

        if (GameData.isPaused)
        {
            return;
        }

        if (!PlayerData.playerItem)
        {
            ParcelUIRender.sprite = parcelSpritesUI[1];
        }

        if (PlayerData.playerItem)
        {
            ParcelUIRender.sprite = parcelSpritesUI[0];
        }

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

        if(!PlayerData.insideBigHouse)
        {
            ParcelRender.sortingOrder = 0;
        }

        if(PlayerData.insideBigHouse)
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
