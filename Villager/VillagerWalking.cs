// Steven Hankinson 21129647

// Created and adjusted by following "Make Characters MOVE around on Their OWN! - Top Down Unity 2D #20" video
// by Game Code Library uploaded on March 2025
// https://www.youtube.com/watch?v=40oTxM6cSYw

using System.Collections;
using UnityEngine;

public class VillagerWalking : MonoBehaviour
{
    public int villagerNumber;
    public GameObject Player;
    public GameObject InteractSpace;
    public Sprite[] WalkSprites;
    public Transform levelPathsParent;
    public Transform housePathsParent;
    public Transform housesParent;

    public float maxSpeed;
    
    private bool[] startLv1ToLv2 = new bool[3];
    private bool[] startLv3ToLv4 = new bool[3];
    private bool[] startLv5ToLv6 = new bool[3];
    private bool[] startLv7 = new bool[3];

    private bool[] targetLv1ToLv2 = new bool[3];
    private bool[] targetLv3ToLv4 = new bool[3];
    private bool[] targetLv5ToLv6 = new bool[3];
    private bool[] targetLv7 = new bool[3];

    private Vector3[] housePathLv1ToLv2 = new Vector3[3];
    private Vector3[] housePathLv3ToLv4 = new Vector3[3];
    private Vector3[] housePathLv5ToLv6 = new Vector3[3];
    private Vector3[] housePathLv7 = new Vector3[3];

    private Vector3[] houseLv1ToLv2 = new Vector3[3];
    private Vector3[] houseLv3ToLv4 = new Vector3[3];
    private Vector3[] houseLv5ToLv6 = new Vector3[3];
    private Vector3[] houseLv7 = new Vector3[3];

    private Vector3[] pathLv1toLv3 = new Vector3[3];
    private Vector3[] pathLv2toLv4 = new Vector3[2];
    private Vector3[] pathLv3toLv5 = new Vector3[2];
    private Vector3[] pathLv5toLv7 = new Vector3[2];
    private Vector3[] pathLv4toLv6 = new Vector3[3];
    private Vector3[] pathLv6toLv7 = new Vector3[2];

    private float moveSpeed;
    private float walkSpeed;
    
    public float maxWaitTime = 1.1f;
    private int currentWaypointIndex = 0;
    private int maxWaypointIndex;
    private Transform[] lvlPaths;
    private Transform[] housePaths;
    private Transform[] houses;

    SpriteRenderer VillagerRender;

    private float currentWaitTime = 0;

    float animationTime;

    int NorthCycle1 = 0;
    int NorthCycle2 = 1;

    int EastCycle1 = 2;
    int EastCycle2 = 3;

    int SouthCycle1 = 4;
    int SouthCycle2 = 5;

    int WestCycle1 = 6;
    int WestCycle2 = 7;

    float maxX = 1.5f;
    float minX = 1.5f;
    float minY = 1.3f;
    float maxY = 1.5f;

    float parcelCooldownTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VillagerData.villagerActions[villagerNumber] = VillagerData.PatrolAction;

        lvlPaths = new Transform[levelPathsParent.childCount];

        housePaths = new Transform[housePathsParent.childCount];

        houses = new Transform[housesParent.childCount];

        for (int i = 0; i < levelPathsParent.childCount; i++)
        {
            lvlPaths[i] = levelPathsParent.GetChild(i);
        }

        for (int i = 0; i < housePathsParent.childCount; i++)
        {
            housePaths[i] = housePathsParent.GetChild(i);
        }

        for (int i = 0; i < housesParent.childCount; i++)
        {
            houses[i] = housesParent.GetChild(i);
        }

        pathLv1toLv3[0] = lvlPaths[0].transform.position;
        pathLv1toLv3[1] = lvlPaths[6].transform.position;
        pathLv1toLv3[2] = lvlPaths[1].transform.position;

        pathLv2toLv4[0] = lvlPaths[7].transform.position;
        pathLv2toLv4[1] = lvlPaths[8].transform.position;

        pathLv3toLv5[0] = lvlPaths[2].transform.position;
        pathLv3toLv5[1] = lvlPaths[3].transform.position;
        
