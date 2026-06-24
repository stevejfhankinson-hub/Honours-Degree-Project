// Steven Hankinson 21129647

// Created and adjusted by following "Make Characters MOVE around on Their OWN! - Top Down Unity 2D #20" video
// by Game Code Library uploaded on March 2025
// https://www.youtube.com/watch?v=40oTxM6cSYw

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatrol : MonoBehaviour
{
    public int enemyNumber;
    public GameObject player;
    public GameObject sight;
    public int startWaypointIndex = 0;
    public Transform waypointParent;
    public float maxSpeed;
    float moveSpeed;

    Rigidbody2D rd;
    float walkSpeed;
    bool getToPlayer = false;

    public float waitTime = 1.1f;
    private int currentWaypointIndex;
    private bool loopWaypoints = true;
    private bool isWaiting = false;
    private Transform[] waypoints;
    
    float sightTime = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyData.enemyActions[enemyNumber] = EnemyData.patrolAction;
        waypoints = new Transform[waypointParent.childCount];

        for(int i = 0; i < waypointParent.childCount; i++)
        {
            waypoints[i] = waypointParent.GetChild(i);
        }

        currentWaypointIndex = startWaypointIndex;

        if(currentWaypointIndex <= 0 || currentWaypointIndex >= waypointParent.childCount)
        {
            currentWaypointIndex = 0;
        }

        if(currentWaypointIndex == 0)
        {
            transform.position = new Vector3(waypoints[waypointParent.childCount - 1].position.x, waypoints[waypointParent.childCount - 1].position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(waypoints[currentWaypointIndex - 1].position.x, 
                                             waypoints[currentWaypointIndex - 1].position.y, 
                                             transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyData.resetPatrol[enemyNumber])
        {
            currentWaypointIndex = startWaypointIndex;
            EnemyData.resetPatrol[enemyNumber] = false;
        }

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

        rd = sight.GetComponent<Rigidbody2D>();

        walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

        if (EnemyData.chaseCooldown[enemyNumber])
        {
            EnemyData.chaseCooldownTime[enemyNumber] += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (EnemyData.chaseCooldownTime[enemyNumber] >= EnemyData.chaseCooldownTimeMax)
            {
                EnemyData.chaseCooldown[enemyNumber] = false;
                EnemyData.chaseCooldownTime[enemyNumber] = 0f;
            }

        }

        if (EnemyData.spawn[enemyNumber])
        {
            currentWaypointIndex = startWaypointIndex;

            if (currentWaypointIndex <= 0 || currentWaypointIndex >= waypointParent.childCount)
            {
                currentWaypointIndex = 0;
            }

            if (currentWaypointIndex == 0)
            {
                transform.position = new Vector3(waypoints[waypointParent.childCount - 1].position.x, 
                                                 waypoints[waypointParent.childCount - 1].position.y, 
                                                 transform.position.z);
            }
            else
            {
                transform.position = new Vector3(waypoints[currentWaypointIndex - 1].position.x,
                                                 waypoints[currentWaypointIndex - 1].position.y,
                                                 transform.position.z);
            }

            EnemyData.spawn[enemyNumber] = false;
        }
        
        if (EnemyData.enemiesRespawn[enemyNumber])
        {
            return;
        }

        if (!EnemyData.bouncing[enemyNumber])
        {
            if (EnemyData.isDizzy[enemyNumber])
            {
                return;
            }

            if (EnemyData.enemyActions[enemyNumber] == EnemyData.patrolAction)
            {
                if (isWaiting)
                {
                    return;
                }

                MoveToWaypoint();

                sightTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

                if (sightTime >= 0.5)
                {
                    sight.transform.position = new Vector3(transform.position.x, transform.position.y, sight.transform.position.z);
                    sightTime = 0.0f;
                }

                if (Vector2.Distance(transform.position, player.transform.position) <= 13)
                {
                    rd.MovePosition(player.transform.position);

                    if (Vector2.Distance(sight.transform.position, player.transform.position) <= 0.1)
                    {
                        getToPlayer = true;
                    }
                    else
                    {
                        getToPlayer = false;
                    }
                }
                else
                {
                    sight.transform.position = transform.position;
                    getToPlayer = false;
                }

                if (getToPlayer)
                {
                    if((player.transform.position.y >= -15.5 && player.transform.position.y <= 5)
                     &&(player.transform.position.x >= -4.7 && player.transform.position.x <= 9.6))
                    {
                        if (PlayerData.playerItem && !EnemyData.chaseCooldown[enemyNumber])
                        {
                            EnemyData.enemyActions[enemyNumber] = EnemyData.followAction;
                        }

                        if (EnemyData.gotParcel[enemyNumber])
                        {
                            if (Vector2.Distance(transform.position, player.transform.position) <= 5)
                            {
                                EnemyData.enemyActions[enemyNumber] = EnemyData.reteatAction;
                            }
                        }
                    }
                }
            }
        }
    }

    void MoveToWaypoint()
    {
        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector2.MoveTowards(transform.position, target.position, walkSpeed);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);


        currentWaypointIndex = loopWaypoints ? (currentWaypointIndex + 1) % waypoints.Length : Mathf.Min(currentWaypointIndex + 1, waypoints.Length - 1);

        isWaiting = false;
    }
}
