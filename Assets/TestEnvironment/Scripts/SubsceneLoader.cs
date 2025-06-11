using UnityEngine;
using Unity.Entities;
using Unity.Scenes;
using System.Collections;

public class SubsceneLoader : MonoBehaviour
{

    public SubScene subSceneToLoad;

    private Entity subScene;



    public void LoadSubScene()
    {
        Debug.Log("LoadSubScene method was called");
        if (!World.DefaultGameObjectInjectionWorld.IsCreated)
        {
            Debug.LogError("Default World is not created!");
            return;
        }


        var loadParameters = new SceneSystem.LoadParameters
        {
            Flags = SceneLoadFlags.NewInstance
        };

        subScene = SceneSystem.LoadSceneAsync(World.DefaultGameObjectInjectionWorld.Unmanaged, subSceneToLoad.SceneGUID, loadParameters);
        Debug.Log("loadingscene " + subSceneToLoad.name);

        StartCoroutine(CheckScene());

    }

    public void UnLoadSubScene()
    {
        // Specify unload parameters, you can adjust these based on your requirements.
        var unloadParameters = SceneSystem.UnloadParameters.DestroyMetaEntities;

        SceneSystem.UnloadScene(World.DefaultGameObjectInjectionWorld.Unmanaged, subScene, unloadParameters);
    }

    IEnumerator CheckScene()
    {
        int safetyCounter = 0;

        while (!SceneSystem.IsSceneLoaded(World.DefaultGameObjectInjectionWorld.Unmanaged, subScene))
        {
            yield return null;
            safetyCounter++;
            if (safetyCounter > 300)
            {
                Debug.LogWarning("Scene load timeout");
                break;
            }
        }

        Debug.Log("Scene finished loading");
    }


}
