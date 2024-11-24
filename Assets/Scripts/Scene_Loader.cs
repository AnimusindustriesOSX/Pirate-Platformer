using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loader : MonoBehaviour
{
    // Start is called before the first frame update
    // Array of scene names to load additively
    public string[] scenesToLoad;

    void Start()
    {
        // Start the coroutine to load scenes
        StartCoroutine(LoadScenesAdditively());
    }

    IEnumerator LoadScenesAdditively()
    {
        // Iterate over the scene names
        for (int i = 0; i < scenesToLoad.Length; i++)
        {
            // Check if the scene is already loaded
            if (SceneManager.GetSceneByName(scenesToLoad[i]).isLoaded)
            {
                continue;
            }

            // Load the scene additively
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);

            // Wait until the scene is loaded
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