        pathLv4toLv6[0] = lvlPaths[9].transform.position;
        pathLv4toLv6[1] = lvlPaths[10].transform.position;
        pathLv4toLv6[2] = lvlPaths[11].transform.position;

        pathLv5toLv7[0] = lvlPaths[4].transform.position;
        pathLv5toLv7[1] = lvlPaths[5].transform.position;

        pathLv6toLv7[0] = lvlPaths[12].transform.position;
        pathLv6toLv7[1] = lvlPaths[13].transform.position;



        housePathLv1ToLv2[0] = housePaths[0].transform.position;
        housePathLv1ToLv2[1] = housePaths[1].transform.position;
        housePathLv1ToLv2[2] = housePaths[2].transform.position;

        housePathLv3ToLv4[0] = housePaths[3].transform.position;
        housePathLv3ToLv4[1] = housePaths[4].transform.position;
        housePathLv3ToLv4[2] = housePaths[10].transform.position;

        housePathLv5ToLv6[0] = housePaths[11].transform.position;
        housePathLv5ToLv6[1] = housePaths[5].transform.position;
        housePathLv5ToLv6[2] = housePaths[6].transform.position;

        housePathLv7[0] = housePaths[7].transform.position;
        housePathLv7[1] = housePaths[8].transform.position;
        housePathLv7[2] = housePaths[9].transform.position;



        houseLv1ToLv2[0] = houses[0].transform.position;
        houseLv1ToLv2[1] = houses[1].transform.position;
        houseLv1ToLv2[2] = houses[2].transform.position;

        houseLv3ToLv4[0] = houses[3].transform.position;
        houseLv3ToLv4[1] = houses[4].transform.position;
        houseLv3ToLv4[2] = houses[10].transform.position;

        houseLv5ToLv6[0] = houses[11].transform.position;
        houseLv5ToLv6[1] = houses[5].transform.position;
        houseLv5ToLv6[2] = houses[6].transform.position;

        houseLv7[0] = houses[7].transform.position;
        houseLv7[1] = houses[8].transform.position;
        houseLv7[2] = houses[9].transform.position;

        VillagerData.homeNumber[villagerNumber] = Random.Range(0, 10);
        VillagerData.building1Number[villagerNumber] = Random.Range(0, 10);
        VillagerData.building2Number[villagerNumber] = Random.Range(0, 10);

        
        int nextLocation = Random.Range(0, 3);

        if (nextLocation == 0)
        {
            VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.homeNumber[villagerNumber];
        }

        if (nextLocation == 1)
        {
            VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.building1Number[villagerNumber];
        }

        if (nextLocation == 2)
        {
            VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.building2Number[villagerNumber];
        }

        VillagerData.startBuildingNumber[villagerNumber] = VillagerData.homeNumber[villagerNumber];
        VillagerData.targetBuildingNumber[villagerNumber] = nextLocation;
        
        transform.position = houses[VillagerData.homeNumber[villagerNumber]].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (VillagerData.villagerReset[villagerNumber])
        {
            int nextLocation = Random.Range(0, 3);

            if (nextLocation == 0)
            {
                VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.homeNumber[villagerNumber];
            }

            if (nextLocation == 1)
            {
                VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.building1Number[villagerNumber];
            }

            if (nextLocation == 2)
            {
                VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.building2Number[villagerNumber];
            }

            transform.position = houses[VillagerData.homeNumber[villagerNumber]].transform.position;

            VillagerData.villagerParcel[villagerNumber] = false;
            VillagerData.villagerReset[villagerNumber] = false;
        }

        VillagerRender = GetComponent<SpriteRenderer>();
        
