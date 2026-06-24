// Steven Hankinson 21129647

using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class DayCycle : MonoBehaviour
{
    public Light2D lightColour;
    public Color dayLight;
    public Color nightLight;
    float startNight = 60;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightColour.color = dayLight;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.gameTime == 0f)
        {
            lightColour.color = dayLight;
        }

        if (GameData.isPaused)
        {
            return;
        }

        float colourSpeed = (GameData.gameTimeMax * (1f / Time.deltaTime) / GameData.timeSpeed) - ((startNight * (1f / Time.deltaTime)) / GameData.timeSpeed);

        float redAmount = ((nightLight.r - dayLight.r) / colourSpeed);
        float greenAmount = ((nightLight.g - dayLight.g) / colourSpeed);
        float blueAmount = ((nightLight.b - dayLight.b) / colourSpeed);

        GameData.gameTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

        if (GameData.gameTime >= startNight)
        {
            if (GameData.gameTime < GameData.gameTimeMax)
            {
                lightColour.color = new Color(lightColour.color.r + redAmount,
                                              lightColour.color.g + greenAmount,
                                              lightColour.color.b + blueAmount,
                                              lightColour.color.a);
            }
        }

        if (GameData.gameTime >= GameData.gameTimeMax)
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}
