// Steven Hankinson 21129647

using UnityEngine;

public class Weather : MonoBehaviour
{
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
        if (GameData.isPaused)
        {
            return;
        }

        WeatherRender = GetComponent<SpriteRenderer>();

        weatherTime += (1f / (1f / Time.deltaTime)) * GameData.timeSpeed;

        if (weatherTime >= endWeatherTime)
        {
            weatherTime = 0f;

            startWeatherTime = Random.Range(10, 31);
            endWeatherTime = startWeatherTime + (Random.Range(10, 21));

            GameData.isFog = false;
            GameData.isRaining = false;
        }

        if(weatherTime < startWeatherTime)
        {
            WeatherRender.sortingOrder = 0;
        }

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
