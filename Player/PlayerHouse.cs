// Steven Hankinson 21129647

using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHouse : MonoBehaviour
{
    public Transform houseEnteranceParent;
    public GameObject[] houseExterior;
    public GameObject[] houseInterior;
    public GameObject[] houseGrass;

    private Transform[] houseEnterance;

    SpriteRenderer PlayerRender;
    TilemapRenderer[] ExteriorRender;
    TilemapRenderer[] InteriorRender;
    TilemapRenderer[] GrassRender;

    bool[] inHouseEnterance;
    bool[] inHouse;
    int[] houseToggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        houseEnterance = new Transform[houseEnteranceParent.childCount];

        for (int i = 0; i < houseEnteranceParent.childCount; i++)
        {
           houseEnterance[i] = houseEnteranceParent.GetChild(i);
        }

        inHouseEnterance = new bool[houseEnterance.Length];
        inHouse = new bool[houseEnterance.Length];
        houseToggle = new int[houseEnterance.Length];

        ExteriorRender = new TilemapRenderer[houseExterior.Length];
        InteriorRender = new TilemapRenderer[houseInterior.Length];
        GrassRender = new TilemapRenderer[houseGrass.Length];

        for (int i = 0; i < houseEnterance.Length; i++)
        {
            inHouseEnterance[i] = false;
            inHouse[i] = false;
            houseToggle[i] = 0;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameData.isPaused)
        {
            return;
        }

        PlayerRender = GetComponent<SpriteRenderer>();

        for(int i = 0; i < ExteriorRender.Length; i++)
        {
            ExteriorRender[i] = houseExterior[i].GetComponent<TilemapRenderer>();
            InteriorRender[i] = houseInterior[i].GetComponent<TilemapRenderer>();
            GrassRender[i] = houseGrass[i].GetComponent<TilemapRenderer>();
        }

        for (int i = 0; i < houseEnterance.Length; i++)
        {
            if(i == 10)
            {
                if (transform.position.x <= houseEnterance[i].position.x + 5.65f
            && transform.position.x >= houseEnterance[i].position.x - 5.85f
            && transform.position.y >= houseEnterance[i].position.y
            && transform.position.y <= houseEnterance[i].position.y + 6.4f)
                {
                    inHouse[i] = true;
                    PlayerData.insideBigHouse = true;
                }

                if (!(transform.position.x <= houseEnterance[i].position.x + 5.65f
                && transform.position.x >= houseEnterance[i].position.x - 5.85f
                && transform.position.y >= houseEnterance[i].position.y
                && transform.position.y <= houseEnterance[i].position.y + 6.4f))
                {
                    inHouse[i] = false;
                    PlayerData.insideBigHouse = false;
                }
            }

            if(i != 10)
            {
                if (transform.position.x <= houseEnterance[i].position.x + 1.85f
            && transform.position.x >= houseEnterance[i].position.x - 1.85f
            && transform.position.y >= houseEnterance[i].position.y
            && transform.position.y <= houseEnterance[i].position.y + 2.4f)
                {
                    inHouse[i] = true;
                }

                if (!(transform.position.x <= houseEnterance[i].position.x + 1.85f
                && transform.position.x >= houseEnterance[i].position.x - 1.85f
                && transform.position.y >= houseEnterance[i].position.y
                && transform.position.y <= houseEnterance[i].position.y + 2.4f))
                {
                    inHouse[i] = false;
                }
            }
        }

        PlayerData.checkHouse = false;
        //bool checkHouse = false;

        for(int i = 0; i < 12; i ++)
        {
            PlayerData.insideSmallHouse[i] = false;
        }

        for(int i = 0; i < houseEnterance.Length;i++)
        {
            if (inHouse[i])
            {
                PlayerData.checkHouse = true;
                //checkHouse = true;
                PlayerRender.sortingOrder = GameData.layerInsideHouse;
                InteriorRender[i].sortingOrder = GameData.layerInsideHouse - 1;
                ExteriorRender[i].sortingOrder = GameData.layerInsideHouse - 2;
                GrassRender[i].sortingOrder = 0;
                
                if(i < 10)
                {
                    PlayerData.insideSmallHouse[i] = true;
                }

                if( i > 10)
                {
                    PlayerData.insideSmallHouse[i - 1] = true;
                }

                if(i == 10)
                {
                    PlayerData.insideBigHouse = true;
                }
            }

            if (!inHouse[i])
            {
                InteriorRender[i].sortingOrder = GameData.layerOutsideHouse - 3;
                ExteriorRender[i].sortingOrder = GameData.layerOutsideHouse - 1;
                GrassRender[i].sortingOrder = GameData.layerOutsideHouse - 2;

                if (i == 10)
                {
                    PlayerData.insideBigHouse = false;
                }
            }
        }

        if(!PlayerData.checkHouse)
        {
            PlayerRender.sortingOrder = GameData.layerOutsideHouse;
        }    
    }
}
