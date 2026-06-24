// Steven Hankinson 21129647

using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public int enemyNumber;
    public GameObject player;
    public GameObject[] zones;
    public float maxSpeed;
    float moveSpeed;
    float walkSpeed;

    private GameObject[] newZones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.isPaused)
        {
            return;
        }
        
        if (EnemyData.chaseTime[enemyNumber] >= EnemyData.chaseTimeMax)
        {
            EnemyData.chaseCooldown[enemyNumber] = true;
            EnemyData.chaseTime[enemyNumber] = 0f;
            EnemyData.chaseCooldownTime[enemyNumber] = 0f;
            EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;

            EnemyData.resetPatrol[enemyNumber] = true;
        }

        if(EnemyData.enemyActions[enemyNumber] == EnemyData.followAction)
        {
            if (EnemyData.chaseTime[enemyNumber] > 0f && EnemyData.chaseTime[enemyNumber] <= 1f)
            {
                moveSpeed = 3.75f;
            }

            if (EnemyData.chaseTime[enemyNumber] > 1f && EnemyData.chaseTime[enemyNumber] <= 9f)
            {
                moveSpeed = 3.25f;
            }

            if (EnemyData.chaseTime[enemyNumber] > 9 && EnemyData.chaseTime[enemyNumber] <= 14)
            {
                moveSpeed = 2.75f;
            }

            if (EnemyData.chaseTime[enemyNumber] > 14 && EnemyData.chaseTime[enemyNumber] > 16.5)
            {
                moveSpeed = 2.05f;
            }

            if (EnemyData.chaseTime[enemyNumber] > 16.5)
            {
                moveSpeed = 1.8f;
            }
        }

        if (EnemyData.enemyActions[enemyNumber] == EnemyData.followAction)
        {
            EnemyData.chaseTime[enemyNumber] += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;
        }

        if (GameData.isFog)
        {
            moveSpeed = maxSpeed * 0.85f;
        }

        if (GameData.isRaining)
        {
            moveSpeed = maxSpeed * 1.15f;
        }

        bool inZone = false;

        for(int i = 0; i < zones.Length; i++)
        {
            if ((player.transform.position.x >= zones[i].transform.position.x - zones[i].transform.localScale.x / 2)
        && (player.transform.position.y >= zones[i].transform.position.y - zones[i].transform.localScale.y / 2)

        && (player.transform.position.x <= zones[i].transform.position.x + zones[i].transform.localScale.x / 2)
        && (player.transform.position.y >= zones[i].transform.position.y - zones[i].transform.localScale.y / 2)

        && (player.transform.position.x <= zones[i].transform.position.x + zones[i].transform.localScale.x / 2)
        && (player.transform.position.y <= zones[i].transform.position.y + zones[i].transform.localScale.y / 2)

        && (player.transform.position.x >= zones[i].transform.position.x - zones[i].transform.localScale.x / 2)
        && (player.transform.position.y <= zones[i].transform.position.y + zones[i].transform.localScale.y / 2))
            {
                inZone = true;

            }
        }
        

        if(inZone)
        {

        }
        else
        {
            EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
        } 
        
        
        walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

        if (PlayerData.checkHouse)
        {
            EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
        }

        if (!EnemyData.bouncing[enemyNumber])
        {
            if (EnemyData.isDizzy[enemyNumber])
            {
                return;
            }

            if (EnemyData.enemyActions[enemyNumber] == EnemyData.followAction)
            {
                Vector3 target = player.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed);

                if (Vector2.Distance(transform.position, player.transform.position) >= 13)
                {
                    EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
                    EnemyData.resetPatrol[enemyNumber] = true;
                }

                if (PlayerData.playerItem && Vector2.Distance(transform.position, player.transform.position) <= 0.1)
                {
                    PlayerData.playerItem = false;
                    EnemyData.gotParcel[enemyNumber] = true;
                }

                if (EnemyData.gotParcel[enemyNumber] && !PlayerData.playerItem)
                {
                    EnemyData.enemyActions[enemyNumber] = EnemyData.reteatAction;
                }
            }
        }

        
    }
}
