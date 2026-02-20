using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private PickupItem currentItem;

    void Update()
    {
        if (currentItem != null && Input.GetKeyDown(KeyCode.E))
        {
            currentItem.PickUp();
            currentItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PickupItem item))
        {
            currentItem = item;
            item.ShowPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PickupItem item))
        {
            if (currentItem == item)
            {
                item.ShowPrompt(false);
                currentItem = null;
            }
        }
    }
}
