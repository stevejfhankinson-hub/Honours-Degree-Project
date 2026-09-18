// Steven Hankinson 21129647

using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // Used to track the enemy across different scripts
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
        // Stops the code from progressing past this point if the game is paused
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

        // Changes the enemy's speed based on how long it has been chasing the player
        if (EnemyData.enemyActions[enemyNumber] == EnemyData.followAction)
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

        // Tracks the time a enemy has been chasing the player
        if (EnemyData.enemyActions[enemyNumber] == EnemyData.followAction)
        {
            EnemyData.chaseTime[enemyNumber] += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;
        }

        // Slows the enemy down if it is there is fog or rain
        if (GameData.isFog)
        {
            moveSpeed = maxSpeed * 0.85f;
        }

        if (GameData.isRaining)
        {
            moveSpeed = maxSpeed * 1.15f;
        }

        bool inZone = false;

        // Checks if the player is in a section of the map that the enemy is allowed to chase the player in
        for (int i = 0; i < zones.Length; i++)
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

        // Gets the enemy to stop chasing the player if they are not in a section of the map that the enemy is allowed to chase the player in
        if (!inZone)
        {
            EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
        }

        // Sets the enemy speed to moveSpeed variable which is affected by being converted to be the speed for a frame
        // and modified for the speed of the game to make it go faster or slower
        walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

        // Stops the enemy from chasing the player if they are inside a house
        if (PlayerData.checkHouse)
        {
            EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
        }

        if (!EnemyData.bouncing[enemyNumber])
        {
            // Stops the enemy from chasing the player if they are dizzy
            if (EnemyData.isDizzy[enemyNumber])
            {
                return;
            }

            if (EnemyData.enemyActions[enemyNumber] == EnemyData.followAction)
            {
                // Gets the enemy to move towards the player
                Vector3 target = player.transform.position;
                transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed);

                // If the player is too far away from the enemy, the enemy will stop chasing them and return to their patrol route
                if (Vector2.Distance(transform.position, player.transform.position) >= 13)
                {
                    EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
                    EnemyData.resetPatrol[enemyNumber] = true;
                }

                // If the enemy runs into the player while they are carrying a parcel, the enemy will steal it from them
                if (PlayerData.playerItem && Vector2.Distance(transform.position, player.transform.position) <= 0.1)
                {
                    PlayerData.playerItem = false;
                    EnemyData.gotParcel[enemyNumber] = true;
                }

                // If the enemy steals a parcel they will start running away from the player instead of chasing them
                if (EnemyData.gotParcel[enemyNumber] && !PlayerData.playerItem)
                {
                    EnemyData.enemyActions[enemyNumber] = EnemyData.reteatAction;
                }
            }
        }

        
    }
}
