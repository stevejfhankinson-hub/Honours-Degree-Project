// Steven Hankinson 21129647

using TMPro;
using UnityEngine;

public class ControlsPage : MonoBehaviour
{
    public GameObject ControlsBackground;
    public TextMeshPro ControlsHeadingText;
    public TextMeshPro ControlsDescriptionText;
    public TextMeshPro titleText;
    public GameObject[] buttons;

    SpriteRenderer backgroundRenderer;
    SpriteRenderer[] buttonsRenderer;
    MeshRenderer controlsTextRenderer;
    MeshRenderer descriptionTextRenderer;
    MeshRenderer titleTextRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonsRenderer = new SpriteRenderer[buttons.Length];

        for(int i = 0; i < buttons.Length; i++)
        {
            buttonsRenderer[i] = buttons[i].GetComponent<SpriteRenderer>();
        }

        backgroundRenderer = ControlsBackground.GetComponent<SpriteRenderer>();
        controlsTextRenderer = ControlsHeadingText.GetComponent<MeshRenderer>();
        descriptionTextRenderer = ControlsDescriptionText.GetComponent<MeshRenderer>();
        titleTextRenderer = titleText.GetComponent<MeshRenderer>();

        ControlsHeadingText.transform.position = new Vector3(ControlsBackground.transform.position.x, ControlsBackground.transform.position.y + 4.81f, ControlsHeadingText.transform.position.z);
        ControlsDescriptionText.transform.position = new Vector3(ControlsBackground.transform.position.x, ControlsBackground.transform.position.y - 1.3f, ControlsHeadingText.transform.position.z);

        backgroundRenderer.sortingOrder = 2;
        controlsTextRenderer.sortingOrder = 3;
        descriptionTextRenderer.sortingOrder = 3;

        if(GameData.showControls)
        {
            ControlsBackground.transform.position = new Vector3(0, 0, 0);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttonsRenderer[i].sortingOrder = -1;
            }

            titleTextRenderer.sortingOrder = -1;
        }

        if (!GameData.showControls)
        {
            ControlsBackground.transform.position = new Vector3(25, 0, 0);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttonsRenderer[i].sortingOrder = 1;
            }

            titleTextRenderer.sortingOrder = 1;
        }

        if(GameData.homeMenuPosition == -1)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttonsRenderer[i].sortingOrder = -1;
            }
        }
    }
}
