using UnityEngine;

public class InventoryUnlockTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shipB; // Statek B

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Sprawdzamy czy gracz ma wszystkie itemy
        if (!PlayerInventoryData.HasAllItems)
        {
            Debug.Log("Gracz nie ma wszystkich itemów — Statek B NIE zostanie aktywowany.");
            return;
        }

        // Jeśli ma itemy → aktywujemy statek B
        shipB.SetActive(true);

        // Usuwamy statek A
        Destroy(gameObject);
    }
}
