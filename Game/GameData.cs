// Steven Hankinson 21129647

using UnityEngine;

public class GameData : MonoBehaviour
{
    public static int layerUI = 6;
    public static int layerInsideHouse = 6;
    public static int layerOutsideHouse = 4;
    public static float timeSpeed = 1;
    public static float gameTime = 0;
    public static float gameTimeMax = 150;
    public static float animationTimeReset = 0.4f;

    public static bool isRaining = false;
    public static bool isFog = false;
    public static bool isPaused = false;
    public static bool doHomeAction = false;

    public static bool buttonCooldown = false;
    public static float buttonCooldownTime = 0f;

    public static bool menuCooldown = false;
    public static bool resetGame = false;
    public static bool showControls = false;

    public static int homeMenuPosition = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
