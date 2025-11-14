using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerScene : MonoBehaviour
{
    public string sceneToLoad;
    public string spawnPointTag = "SpawnPoint"; // tag for identifying spawn point

    private string currentSceneName; // to track the current scene
    public GameObject playerPrefab;
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
        // Znajdź istniejącego gracza
        GameObject player = GameObject.FindWithTag("Player");

        // Jeśli istnieje, ustaw jego pozycję na spawn point
        GameObject spawnpoint = GameObject.FindWithTag(spawnPointTag);
        if (player != null && spawnpoint != null)
        {
            player.transform.position = spawnpoint.transform.position;
        }

        // Ustawienia kursora i czasu gry
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        Time.timeScale = 1f;

        // Wznowienie kontrolera gracza
        var controller = player?.GetComponent<Artemis.FPController>();
        if (controller != null)
        {
            controller.ResumeController();
        }

        // Odładuj poprzednią scenę
        SceneManager.UnloadSceneAsync(currentSceneName);

        // Zaktualizuj aktualną scenę i odłącz event handler
        currentSceneName = sceneToLoad;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


    
    

}
