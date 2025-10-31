using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public FadeScreen fadeScreen;

    // [SerializeField] GameObject wristUI;
    // public bool activeWristUI = false;

    public float startVideoTime = 5f;
    public float finalVideoTime = 10f;
    //public float finalVideoTime = 7f;
    Scene currentScene;
    private string sceneName;
    //public Button continueBtn;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    public void GoToScene(string name)
    {
        StartCoroutine(GoToSceneRoutine(name));
    }

    IEnumerator GoToSceneRoutine(string name)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(name);
    }

    private void Update()
    {
        if (sceneName == "StartCutScene")
        {
            startVideoTime -= Time.deltaTime;

            if (startVideoTime <= 0) GoToScene("FINAL SCENE");
        }
        if (sceneName == "FinalDemoScene")
        {
            finalVideoTime -= Time.deltaTime;

            if (finalVideoTime <= 0) GoToScene("CreditsScene");
        }
    }

    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed) DisplayWristUI();
    }

    public void DisplayWristUI()
    {
        // if (activeWristUI)
        // {
        //     // wristUI.SetActive(false);
        //     activeWristUI = false;
        //     Time.timeScale = 1;
        // } else
        // {
        //     // wristUI.SetActive(true);
        //     activeWristUI = true;
        //     Time.timeScale = 0;
        // }
    }

    public void ResumeGame()
    {
        DisplayWristUI();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
