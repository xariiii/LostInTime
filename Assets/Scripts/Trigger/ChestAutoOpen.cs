using UnityEngine;

public class ChestAutoOpen : MonoBehaviour
{
    [Header("Chest Objects")]
    public GameObject chestClosed;
    public GameObject chestOpen;
    public GameObject itemInside;

    private void OnTriggerEnter(Collider other)
    {
        // sprawdzamy czy gracz trzyma jakiś przedmiot
        if (PlayerHoldItem.Instance != null && PlayerHoldItem.Instance.heldItem != null)
        {
            PickupItem held = PlayerHoldItem.Instance.heldItem;

            // sprawdzamy czy trzymany przedmiot to klucz
            if (held.TryGetComponent(out KeyItem key))
            {
                if (key.keyId == "ChestKey")
                {
                    // otwieramy skrzynię
                    chestClosed.SetActive(false);
                    chestOpen.SetActive(true);
                    itemInside.SetActive(true);

                    // usuwamy klucz z ręki
                    Destroy(held.gameObject);
                    PlayerHoldItem.Instance.heldItem = null;
                }
            }
        }
    }
}
