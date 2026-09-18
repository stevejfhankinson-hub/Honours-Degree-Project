// Steven Hankinson 21129647

using UnityEngine;

public class Weather : MonoBehaviour
{
    // Store the sprites for rain and fog
    public Sprite[] RainSprites;
    public Sprite[] FogSprites;

    SpriteRenderer WeatherRender;

    float animationTime = 0f;
    float animationMax = 0.2f;

    float weatherTime = 0f;
    float startWeatherTime;
    float endWeatherTime;

    int weatherRandom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startWeatherTime = Random.Range(10, 31);
        endWeatherTime = startWeatherTime + (Random.Range(10, 21));
    }

    // Update is called once per frame
    void Update()
    {
        // Stops the code from progressing past this point if the game is paused
        if (GameData.isPaused)
        {
            return;
        }

        WeatherRender = GetComponent<SpriteRenderer>();

        weatherTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

        // Once the weather is meant to stop it stops it
        // The next weather start time and stop time is randomly generated between different numbers
        if (weatherTime >= endWeatherTime)
        {
            weatherTime = 0f;

            startWeatherTime = Random.Range(10, 31);
            endWeatherTime = startWeatherTime + (Random.Range(10, 21));

            GameData.isFog = false;
            GameData.isRaining = false;
        }

        // Hides the weather when it is not active
        if(weatherTime < startWeatherTime)
        {
            WeatherRender.sortingOrder = 0;
        }

        // Once the weather can start makes it appear and randomly picks between rain and fog
        if (weatherTime >= startWeatherTime && !GameData.isRaining && !GameData.isFog)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -9);
            WeatherRender.sortingOrder = GameData.layerInsideHouse - 2;

            weatherRandom = Random.Range(0, 2);

            if (weatherRandom == 0)
            {
                GameData.isRaining = true;
            }

            if (weatherRandom == 1)
            {
                GameData.isFog = true;
            }
        }

        animationTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

        // If it is meant to be raining and foggy it stops these both from happening
        if (!GameData.isRaining && !GameData.isFog)
        {
            weatherRandom = 2;
            WeatherRender.sortingOrder = 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, 2);
        }

        if (animationTime >= animationMax)
        {
            animationTime = 0f;
        }

        // Displays the right sprites for each whether with the rain one being animated
        if (GameData.isRaining)
        {
            if(animationTime < animationMax / 2f)
            {
                WeatherRender.sprite = RainSprites[0];
            }

            if (animationTime >= animationMax / 2f)
            {
                WeatherRender.sprite = RainSprites[1];
            }
        }

        if (GameData.isFog)
        {
            if (animationTime < animationMax / 2f)
            {
                WeatherRender.sprite = FogSprites[0];
            }

            if (animationTime >= animationMax / 2f)
            {
                WeatherRender.sprite = FogSprites[1];
            }
        }

    }
}
