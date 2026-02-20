using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTP : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject playerPrefab;

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneToLoad);
    }
}


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    SceneManager.sceneLoaded -= OnSceneLoaded;

    // Try to find existing player
    GameObject player = GameObject.FindWithTag("Player");

    // If no player exists, try to spawn one
    if (player == null)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("TriggerTP: playerPrefab is NOT assigned! Cannot spawn player.");
            return;
        }

        player = Instantiate(playerPrefab);
        player.tag = "Player";
        Debug.Log("Player was missing in scene. Instantiated new player.");
    }

    // Optional: move to spawn point
    GameObject spawn = GameObject.FindWithTag("SpawnPoint");
    if (spawn != null)
    {
        player.transform.position = spawn.transform.position;
        player.transform.rotation = spawn.transform.rotation;
    }
}

}
