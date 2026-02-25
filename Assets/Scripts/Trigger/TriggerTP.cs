using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTP : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string spawnID; // nazwa spawnpointu
    [SerializeField] private GameObject playerPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // zapamiętaj ID spawnpointu
            SpawnManager.SpawnID = spawnID;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // znajdź istniejącego gracza
        GameObject player = GameObject.FindWithTag("Player");

        // jeśli nie istnieje — stwórz nowego
        if (player == null)
        {
            if (playerPrefab == null)
            {
                Debug.LogError("TriggerTP: playerPrefab is NOT assigned! Cannot spawn player.");
                return;
            }

            player = Instantiate(playerPrefab);
            player.tag = "Player";
        }

        // -----------------------------
        // 1. TELEPORTACJA PO NAZWIE
        // -----------------------------
        GameObject spawnByName = null;

        if (!string.IsNullOrEmpty(SpawnManager.SpawnID))
            spawnByName = GameObject.Find(SpawnManager.SpawnID);

        if (spawnByName != null)
        {
            // USTAWIAMY TYLKO POZYCJĘ
            // NIE ruszamy rotacji — FPController sam ją ustawi
            player.transform.position = spawnByName.transform.position;
            return;
        }

        // -----------------------------
        // 2. FALLBACK: SpawnPoint po tagu
        // -----------------------------
        GameObject spawn = GameObject.FindWithTag("SpawnPoint");
        if (spawn != null)
        {
            player.transform.position = spawn.transform.position;
            // rotacji też nie ruszamy
        }
    }
}
