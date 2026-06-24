// Steven Hankinson 21129647

using UnityEngine;

public class EnemyRetreat : MonoBehaviour
{
    public int enemyNumber;
    public GameObject player;
    public float maxSpeed;
    float moveSpeed;
    float walkSpeed;

    Vector2 difference;
    Vector3 target;
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

        if (!EnemyData.bouncing[enemyNumber])
        {
            if (EnemyData.isDizzy[enemyNumber])
            {
                return;
            }

            if (EnemyData.enemyActions[enemyNumber] == EnemyData.reteatAction)
            {
                difference.x = player.transform.position.x - transform.position.x;
                difference.y = player.transform.position.y - transform.position.y;

                target.x = transform.position.x - difference.x;
                target.y = transform.position.y - difference.y;
                target.z = transform.position.z;

                transform.position = Vector2.MoveTowards(transform.position, target, walkSpeed);

                if (Vector2.Distance(transform.position, player.transform.position) >= 5)
                {
                    EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;

                    EnemyData.resetPatrol[enemyNumber] = true;
                }
            }
        }
    }
}
