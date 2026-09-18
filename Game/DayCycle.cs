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
        // Set the initial light color to dayLight
        lightColour.color = dayLight;
    }

    // Update is called once per frame
    void Update()
    {
        // Resets the light colour  when the time is reset
        if (GameData.gameTime == 0f)
        {
            lightColour.color = dayLight;
        }

        // Stops the code from progressing past this point if the game is paused
        if (GameData.isPaused)
        {
            return;
        }

        // Calculates the speed at which the light colour should change based on the game time and time speed
        float colourSpeed = (GameData.gameTimeMax * (1f / Time.deltaTime) / GameData.timeSpeed) - ((startNight * (1f / Time.deltaTime)) / GameData.timeSpeed);

        // Works out the difference between the day and night light colours
        // and divides it by the colourSpeed to get the amount to change the light colour by each frame
        float redAmount = ((nightLight.r - dayLight.r) / colourSpeed);
        float greenAmount = ((nightLight.g - dayLight.g) / colourSpeed);
        float blueAmount = ((nightLight.b - dayLight.b) / colourSpeed);

        // As time progresses, the light colour will change from dayLight to nightLight
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

        // Once the game has been playing for a certain time the game ends
        if (GameData.gameTime >= GameData.gameTimeMax)
        {
            SceneManager.LoadScene("HomeScene");
        }
    }
}
