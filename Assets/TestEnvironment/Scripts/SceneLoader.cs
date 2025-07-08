using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    public bool isPaused = false;

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
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
