// Steven Hankinson 21129647

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Used to track the enemy across different scripts
    public int enemyNumber;

    public GameObject player;
    public GameObject damageSpace;
    public GameObject smoke;
    public float maxSpeed;
    float moveSpeed;
    public float maxHealth;

    float walkSpeed;
    float swordDamage = 20;
    float damageRange = 0.5f;
    float Dis;
    float minRange = 0.35f;

    float dizzyTime = 0f;
    float maxDizzyTime = 5f;

    float recordTime = 0;
    float posTime = 3f;
    float bounceTime = 0f;
    float stopBounceTime = 0.8f;
    float damageTime = 0;
    float damageTimeMax = 1.2f;
    float respawnTime = 0f;
    float respawnTimeMax = 4f;

    Vector2 oldPos;
    Vector2 startPos;
    Vector2 bouncePos;
    Vector2 damageDifference;

    bool damage = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyData.health[enemyNumber] = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Resets everything to the default values when the game is reset
        if (EnemyData.enemyReset[enemyNumber])
        {
            EnemyData.health[enemyNumber] = maxHealth;
            EnemyData.spawn[enemyNumber] = true;
            EnemyData.enemiesRespawn[enemyNumber] = false;

            EnemyData.enemyReset[enemyNumber] = false;
        }

        // Stops the code from progressing past this point if the game is paused
        if (GameData.isPaused)
        {
            return;
        }

        // Gets the distance between the enemy and the player
        Dis = Vector2.Distance(transform.position, player.transform.position);

        moveSpeed = maxSpeed;

        // Slows the enemy down if it is there is fog or rain
        if (GameData.isFog)
        {
            moveSpeed = maxSpeed * 0.85f;
        }

        if (GameData.isRaining)
        {
            moveSpeed = maxSpeed * 1.15f;
        }

        // Sets the enemy speed to moveSpeed variable which is affected by being converted to be the speed for a frame
        // and modified for the speed of the game to make it go faster or slower
        walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

        // If the enemy is meant to bounce of the player is will move towards the bounce position unti it is meant to stop bouncing
        if (EnemyData.bouncing[enemyNumber])
        {
            bounceTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (bounceTime >= stopBounceTime)
            {
                EnemyData.bouncing[enemyNumber] = false;
            }
            else
            {
                bouncePos = (oldPos - startPos) * 0.35f + startPos;
                walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed * 1.2f;
                transform.position = Vector2.MoveTowards(transform.position, bouncePos, walkSpeed);
            }
        }

        if (!EnemyData.bouncing[enemyNumber])
        {
            // Records the position of the enemy every posTime seconds to be used for bouncing off the player
            recordTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (recordTime >= posTime)
            {
                oldPos = transform.position;
                recordTime = 0;
            }

            // If a enemy is too close to the player it will take the parcel from the player if they player is not attacking
            if (Dis <= minRange)
            {
                if (!PlayerData.attacking && !PlayerData.dashing && PlayerData.playerItem)
                {
                    EnemyData.gotParcel[enemyNumber] = true;
                    PlayerData.playerItem = false;
                }
                EnemyData.bouncing[enemyNumber] = true;
                bounceTime = 0f;
                recordTime = 0f;
            }
        }

        // Works out the difference between where the player can damage the enemy and enemy position to see if the player is close enough to damage the enemy
        damageDifference.x = damageSpace.transform.position.x - transform.position.x;
        damageDifference.y = damageSpace.transform.position.y - transform.position.y;

        if (damageDifference.x < 0)
        {
            damageDifference.x = -damageDifference.x;
        }

        if (damageDifference.y < 0)
        {
            damageDifference.y = -damageDifference.y;
        }

        // When the enemy can be damaged it will take damage when in the player is attacking. If the player is dashing it will take more damage
        if (damage)
        {
            if (damageRange >= damageDifference.x && damageRange >= damageDifference.y && (PlayerData.attacking || PlayerData.dashing))
            {
                float damageAmount = swordDamage;

                if (PlayerData.dashing)
                {
                    damageAmount = damageAmount * 1.25f;
                }

                EnemyData.health[enemyNumber] -= damageAmount;
                damage = false;
                damageTime = 0;
            }
        }

        // If the enemy has just been damaged it will not be able to take damage for a certain time
        if (!damage)
        {
            damageTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (damageTime >= damageTimeMax)
            {
                damage = true;
            }
        }

        // Makes the enemy reset and put it in a position so it cannot be seen. If it took a parcel off
        // a player it will give it back to the player and set the score multiplier to 1.5 for that parcel
        if (EnemyData.health[enemyNumber] <= 0.0f && !EnemyData.enemiesRespawn[enemyNumber])
        {
            EnemyData.enemiesRespawn[enemyNumber] = true;
            transform.position = new Vector3(50, 50, transform.position.z);
            respawnTime = 0f;
            EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;

            if (EnemyData.gotParcel[enemyNumber])
            {
                PlayerData.playerItem = true;
                EnemyData.gotParcel[enemyNumber] = false;
                PlayerData.scoreMultiplier = 1.5f;
            }
        }

        // If the enemy is in a smoke bomb it will become dizzy for a certain amount of time so it cannot walk
        if(!EnemyData.isDizzy[enemyNumber] && PlayerData.isSmoke && (Vector2.Distance(transform.position, smoke.transform.position) < smoke.transform.localScale.x / 2f))
        {
            EnemyData.isDizzy[enemyNumber] = true;
            dizzyTime = 0f;
        }

        if (EnemyData.isDizzy[enemyNumber])
        {
            dizzyTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if(dizzyTime > maxDizzyTime )
            {
                EnemyData.isDizzy[enemyNumber] = false;
            }
        }

        // After a set amount of time, the enemy will respawn after it has died
        if(EnemyData.enemiesRespawn[enemyNumber])
        {
            respawnTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (respawnTime >= respawnTimeMax)
            {
                EnemyData.health[enemyNumber] = maxHealth;
                EnemyData.spawn[enemyNumber] = true;
                EnemyData.enemiesRespawn[enemyNumber] = false;
            }
        }
    }
}
