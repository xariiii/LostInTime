using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerScene : MonoBehaviour
{
    public string sceneToLoad;
    public string spawnPointTag = "SpawnPoint"; // tag for identifying spawn point

    private string currentSceneName; // to track the current scene

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DontDestroyOnLoad(other.gameObject); // Prevert player object from being destroyed

            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive); // Load new scene
            SceneManager.sceneLoaded += OnSceneLoaded; // Register callback for scene load
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {


        if (scene.name == sceneToLoad)
        {
            GameObject spawnpoint = GameObject.FindWithTag(spawnPointTag);
            if (spawnpoint != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnpoint.transform.position;
                }
            }
            // Unload previous scene
            SceneManager.UnloadSceneAsync(currentSceneName);

            // Update the current scene name and unregister event handler

            currentSceneName = sceneToLoad;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    
    

}
