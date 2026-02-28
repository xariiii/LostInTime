using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipStart : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    private bool playerInRange = false;

    private void Start()
    {
        startPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (PlayerInventoryData.HasAllItems)
            startPanel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = false;
        startPanel.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (PlayerInventoryData.HasAllItems && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("endScene");
        }
    }
}
