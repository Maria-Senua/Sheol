using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject wristUI;
    public bool activeWristUI = false;

    public float startVideoTime = 5f;
    public float finalVideoTime = 7f;
    Scene currentScene;
    private string sceneName;
    //public Button continueBtn;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void Update()
    {
        if (sceneName == "StartCutScene")
        {
            Cursor.lockState = CursorLockMode.Locked;
            startVideoTime -= Time.deltaTime;

            if (startVideoTime <= 0) OpenSpiralScene();
        }
        if (sceneName == "FinalCutScene")
        {
            Cursor.lockState = CursorLockMode.Locked;
            finalVideoTime -= Time.deltaTime;

            if (finalVideoTime <= 0)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ShowCredits();
            }
        }
    }

    public void OpenSpiralScene()
    {
        SceneManager.LoadScene("FINAL SCENE");
    }

    public void ShowStartCutScene()
    {
        SceneManager.LoadScene("StartCutScene");
    }

    public void ShowFinalCutScene()
    {
        SceneManager.LoadScene("FinalCutScene");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        DisplayWristUI();
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed) DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
            Time.timeScale = 1;
        } else
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        DisplayWristUI();
    }
}
