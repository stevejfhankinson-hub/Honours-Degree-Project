// Steven Hankinson 21129647

using UnityEngine;

public class GameReset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameData.resetGame)
        {
            GameData.resetGame = false;
            PlayerData.playerReset = true;
            PlayerData.resetDashUI = true;

            GameData.gameTime = 0f;

            for (int i = 0; i < EnemyData.enemyReset.Length; i++)
            {
                EnemyData.enemyReset[i] = false;
            }

            for (int i = 0; i < VillagerData.villagerReset.Length; i++)
            {
                VillagerData.villagerReset[i] = false;
            }
        }
    }
}
