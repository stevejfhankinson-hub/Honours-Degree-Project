// Steven Hankinson 21129647

using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static bool doInteract = false;
    public static bool directionNorth = true;
    public static bool directionEast = false;
    public static bool directionSouth = false;
    public static bool directionWest = false;

    public static bool directionNorthEast = false;
    public static bool directionNorthWest = false;
    public static bool directionSouthEast = false;
    public static bool directionSouthWest = false;

    public static bool playerItem = false;
    public static bool removeItem = false;
    public static bool checkHouse = false;
    public static bool insideBigHouse = false;
    public static bool[] insideSmallHouse = new bool[12];
    public static float walkSpeed;
    public static float dashingTime;
    public static float dashingTimeMax = 0.6f;
    public static float dashCooldownTime = 0;
    public static float dashCooldownTimeMax = 3f;

    public static float attackingTime = 0;
    public static float attackingTimeMax = 0.45f;

    public static bool attacking = false;
    public static bool dashing = false;
    public static bool dashCooldown = false;

    public static int smokeBombAmount = 5;
    public static bool isSmoke = false;
    public static bool useSmokeBomb = false;
    public static float smokeTime = 0;
    public static float smokeTimeMax = 5f;

    public static float cameraSpeed;
    public static Vector2 differencePos = Vector2.zero;
    public static Vector2 moveDirection = Vector2.zero;
    public static float score = 0;
    public static float scoreMultiplier = 1;
    public static int returnSprite;
    public static bool resetSprite = false;

    public static bool playerReset = false;
    public static bool resetDashUI = false;

    public static bool unpauseDelay = false;
    public static float unpauseDelayTime = 0f;
    public static float unpauseDelayTimeMax = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
