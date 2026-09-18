// Steven Hankinson 21129647

using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRetreat : MonoBehaviour
{
    // Used to track the enemy across different scripts
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
        // Stops the code from progressing past this point if the game is paused
        if (GameData.isPaused)
        {
            return;
        }

        
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

        if (!EnemyData.bouncing[enemyNumber])
        {
            // Stops enemies from moving if they are dizzy
            if (EnemyData.isDizzy[enemyNumber])
            {
                return;
            }

            // If enemies are retreating they will move away from the player
            // until they are 5 units away from the player where they will then go back to patrolling
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
