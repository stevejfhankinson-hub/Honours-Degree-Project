// Steven Hankinson 21129647

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
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
        if (EnemyData.enemyReset[enemyNumber])
        {
            EnemyData.health[enemyNumber] = maxHealth;
            EnemyData.spawn[enemyNumber] = true;
            EnemyData.enemiesRespawn[enemyNumber] = false;

            EnemyData.enemyReset[enemyNumber] = false;
        }

        if (GameData.isPaused)
        {
            return;
        }

        Dis = Vector2.Distance(transform.position, player.transform.position);

        moveSpeed = maxSpeed;

        if (GameData.isFog)
        {
            moveSpeed = maxSpeed * 0.85f;
        }

        if (GameData.isRaining)
        {
            moveSpeed = maxSpeed * 1.15f;
        }

        walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

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
            recordTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (recordTime >= posTime)
            {
                oldPos = transform.position;
                recordTime = 0;
            }

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

        if (!damage)
        {
            damageTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (damageTime >= damageTimeMax)
            {
                damage = true;
            }
        }

        if(EnemyData.health[enemyNumber] <= 0.0f && !EnemyData.enemiesRespawn[enemyNumber])
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
