// Steven Hankinson 21129647

using UnityEngine;

public class EnemyData : MonoBehaviour
{
    
    public static int[] enemyActions = new int[2];
    public static int patrolAction = 0;
    public static int followAction = 1;
    public static int reteatAction = 2;

    public static bool[] isDizzy = new bool[2];
    public static bool[] gotParcel = new bool[2];
    public static bool[] enemiesRespawn = new bool[2];

    public static bool[] directionNorth = new bool[2];
    public static bool[] directionEast = new bool[2];
    public static bool[] directionSouth = new bool[2];
    public static bool[] directionWest = new bool[2];

    public static bool[] bouncing = new bool[2];
    public static float[] health = new float[2];

    public static bool[] spawn = new bool[2];
    public static bool[] enemyReset = new bool[2];

    public static bool[] chaseCooldown = new bool[2];
    public static float[] chaseCooldownTime = new float[2];
    public static float chaseCooldownTimeMax = 14f;

    public static float[] chaseTime = new float[2];
    public static float chaseTimeMax = 20f;

    public static bool[] resetPatrol = new bool[2];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
