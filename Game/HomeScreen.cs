// Steven Hankinson 21129647

using TMPro;
using Unity.VectorGraphics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class HomeScreen : MonoBehaviour
{
    public GameObject playButton;
    public GameObject controlsButton;
    public GameObject exitButton;
    public TextMeshPro spaceText;
    public TextMeshPro startText;
    public TextMeshPro controlsText;
    public TextMeshPro exitText;

    public Sprite[] buttons;

    public InputAction pauseNavigateSelect;
    public InputAction pauseNavigateLeft;
    public InputAction pauseNavigateRight;

    SpriteRenderer playRenderer;
    SpriteRenderer controlsRenderer;
    SpriteRenderer exitRenderer;

    MeshRenderer spaceTextRenderer;
    MeshRenderer startTextRenderer;
    MeshRenderer controlsTextRenderer;
    MeshRenderer exitTextRenderer;


    //int homeMenuPosition = -1;

    private void OnEnable()
    {
        pauseNavigateLeft.Enable();
        pauseNavigateLeft.performed += startPauseNavigateLeft;

        pauseNavigateRight.Enable();
        pauseNavigateRight.performed += startPauseNavigateRight;

        pauseNavigateSelect.Enable();
        pauseNavigateSelect.performed += startPauseNavigateSelect;
    }
    private void OnDisable()
    {
        pauseNavigateLeft.Disable();
        pauseNavigateRight.Disable();
        pauseNavigateSelect.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameData.doHomeAction = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        playRenderer = playButton.GetComponent<SpriteRenderer>();
        controlsRenderer = controlsButton.GetComponent<SpriteRenderer>();
        exitRenderer = exitButton.GetComponent<SpriteRenderer>();

        spaceTextRenderer = spaceText.GetComponent<MeshRenderer>();
        startTextRenderer = startText.GetComponent<MeshRenderer>();
        controlsTextRenderer = controlsText.GetComponent<MeshRenderer>();
        exitTextRenderer = exitText.GetComponent<MeshRenderer>();

        if (GameData.buttonCooldown)
        {
            GameData.buttonCooldownTime += 1 / (1f / Time.deltaTime);

            if (GameData.buttonCooldownTime >= 0.1)
            {
                GameData.buttonCooldown = false;
                GameData.buttonCooldownTime = 0f;
            }
        }

        if (GameData.homeMenuPosition >= 3)
        {
            GameData.homeMenuPosition = 0;

        }

        if (GameData.homeMenuPosition <= -2)
        {
            //menuPosition = 2;
        }

        if (GameData.homeMenuPosition == -1)
        {
            playRenderer.sortingOrder = -1;
            controlsRenderer.sortingOrder = -1;
            exitRenderer.sortingOrder = -1;

            spaceTextRenderer.sortingOrder = 2;

            startTextRenderer.sortingOrder = -1;
            controlsTextRenderer.sortingOrder = -1;
            exitTextRenderer.sortingOrder = -1;

            if (GameData.doHomeAction)
            {
                GameData.homeMenuPosition = 0;

                playRenderer.sortingOrder = 1;
                controlsRenderer.sortingOrder = 1;
                exitRenderer.sortingOrder = 1;

                playRenderer.sprite = buttons[0];
                controlsRenderer.sprite = buttons[0];
                exitRenderer.sprite = buttons[0];

                GameData.doHomeAction = false;
            }
            
        }

        if (GameData.homeMenuPosition != -1)
        {
            spaceTextRenderer.sortingOrder = -1;

            startTextRenderer.sortingOrder = 2;
            controlsTextRenderer.sortingOrder = 2;
            exitTextRenderer.sortingOrder = 2;
        }

        if (GameData.homeMenuPosition == 0)
        {
            playRenderer.sprite = buttons[1];
            controlsRenderer.sprite = buttons[0];
            exitRenderer.sprite = buttons[0];

            if (GameData.doHomeAction)
            {
                GameData.menuCooldown = true;
                GameData.doHomeAction = false;
                GameData.homeMenuPosition = -1;
                GameData.resetGame = true;
                startTextRenderer.sortingOrder = -1;
                controlsTextRenderer.sortingOrder = -1;
                exitTextRenderer.sortingOrder = -1;
                SceneManager.LoadScene("LevelScene");
            }
        }

        if (GameData.homeMenuPosition == 1)
        {
            playRenderer.sprite = buttons[0];
            controlsRenderer.sprite = buttons[1];
            exitRenderer.sprite = buttons[0];

            if (GameData.doHomeAction && !GameData.showControls)
            {
                GameData.showControls = true;
                GameData.doHomeAction = false;
            }

            if (GameData.doHomeAction && GameData.showControls)
            {
                GameData.showControls = false;
                GameData.doHomeAction = false;
            }
        }

        if (GameData.homeMenuPosition == 2)
        {
            playRenderer.sprite = buttons[0];
            controlsRenderer.sprite = buttons[0];
            exitRenderer.sprite = buttons[1];

            if(GameData.doHomeAction)
            {
                GameData.doHomeAction = false;
                Application.Quit();
            }
        }

        if(GameData.showControls)
        {
            spaceTextRenderer.sortingOrder = -1;

            startTextRenderer.sortingOrder = -1;
            controlsTextRenderer.sortingOrder = -1;
            exitTextRenderer.sortingOrder = -1;
        }
    }

    private void startPauseNavigateLeft(InputAction.CallbackContext context)
    {
        if (!GameData.buttonCooldown && GameData.homeMenuPosition != -1 && !GameData.showControls)
        {
            GameData.buttonCooldown = true;

            if(GameData.homeMenuPosition == 0)
            {
                GameData.homeMenuPosition = 2;
                return;
            }

            GameData.homeMenuPosition--;
        }
    }

    private void startPauseNavigateRight(InputAction.CallbackContext context)
    {
        if (!GameData.buttonCooldown && GameData.homeMenuPosition != -1 && !GameData.showControls)
        {
            GameData.homeMenuPosition++;
            GameData.buttonCooldown = true;
        }
    }

    private void startPauseNavigateSelect(InputAction.CallbackContext context)
    {
        if(!GameData.buttonCooldown)
        {
            GameData.doHomeAction = true;
        }
    }
}
