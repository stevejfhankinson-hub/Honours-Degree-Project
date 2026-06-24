// Steven Hankinson 21129647

using UnityEngine;

public class EnemyAnimate : MonoBehaviour
{
    public int enemyNumber;
    public Sprite[] WalkSprites;

    SpriteRenderer EnemyRender;

    float positionTime;
    Vector2 oldPos;
    Vector2 difference;
    Vector2 biggerAxis;

    float animationTime;

    int NorthCycle1 = 0;
    int NorthCycle2 = 1;

    int EastCycle1 = 2;
    int EastCycle2 = 3;

    int SouthCycle1 = 4;
    int SouthCycle2 = 5;

    int WestCycle1 = 6;
    int WestCycle2 = 7;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.isPaused)
        {
            return;
        }

        EnemyRender = GetComponent<SpriteRenderer>();

        if (!EnemyData.bouncing[enemyNumber])
        {
            difference.x = transform.position.x - oldPos.x;
            difference.y = transform.position.y - oldPos.y;

            biggerAxis = difference;

            if (biggerAxis.x < 0)
            {
                biggerAxis.x = -biggerAxis.x;
            }

            if (biggerAxis.y < 0)
            {
                biggerAxis.y = -biggerAxis.y;
            }

            animationTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (animationTime > GameData.animationTimeReset)
                animationTime = 0f;

            // East and West
            if (biggerAxis.x >= biggerAxis.y)
            {
                // East
                if (difference.x > 0)
                {
                    EnemyData.directionNorth[enemyNumber] = false;
                    EnemyData.directionEast[enemyNumber] = true;
                    EnemyData.directionSouth[enemyNumber] = false;
                    EnemyData.directionWest[enemyNumber] = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[EastCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[EastCycle2];
                }

                // West
                if (difference.x < 0)
                {
                    EnemyData.directionNorth[enemyNumber] = false;
                    EnemyData.directionEast[enemyNumber] = false;
                    EnemyData.directionSouth[enemyNumber] = false;
                    EnemyData.directionWest[enemyNumber] = true;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[WestCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[WestCycle2];
                }

            }

            // North and South
            if (biggerAxis.x <= biggerAxis.y)
            {
                // North
                if (difference.y > 0)
                {
                    EnemyData.directionNorth[enemyNumber] = true;
                    EnemyData.directionEast[enemyNumber] = false;
                    EnemyData.directionSouth[enemyNumber] = false;
                    EnemyData.directionWest[enemyNumber] = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[NorthCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[NorthCycle2];
                }

                // South
                if (difference.y < 0)
                {
                    EnemyData.directionNorth[enemyNumber] = false;
                    EnemyData.directionEast[enemyNumber] = false;
                    EnemyData.directionSouth[enemyNumber] = true;
                    EnemyData.directionWest[enemyNumber] = false;

                    if (animationTime < GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[SouthCycle1];

                    if (animationTime >= GameData.animationTimeReset / 2f)
                        EnemyRender.sprite = WalkSprites[SouthCycle2];
                }

            }

            positionTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

            if (positionTime >= 0.5f)
            {
                positionTime = 0;
                oldPos = transform.position;
            }
        }
    }
}
