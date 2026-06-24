// Steven Hankinson 21129647

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject smoke;
    public GameObject damageSpace;
    public Sprite[] AttackSprites;
    public InputAction Attack;
    public InputAction Dash;
    public InputAction SmokeBomb;
 
    SpriteRenderer PlayerRender;
    SpriteRenderer SmokeRender;
    Rigidbody2D PlayerRigidbody;

    Vector3 playerWalkingPos;

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

    float dashSpeed = 0f;
    Vector2 dashTarget;

    bool startDashing;
    private void OnEnable()
    {
        Attack.Enable();
        Attack.performed += startAttack;
        Dash.Enable();
        Dash.performed += startDash;
        SmokeBomb.Enable();
        SmokeBomb.performed += startSmokeBomb;
    }

    private void OnDisable()
    {
        Attack.Disable();
        Dash.Disable();
        SmokeBomb.Disable();
    }

    float smokeCooldownTime = 0;
    bool smokeCooldown = false;

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

        if(PlayerData.unpauseDelay)
        {
            if (startDashing)
            {
                startDashing = false;
            }

            PlayerData.unpauseDelayTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (PlayerData.unpauseDelayTime >= PlayerData.unpauseDelayTimeMax)
            {
                PlayerData.unpauseDelay = false;
            }
        }

        PlayerRender = GetComponent<SpriteRenderer>();
        SmokeRender = smoke.GetComponent<SpriteRenderer>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();

        if (PlayerData.useSmokeBomb && !PlayerData.isSmoke && !smokeCooldown && PlayerData.smokeBombAmount > 0)
        {
            PlayerData.useSmokeBomb = false;
            smokeCooldown = true;
            PlayerData.isSmoke = true;
            PlayerData.smokeBombAmount--;
            smoke.transform.position = new Vector3(transform.position.x, transform.position.y, -9f);
            SmokeRender.sortingOrder = PlayerRender.sortingOrder;
        }

        if (smokeCooldown)
        {
            smokeCooldownTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (smokeCooldownTime > 9)
            {
                PlayerData.isSmoke = false;
                smokeCooldownTime = 0;
                smokeCooldown = false;
                smoke.transform.position = new Vector3(50, 50, -9f);
                SmokeRender.sortingOrder = 0;
            }
        }

        if(startDashing && !PlayerData.attacking && !PlayerData.dashing && !PlayerData.dashCooldown && !PlayerData.unpauseDelay)
        {
            dashTarget = transform.position;

            if (PlayerData.directionNorth)
            {
                dashTarget.y += 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f;
            }

            if (PlayerData.directionNorthEast)
            {
                dashTarget.x += 6.5f;
                dashTarget.y += 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f * 0.70710678f;
            }

            if (PlayerData.directionEast)
            {
                dashTarget.x += 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f;
            }

            if (PlayerData.directionSouthEast)
            {
                dashTarget.x += 6.5f;
                dashTarget.y -= 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f * 0.70710678f;
            }

            if (PlayerData.directionSouth)
            {
                dashTarget.y -= 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f;
            }

            if (PlayerData.directionSouthWest)
            {
                dashTarget.x -= 6.5f;
                dashTarget.y -= 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2 * 0.70710678f;
            }

            if (PlayerData.directionWest)
            {
                dashTarget.x -= 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f;
            }

            if (PlayerData.directionNorthWest)
            {
                dashTarget.x -= 6.5f;
                dashTarget.y += 6.5f;
                dashSpeed = PlayerData.walkSpeed * 2.2f * 0.70710678f;
            }

            PlayerData.dashing = true;
            startDashing = false;
        }
        
        if (PlayerData.dashing && !PlayerData.dashCooldown && !PlayerData.unpauseDelay)
        {
            PlayerData.dashingTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (PlayerData.directionNorth)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthCycle2];

                PlayerData.returnSprite = NorthCycle1;
            }

            if (PlayerData.directionNorthEast)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthEastCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthEastCycle2];

                PlayerData.returnSprite = NorthEastCycle1;
            }

            if (PlayerData.directionEast)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[EastCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[EastCycle2];

                PlayerData.returnSprite = EastCycle1;
            }

            if (PlayerData.directionSouthEast)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthEastCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthEastCycle2];

                PlayerData.returnSprite = SouthEastCycle1;
            }

            if (PlayerData.directionSouth)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthCycle2];

                PlayerData.returnSprite = SouthCycle1;
            }

            if (PlayerData.directionSouthWest)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthWestCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthWestCycle2];

                PlayerData.returnSprite = SouthWestCycle1;
            }

            if (PlayerData.directionWest)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[WestCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[WestCycle2];

                PlayerData.returnSprite = WestCycle1;
            }

            if (PlayerData.directionNorthWest)
            {
                if (PlayerData.dashingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthWestCycle1];

                if (PlayerData.dashingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthWestCycle2];

                PlayerData.returnSprite = NorthWestCycle1;
            }

            transform.position = Vector2.MoveTowards(transform.position, dashTarget, dashSpeed);

            if (PlayerData.dashingTime >= PlayerData.dashingTimeMax)
            {
                PlayerData.dashingTime = 0.0f;
                PlayerData.dashCooldown = true;
                PlayerData.dashing = false;
                PlayerData.resetSprite = true;
            }
        }

        if (PlayerData.dashCooldown)
        {
            startDashing = false;
            PlayerData.dashCooldownTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (PlayerData.dashCooldownTime > PlayerData.dashCooldownTimeMax)
            {
                PlayerData.dashCooldown = false;
                PlayerData.dashCooldownTime = 0f;
            }
        }

        if (PlayerData.attacking && !PlayerData.dashing)
        {
            PlayerData.attackingTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (PlayerData.directionNorth)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthCycle2];

                PlayerData.returnSprite = NorthCycle1;
            }

            if (PlayerData.directionNorthEast)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthEastCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthEastCycle2];

                PlayerData.returnSprite = NorthEastCycle1;
            }

            if (PlayerData.directionEast)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                {
                    PlayerRender.sprite = AttackSprites[EastCycle1];
                }

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                {
                    PlayerRender.sprite = AttackSprites[EastCycle2];
                }

                PlayerData.returnSprite = EastCycle1;
            }

            if (PlayerData.directionSouthEast)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthEastCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthEastCycle2];

                PlayerData.returnSprite = SouthEastCycle1;
            }

            if (PlayerData.directionSouth)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthCycle2];

                PlayerData.returnSprite = SouthCycle1;
            }

            if (PlayerData.directionSouthWest)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthWestCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[SouthWestCycle2];

                PlayerData.returnSprite = SouthWestCycle1;
            }

            if (PlayerData.directionWest)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[WestCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[WestCycle2];

                PlayerData.returnSprite = WestCycle1;
            }

            if (PlayerData.directionNorthWest)
            {
                if (PlayerData.attackingTime < PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthWestCycle1];

                if (PlayerData.attackingTime >= PlayerData.attackingTimeMax / 1.55f)
                    PlayerRender.sprite = AttackSprites[NorthWestCycle2];

                PlayerData.returnSprite = NorthWestCycle1;
            }

            if (PlayerData.attackingTime >= PlayerData.attackingTimeMax)
            {
                PlayerData.resetSprite = true;
                PlayerData.attacking = false;
                PlayerData.attackingTime = 0f;
            }
        }

        if (PlayerData.directionNorth)
        {
            damageSpace.transform.position = new Vector3(transform.position.x,
                                                         transform.position.y + 0.5f,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionNorthEast)
        {
            damageSpace.transform.position = new Vector3(transform.position.x + 0.5f,
                                                         transform.position.y + 0.5f,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionEast)
        {
            damageSpace.transform.position = new Vector3(transform.position.x + 0.5f,
                                                         transform.position.y,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionSouthEast)
        {
            damageSpace.transform.position = new Vector3(transform.position.x + 0.65f,
                                                         transform.position.y - 0.65f,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionSouth)
        {
            damageSpace.transform.position = new Vector3(transform.position.x,
                                                         transform.position.y - 0.5f,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionSouthWest)
        {
            damageSpace.transform.position = new Vector3(transform.position.x - 0.75f,
                                                         transform.position.y - 0.65f,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionWest)
        {
            damageSpace.transform.position = new Vector3(transform.position.x - 0.5f,
                                                         transform.position.y,
                                                         damageSpace.transform.position.z);
        }

        if (PlayerData.directionNorthWest)
        {
            damageSpace.transform.position = new Vector3(transform.position.x - 0.5f,
                                                         transform.position.y + 0.5f,
                                                         damageSpace.transform.position.z);
        }
    }

    private void startAttack(InputAction.CallbackContext context)
    {
        if (!PlayerData.attacking && !PlayerData.dashing)
        {
            PlayerData.attackingTime = 0;
            PlayerData.attacking = true;
        }
    }

    private void startDash(InputAction.CallbackContext context)
    {
        if (!startDashing && !PlayerData.dashCooldown && !PlayerData.attacking && !GameData.resetGame)
        {
            PlayerData.dashingTime = 0;
            startDashing = true;
        }
    }

    private void startSmokeBomb(InputAction.CallbackContext context)
    {
        PlayerData.useSmokeBomb = true;
    }
}
