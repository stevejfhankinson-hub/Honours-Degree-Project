// Steven Hankinson 21129647

using UnityEngine;

public class DashUI : MonoBehaviour
{
    public GameObject MainCamera;
    float emptyMeterHeight = -0.7208f;
    float emptyMeterScale = 0.02825f;

    float fullMeterHeight = 0.028f;
    float fullMeterScale = 1.45f;

    float meterHeight;
    float meterScale;

    float heightDifference;
    float scaleDifference;

    bool setStart = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meterHeight = fullMeterHeight;
        meterScale = fullMeterScale;
        heightDifference = (emptyMeterHeight - fullMeterHeight) / ((PlayerData.dashCooldownTimeMax + PlayerData.dashingTimeMax) * 1f) / (1 / Time.deltaTime);
        scaleDifference = (fullMeterScale - emptyMeterScale) / ((PlayerData.dashCooldownTimeMax + PlayerData.dashingTimeMax) * 1f)/ (1 / Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.isPaused)
        {
            return;
        }

        if (meterScale < fullMeterScale)
        {
            meterScale += scaleDifference * GameData.timeSpeed;
            meterHeight -= heightDifference * GameData.timeSpeed;
        }

        if(PlayerData.dashing && !setStart)
        {
            meterHeight = emptyMeterHeight;
            meterScale = emptyMeterScale;
            setStart = true;
        }

        if (!PlayerData.dashCooldown && setStart)
        {
            setStart = false;
        }

        transform.position = new Vector3(transform.position.x, MainCamera.transform.position.y + 4.96f + meterHeight, transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x, meterScale, transform.localScale.z);

        if (PlayerData.resetDashUI)
        {
            transform.position = new Vector3(transform.position.x, 0.09000015f, 1f);
            transform.localScale = new Vector3(transform.localScale.x, 1.6f, 1f);
            PlayerData.resetDashUI = false;
        }
    }
}