        if (VillagerData.directionNorth[villagerNumber])
        {
            InteractSpace.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, InteractSpace.transform.position.z);
        }

        if (VillagerData.directionEast[villagerNumber])
        {
            InteractSpace.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, InteractSpace.transform.position.z);
        }

        if (VillagerData.directionSouth[villagerNumber])
        {
            InteractSpace.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, InteractSpace.transform.position.z);
        }

        if (VillagerData.directionWest[villagerNumber])
        {
            InteractSpace.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, InteractSpace.transform.position.z);
        }

        Vector3 oldPos = transform.position;

        if (GameData.isPaused)
        {
            return;
        }

        moveSpeed = maxSpeed;

        if (GameData.isFog)
        {
            moveSpeed = maxSpeed * 0.85f;
        }

        if (Vector2.Distance(InteractSpace.transform.position, Player.transform.position) < 0.2f)
        {
            if (VillagerData.villagerParcel[villagerNumber])
            {
                VillagerData.villagerParcel[villagerNumber] = false;
                PlayerData.playerItem = false;
                PlayerData.score += 100 * PlayerData.scoreMultiplier;
                PlayerData.scoreMultiplier = 1;
                VillagerData.parcelCooldown[villagerNumber] = true;
            }
        }

        VillagerData.waitEnergy[villagerNumber] = false;

        VillagerData.villagerPosition[villagerNumber] = transform.position;
        if (GameData.isRaining)
        {
            moveSpeed = maxSpeed * 1.15f;

            for (int i = 0; i < 12; i++)
            {
                if (transform.position.y <= houses[i].position.y + maxY && transform.position.y >= houses[i].position.y - minY
                && transform.position.x <= houses[i].position.x + maxX && transform.position.x >= houses[i].position.x - minX
                && currentWaypointIndex != maxWaypointIndex)
                {
                    VillagerData.waitEnergy[villagerNumber] = true;
                    return;
                }
            }
        }

        if (VillagerData.parcelCooldown[villagerNumber])
        {
            parcelCooldownTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;
            if (parcelCooldownTime >= 4)
            {
                VillagerData.parcelCooldown[villagerNumber] = false;
                parcelCooldownTime = 0f;
            }
        }

        if (Vector2.Distance(transform.position, Player.transform.position) < 2.2f && !VillagerData.parcelCooldown[villagerNumber])
        {
            int lookAtPlayer = 0;

            VillagerData.waitEnergy[villagerNumber] = true;

            Vector2 playerDifference; 
            playerDifference.x = transform.position.x - Player.transform.position.x;

            playerDifference.y = transform.position.y - Player.transform.position.y;

            Vector2 playerBiggerAxis = playerDifference;

            if (playerBiggerAxis.x < 0)
            {
                playerBiggerAxis.x = -playerBiggerAxis.x;
            }

            if (playerBiggerAxis.y < 0)
            {
                playerBiggerAxis.y = -playerBiggerAxis.y;
            }

            // East and West
            if (playerBiggerAxis.x > playerBiggerAxis.y)
            {
                if (playerDifference.x < 0)
                {
                    lookAtPlayer = 2;

                    VillagerData.directionNorth[villagerNumber] = false;
                    VillagerData.directionEast[villagerNumber] = true;
                    VillagerData.directionWest[villagerNumber] = false;
                    VillagerData.directionSouth[villagerNumber] = false;
                }

                if (playerDifference.x > 0)
                {
                    lookAtPlayer = 6;

                    VillagerData.directionNorth[villagerNumber] = false;
                    VillagerData.directionEast[villagerNumber] = false;
                    VillagerData.directionWest[villagerNumber] = true;
                    VillagerData.directionSouth[villagerNumber] = false;
                }
            }

            // North and South
            if (playerBiggerAxis.x <= playerBiggerAxis.y)
            {
                if (playerDifference.y < 0)
                {
                    lookAtPlayer = 0;

                    VillagerData.directionNorth[villagerNumber] = true;
                    VillagerData.directionEast[villagerNumber] = false;
                    VillagerData.directionWest[villagerNumber] = false;
                    VillagerData.directionSouth[villagerNumber] = false;
                }

                if (playerDifference.y > 0)
                {
                    lookAtPlayer = 4;

                    VillagerData.directionNorth[villagerNumber] = false;
                    VillagerData.directionEast[villagerNumber] = false;
                    VillagerData.directionWest[villagerNumber] = false;
                    VillagerData.directionSouth[villagerNumber] = true;
                }
            }

            VillagerRender.sprite = WalkSprites[lookAtPlayer];

            return;
        }

        walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

        if (VillagerData.villagerActions[villagerNumber] == VillagerData.WaitingAction)
        {
            currentWaitTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (Vector2.Distance(transform.position, houseLv3ToLv4[2]) < 0.1f)
            {
                VillagerData.villagerEnergy[villagerNumber] = VillagerData.maxFood;
            }

            if (Vector2.Distance(transform.position, houseLv5ToLv6[0]) < 0.1f)
            {
                VillagerData.villagerEnergy[villagerNumber] = VillagerData.maxFood;
            }

            if (currentWaitTime > maxWaitTime)
            {
                currentWaypointIndex = 0;
                VillagerData.startBuildingNumber[villagerNumber] = VillagerData.targetBuildingNumber[villagerNumber];
                
                if (VillagerData.villagerCafe[villagerNumber])
                {
                    VillagerData.villagerActions[villagerNumber] = VillagerData.EnergyAction;

                    float[] cafeDistance = new float[2];

                    cafeDistance[0] = Vector2.Distance(transform.position, houseLv3ToLv4[2]);
                    cafeDistance[1] = Vector2.Distance(transform.position, houseLv5ToLv6[0]);

                    if (cafeDistance[0] <= cafeDistance[1])
                    {
                        VillagerData.targetBuildingNumber[villagerNumber] = 10;
                    }

                    if (cafeDistance[0] >= cafeDistance[1])
                    {
                        VillagerData.targetBuildingNumber[villagerNumber] = 11;
                    }
                }

                if(!VillagerData.villagerCafe[villagerNumber])
                {
                    VillagerData.villagerActions[villagerNumber] = VillagerData.PatrolAction;

                    int nextLocation = Random.Range(0,2);

                    if(nextLocation == 0)
                    {
                        VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.homeNumber[villagerNumber];
                    }

                    if (nextLocation == 1)
                    {
                        VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.building1Number[villagerNumber];
                    }

                    if (nextLocation == 2)
                    {
                        VillagerData.targetBuildingNumber[villagerNumber] = VillagerData.building2Number[villagerNumber];
                    }

                }
            }
        }

        if (VillagerData.villagerActions[villagerNumber] != VillagerData.WaitingAction)
        {
            HouseNumberToBool();

            Vector3 target = GetTarget();

            MoveToWaypoint(target);
        }

        Vector2 positionDifference;
        positionDifference.x = transform.position.x - oldPos.x;
        positionDifference.y = transform.position.y - oldPos.y;

        Vector2 biggerAxis;

        biggerAxis.x = positionDifference.x;
        biggerAxis.y = positionDifference.y;

        if (biggerAxis.x < 0)
        {
            biggerAxis.x = -biggerAxis.x;
        }

        if (biggerAxis.y < 0)
        {
            biggerAxis.y = -biggerAxis.y;
        }

        // East and West
        if(biggerAxis.x >= biggerAxis.y)
        {
            // East
            if (positionDifference.x > 0)
            {
                VillagerData.directionNorth[villagerNumber] = false;
                VillagerData.directionEast[villagerNumber] = true;
                VillagerData.directionWest[villagerNumber] = false;
                VillagerData.directionSouth[villagerNumber] = false;
            }

            // West
            if (positionDifference.x < 0)
            {
                VillagerData.directionNorth[villagerNumber] = false;
                VillagerData.directionEast[villagerNumber] = false;
                VillagerData.directionWest[villagerNumber] = true;
                VillagerData.directionSouth[villagerNumber] = false;
            }
        }

        // North and South
        if (biggerAxis.x <= biggerAxis.y)
        {
            // North
            if (positionDifference.y > 0)
            {
                VillagerData.directionNorth[villagerNumber] = true;
                VillagerData.directionEast[villagerNumber] = false;
                VillagerData.directionWest[villagerNumber] = false;
                VillagerData.directionSouth[villagerNumber] = false;
            }

            // South
            if (positionDifference.y < 0)
            {
                VillagerData.directionNorth[villagerNumber] = false;
                VillagerData.directionEast[villagerNumber] = false;
                VillagerData.directionWest[villagerNumber] = false;
                VillagerData.directionSouth[villagerNumber] = true;
            }
        }

        if (oldPos.x == transform.position.x && oldPos.y == transform.position.y)
        {
            VillagerData.waitEnergy[villagerNumber] = true;
        }

        if (oldPos.x != transform.position.x && oldPos.y != transform.position.y)
        {
            animationTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (animationTime > GameData.animationTimeReset)
                animationTime = 0f;
        }

        if (VillagerData.directionNorth[villagerNumber])
        {
            if (animationTime < GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[NorthCycle1];

            if (animationTime >= GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[NorthCycle2];
        }

        if (VillagerData.directionEast[villagerNumber])
        {
            if (animationTime < GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[EastCycle1];

            if (animationTime >= GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[EastCycle2];
        }

        if (VillagerData.directionWest[villagerNumber])
        {
            if (animationTime < GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[WestCycle1];

            if (animationTime >= GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[WestCycle2];
        }

        if (VillagerData.directionSouth[villagerNumber])
        {
            if (animationTime < GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[SouthCycle1];

            if (animationTime >= GameData.animationTimeReset / 2f)
                VillagerRender.sprite = WalkSprites[SouthCycle2];
        }
    }

    private Vector3 GetTarget()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (startLv1ToLv2[i] && targetLv1ToLv2[j])
                {
                    if (i == j)
                    {
                        maxWaypointIndex = 1;

                        if (currentWaypointIndex == 0)
                        {
                            return housePathLv1ToLv2[i];
                        }

                        if (currentWaypointIndex == 1)
                        {
                            return houseLv1ToLv2[i];
                        }
                    }

                    if (i != j)
                    {
                        if(i != 0 && j != 0)
                        {
                            maxWaypointIndex = 2;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv1ToLv2[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (housePathLv1ToLv2[j]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (houseLv1ToLv2[j]);
                            }
                        }

                        if(i == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv1ToLv2[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv1toLv3[1]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv1toLv3[0]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (housePathLv1ToLv2[j]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (houseLv1ToLv2[j]);
                            }
                        }

                        if(j == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv1ToLv2[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv1toLv3[0]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv1toLv3[1]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (housePathLv1ToLv2[j]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (houseLv1ToLv2[j]);
                            }
                        }
                    }
                }

                if (startLv1ToLv2[i] && targetLv3ToLv4[j])
                {
                    if(i == 0)
                    {
                        if(j != 2)
                        {
                            maxWaypointIndex = 4;

                            if(currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if(currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[1];
                            }

                            if(currentWaypointIndex == 3)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if(j == 2)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }
                    }

                    if(i != 0)
                    {
                        if (j != 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if (j == 2)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }
                    }
                }

                if (startLv1ToLv2[i] && targetLv5ToLv6[j])
                {
                    if(i == 0)
                    {
                        if(j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }
                    }

                    if(i != 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }
                    }
                }

                if (startLv1ToLv2[i] && targetLv7[j])
                {
                    if(i == 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv7[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv7[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 10;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 9)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 10)
                            {
                                return houseLv7[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv1ToLv2[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv7[j];
                            }
                        }
                    }
                }

                if (startLv3ToLv4[i] && targetLv1ToLv2[j])
                {
                    if(i != 2)
                    {
                        if(j == 0)
                        {
                            maxWaypointIndex = 4;

                            if(currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if(currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }
                    }

                    if(i == 2)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }
                    }
                }

                if (startLv3ToLv4[i] && targetLv3ToLv4[j])
                {
                    if (i == j)
                    {
                        maxWaypointIndex = 1;

                        if (currentWaypointIndex == 0)
                        {
                            return housePathLv3ToLv4[i];
                        }

                        if (currentWaypointIndex == 1)
                        {
                            return houseLv3ToLv4[i];
                        }
                    }

                    if (i != j)
                    {
                        if (i != 2 && j != 2)
                        {
                            maxWaypointIndex = 2;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv3ToLv4[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (housePathLv3ToLv4[j]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (houseLv3ToLv4[j]);
                            }
                        }

                        if (i == 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if (j == 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv3ToLv4[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv4toLv6[0]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv4toLv6[1]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (pathLv3toLv5[1]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (pathLv3toLv5[0]);
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return (housePathLv3ToLv4[j]);
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return (houseLv3ToLv4[j]);
                            }
                        }
                    }
                }

                if (startLv3ToLv4[i] && targetLv5ToLv6[j])
                {
                    if (i != 2)
                    {
                        if(j == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }
                    }

                    if (i == 2)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }
                    }
                }

                if (startLv3ToLv4[i] && targetLv7[j])
                {
                    if (i != 2)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv7[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv7[j];
                            }
                        }
                    }

                    if (i == 2)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv7[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv3ToLv4[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv7[j];
                            }
                        }
                    }
                }

                if (startLv5ToLv6[i] && targetLv1ToLv2[j])
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }
                    }
                }

                if (startLv5ToLv6[i] && targetLv3ToLv4[j])
                {
                    if (i == 0)
                    {
                        if (j != 2)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if (j == 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j != 2)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if (j == 2)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if(currentWaypointIndex == 1)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }
                    }
                }

                if (startLv5ToLv6[i] && targetLv5ToLv6[j])
                {
                    if (i == j)
                    {
                        maxWaypointIndex = 1;

                        if (currentWaypointIndex == 0)
                        {
                            return housePathLv5ToLv6[i];
                        }

                        if (currentWaypointIndex == 1)
                        {
                            return houseLv5ToLv6[i];
                        }
                    }

                    if (i != j)
                    {
                        if (i != 0 && j != 0)
                        {
                            maxWaypointIndex = 2;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv5ToLv6[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (housePathLv5ToLv6[j]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (houseLv5ToLv6[j]);
                            }
                        }

                        if (i == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv5ToLv6[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv4toLv6[2]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv4toLv6[1]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (housePathLv5ToLv6[j]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (houseLv5ToLv6[j]);
                            }
                        }

                        if (j == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv5ToLv6[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv4toLv6[1]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv4toLv6[2]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (housePathLv5ToLv6[j]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (houseLv5ToLv6[j]);
                            }
                        }
                    }
                }

                if (startLv5ToLv6[i] && targetLv7[j])
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv7[j];
                            }

                            
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv7[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv7[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv5ToLv6[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv7[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv7[j];
                            }
                        }
                    }
                }

                if (startLv7[i] && targetLv1ToLv2[j])
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 10;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv2toLv4[1];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv2toLv4[0];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 9)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 10)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv1toLv3[1];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv1toLv3[2];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv1toLv3[0];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv1ToLv2[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv1ToLv2[j];
                            }
                        }
                    }
                }

                if (startLv7[i] && targetLv3ToLv4[j])
                {
                    if (i == 0)
                    {
                        if (j != 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if (j == 2)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j != 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }

                        if (j == 2)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv3toLv5[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv3toLv5[0];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv3ToLv4[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv3ToLv4[j];
                            }
                        }
                    }
                }

                if (startLv7[i] && targetLv5ToLv6[j])
                {
                    if (i == 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if( currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv6toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv6toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }
                    }

                    if (i != 0)
                    {
                        if (j == 0)
                        {
                            maxWaypointIndex = 6;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return pathLv4toLv6[1];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return pathLv4toLv6[2];
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }

                        if (j != 0)
                        {
                            maxWaypointIndex = 4;

                            if (currentWaypointIndex == 0)
                            {
                                return housePathLv7[i];
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return pathLv5toLv7[1];
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return pathLv5toLv7[0];
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return housePathLv5ToLv6[j];
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return houseLv5ToLv6[j];
                            }
                        }
                    }
                }

                if (startLv7[i] && targetLv7[j])
                {
                    if (i == j)
                    {
                        maxWaypointIndex = 1;

                        if (currentWaypointIndex == 0)
                        {
                            return housePathLv7[i];
                        }

                        if (currentWaypointIndex == 1)
                        {
                            return houseLv7[i];
                        }
                    }

                    if (i != j)
                    {
                        if (i != 0 && j != 0)
                        {
                            maxWaypointIndex = 2;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv7[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (housePathLv7[j]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (houseLv7[j]);
                            }
                        }

                        if (i == 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv7[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv6toLv7[1]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv6toLv7[0]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (pathLv4toLv6[2]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (pathLv4toLv6[1]);
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return (pathLv5toLv7[0]);
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return (pathLv5toLv7[1]);
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return (housePathLv7[j]);
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return (houseLv7[j]);
                            }
                        }

                        if (i != 0)
                        {
                            maxWaypointIndex = 8;

                            if (currentWaypointIndex == 0)
                            {
                                return (housePathLv7[i]);
                            }

                            if (currentWaypointIndex == 1)
                            {
                                return (pathLv5toLv7[1]);
                            }

                            if (currentWaypointIndex == 2)
                            {
                                return (pathLv5toLv7[0]);
                            }

                            if (currentWaypointIndex == 3)
                            {
                                return (pathLv4toLv6[1]);
                            }

                            if (currentWaypointIndex == 4)
                            {
                                return (pathLv4toLv6[2]);
                            }

                            if (currentWaypointIndex == 5)
                            {
                                return (pathLv6toLv7[0]);
                            }

                            if (currentWaypointIndex == 6)
                            {
                                return (pathLv6toLv7[1]);
                            }

                            if (currentWaypointIndex == 7)
                            {
                                return (housePathLv7[j]);
                            }

                            if (currentWaypointIndex == 8)
                            {
                                return (houseLv7[j]);
                            }
                        }
                    }
                }
            }
        }

        return transform.position;
    }
    private void HouseNumberToBool()
    {
        for (int i = 0; i < 3; i++)
        {
            startLv1ToLv2[i] = false;
            startLv3ToLv4[i] = false;
            startLv5ToLv6[i] = false;
            startLv7[i] = false;

            targetLv1ToLv2[i] = false;
            targetLv3ToLv4[i] = false;
            targetLv5ToLv6[i] = false;
            targetLv7[i] = false;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 0)
        {
            startLv1ToLv2[0] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 1)
        {
            startLv1ToLv2[1] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 2)
        {
            startLv1ToLv2[2] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 3)
        {
            startLv3ToLv4[0] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 4) 
        {
            startLv3ToLv4[1] = true;
        }
        
        if (VillagerData.startBuildingNumber[villagerNumber] == 5)
        {
            startLv5ToLv6[1] = true;
        }

        if(VillagerData.startBuildingNumber[villagerNumber] == 6)
        {
            startLv5ToLv6[2] = true;
        }

        if(VillagerData.startBuildingNumber[villagerNumber] == 7)
        {
            startLv7[0] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 8)
        {
            startLv7[1] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 9)
        {
            startLv7[2] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 10)
        {
            startLv3ToLv4[2] = true;
        }

        if (VillagerData.startBuildingNumber[villagerNumber] == 11)
        {
            startLv5ToLv6[0] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 0)
        {
            targetLv1ToLv2[0] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 1)
        {
            targetLv1ToLv2[1] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 2)
        {
            targetLv1ToLv2[2] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 3)
        {
            targetLv3ToLv4[0] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 4)
        {
            targetLv3ToLv4[1] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 5)
        {
            targetLv5ToLv6[1] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 6)
        {
            targetLv5ToLv6[2] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 7)
        {
            targetLv7[0] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 8)
        {
            targetLv7[1] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 9)
        {
            targetLv7[2] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 10)
        {
            targetLv3ToLv4[2] = true;
        }

        if (VillagerData.targetBuildingNumber[villagerNumber] == 11)
        {
            targetLv5ToLv6[0] = true;
        }
    }
    private void MoveToWaypoint(Vector3 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex > maxWaypointIndex)
            {
                VillagerData.villagerActions[villagerNumber] = VillagerData.WaitingAction;
                currentWaitTime = 0f;
            }
        }
    }
}
