// Steven Hankinson 21129647

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalking : MonoBehaviour
{
    public float maxSpeed;
    float moveSpeed;
    public InputAction Walking;
    public Sprite[] WalkSprites;
    SpriteRenderer PlayerRender;
    Rigidbody2D PlayerRigidbody;

    float animationTime;

    int NorthCycle1 = 0;
    int NorthCycle2 = 1;

    int NorthEastCycle1 = 2;
    int NorthEastCycle2 = 3;

    int EastCycle1 = 4;
    int EastCycle2 = 5;

    int SouthEastCycle1 = 6;
    int SouthEastCycle2 = 7;

    int SouthCycle1 = 8;
    int SouthCycle2 = 9;

    int SouthWestCycle1 = 10;
    int SouthWestCycle2 = 11;

    int WestCycle1 = 12;
    int WestCycle2 = 13;

    int NorthWestCycle1 = 14;
    int NorthWestCycle2 = 15;

    Vector2 walking;
    private void OnEnable()
    {
        Walking.Enable();
    }

    private void OnDisable()
    {
        Walking.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerData.playerReset)
        {
            transform.position = new Vector3(7, -15, 0);

            PlayerData.playerItem = false;
            PlayerData.smokeBombAmount = 5;
            PlayerData.score = 0f;
            PlayerData.scoreMultiplier = 1;
            PlayerData.dashing = false;
            PlayerData.attacking = false;

            PlayerData.dashingTime = 0.0f;
            PlayerData.dashCooldown = true;
            PlayerData.dashing = false;
            PlayerData.dashCooldown = false;
            PlayerData.dashCooldownTime = 0f;

            PlayerData.directionNorth = true;
            PlayerData.directionEast = false;
            PlayerData.directionSouth = false;
            PlayerData.directionWest = false;

            PlayerData.directionNorthEast = false;
            PlayerData.directionNorthWest = false;
            PlayerData.directionSouthEast = false;
            PlayerData.directionSouthWest = false;

            PlayerData.playerReset = false;
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
            moveSpeed = maxSpeed * 0.85f;
        }

        PlayerData.walkSpeed = (moveSpeed / (1 / Time.deltaTime)) * GameData.timeSpeed;

        PlayerRender = GetComponent<SpriteRenderer>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();

        if (GameData.isPaused)
        {
            return;
        }

        PlayerData.moveDirection = Walking.ReadValue<Vector2>();

        if(PlayerData.resetSprite)
        {
            PlayerData.resetSprite = false;
            PlayerRender.sprite = WalkSprites[PlayerData.returnSprite];
        }

        if (PlayerData.moveDirection.x != 0 || PlayerData.moveDirection.y != 0)
        {
            animationTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (animationTime > GameData.animationTimeReset)
                animationTime = 0f;

            if (!PlayerData.dashing && !PlayerData.attacking)
            {
                transform.Translate(PlayerData.moveDirection.x * PlayerData.walkSpeed, PlayerData.moveDirection.y * PlayerData.walkSpeed, 0f);

                // North
                if (PlayerData.moveDirection.y > 0 && PlayerData.moveDirection.x == 0)
                {
                    PlayerData.directionNorth = true;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[NorthCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[NorthCycle2];

                }

                // North East
                if (PlayerData.moveDirection.y > 0 && PlayerData.moveDirection.x > 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = true;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[NorthEastCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[NorthEastCycle2];
                }

                // North West
                if (PlayerData.moveDirection.y > 0 && PlayerData.moveDirection.x < 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = true;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[NorthWestCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[NorthWestCycle2];
                }

                // South
                if (PlayerData.moveDirection.y < 0 && PlayerData.moveDirection.x == 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = true;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[SouthCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[SouthCycle2];
                }

                // South East
                if (PlayerData.moveDirection.y < 0 && PlayerData.moveDirection.x > 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = true;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[SouthEastCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[SouthEastCycle2];
                }

                // South West
                if (PlayerData.moveDirection.y < 0 && PlayerData.moveDirection.x < 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = true;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[SouthWestCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[SouthWestCycle2];
                }

                // East
                if (PlayerData.moveDirection.x > 0 && PlayerData.moveDirection.y == 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = true;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = false;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[EastCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[EastCycle2];
                }

                // West
                if (PlayerData.moveDirection.x < 0 && PlayerData.moveDirection.y == 0)
                {
                    PlayerData.directionNorth = false;
                    PlayerData.directionEast = false;
                    PlayerData.directionSouth = false;
                    PlayerData.directionWest = true;

                    PlayerData.directionNorthEast = false;
                    PlayerData.directionNorthWest = false;
                    PlayerData.directionSouthEast = false;
                    PlayerData.directionSouthWest = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[WestCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        PlayerRender.sprite = WalkSprites[WestCycle2];
                }
            }
        }
    }
}
