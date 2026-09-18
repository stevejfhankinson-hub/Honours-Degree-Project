// Steven Hankinson 21129647

using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PauseGame : MonoBehaviour
{
    public GameObject background;
    public GameObject arrow;
    public TextMeshPro resume;
    public TextMeshPro exit;

    // Store the input actions for action, navigating up, navigating down and selecting in the pause menu
    public InputAction pauseAction;
    public InputAction pauseNavigateSelect;
    public InputAction pauseNavigateUp;
    public InputAction pauseNavigateDown;

    bool doAction = false;

    int pauseState = 0;
    int pauseMenuState = 0;

    SpriteRenderer backgroundRender;
    SpriteRenderer arrowRender;
    MeshRenderer resumeRender;
    MeshRenderer exitRender;

    // Enables the input actions and makes them call the functions for them
    private void OnEnable()
    {
        pauseAction.Enable();
        pauseAction.performed += startPauseAction;

        pauseNavigateUp.Enable();
        pauseNavigateUp.performed += startPauseNavigateUp;

        pauseNavigateDown.Enable();
        pauseNavigateDown.performed += startPauseNavigateDown;

        pauseNavigateSelect.Enable();
        pauseNavigateSelect.performed += startPauseNavigateSelect;
    }

    // Disables the input actions
    private void OnDisable()
    {
        pauseAction.Disable();
        pauseNavigateUp.Disable();
        pauseNavigateDown.Disable();
        pauseNavigateSelect.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        background.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, exit.rectTransform.position.z);
        resume.rectTransform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, resume.rectTransform.position.z);
        exit.rectTransform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, exit.rectTransform.position.z);

        backgroundRender = background.GetComponent<SpriteRenderer>();
        arrowRender = arrow.GetComponent<SpriteRenderer>();
        resumeRender = resume.GetComponent<MeshRenderer>();
        exitRender = exit.GetComponent<MeshRenderer>();

        // If the game is paused, it bring up the UI for the pause screen
        if(GameData.isPaused)
        {
            GameData.menuCooldown = true;
            backgroundRender.sortingOrder = GameData.layerUI + 2;
            resumeRender.sortingOrder = GameData.layerUI + 3;
            exitRender.sortingOrder = GameData.layerUI + 3;
            arrowRender.sortingOrder = GameData.layerUI + 3;
        }
        
        

        // Gives a cooldown to after pressing a button so it doesn't continuously scroll through the menu options.
        if (GameData.buttonCooldown)
        {
            GameData.buttonCooldownTime += 1 / (1f / Time.deltaTime);

            if (GameData.buttonCooldownTime >= 0.1)
            {
                GameData.buttonCooldown = false;
                GameData.buttonCooldownTime = 0f;
            }
        }

        // Changes the pause menu state to be within the range
        if (pauseMenuState >= 2)
        {
            pauseMenuState = 0;

        }
        
        if (pauseState >= 2)
        {
            pauseState = 0;

        }
        
        if (pauseMenuState <= -1)
        {
            pauseMenuState = 1;

        }
        
        // Moves the arrow to show you are on resume
        
        if (pauseMenuState == 0)
        {
            arrow.transform.position = new Vector3(resume.rectTransform.position.x - 1.75f, resume.rectTransform.position.y + 0.1f, arrow.transform.position.z);
        }

        // Hides all pause screen UI and unpauses the game
        if (pauseState == 0)
        {
            GameData.isPaused = false;
            
            backgroundRender.sortingOrder = 0;
            resumeRender.sortingOrder = 0;
            exitRender.sortingOrder = 0;
            arrowRender.sortingOrder = 0;
        }
        
        // If you press a button while highlighting unpause, it unpauses the game
        if (doAction && pauseMenuState == 0 && GameData.isPaused)
        {
            pauseState = 0;
            doAction = false;

            PlayerData.unpauseDelay = true;
            PlayerData.unpauseDelayTime = 0f;
        }


        // Shows all the pause menu UI
        if (pauseState == 1)
        {
            GameData.isPaused = true;

            backgroundRender.sortingOrder = GameData.layerUI;
            arrowRender.sortingOrder = GameData.layerUI + 1;
            resumeRender.sortingOrder = GameData.layerUI + 1;
            exitRender.sortingOrder = GameData.layerUI + 1;
        }

        // Moves the arrow to show you are on exit
        if (pauseMenuState == 1)
        {
            arrow.transform.position = new Vector3(exit.rectTransform.position.x - 1.75f, exit.rectTransform.position.y + 0.1f, arrow.transform.position.z);
        }

        // Ends the game if you press the exit button in the pause menu
        if (doAction && pauseMenuState == 1 && GameData.isPaused)
        {
            pauseState = 0;
            SceneManager.LoadScene("HomeScene");
        }

        GameData.doHomeAction = false;
    }
    private void startPauseAction(InputAction.CallbackContext context)
    {
        if (!GameData.buttonCooldown)
        {
            pauseMenuState = 0;
            pauseState++;
            GameData.buttonCooldown = true;
            doAction = false;
        }
    }

    private void startPauseNavigateUp(InputAction.CallbackContext context)
    {
        if (!GameData.buttonCooldown)
        {
            pauseMenuState--;
            GameData.buttonCooldown = true;
        }
    }

    private void startPauseNavigateDown(InputAction.CallbackContext context)
    {
        if (!GameData.buttonCooldown)
        {
            pauseMenuState++;
            GameData.buttonCooldown = true;
        }
    }

    private void startPauseNavigateSelect(InputAction.CallbackContext context)
    {
        doAction = true;
    }
}
