using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public enum ItemType { Engine, FireExt, Fuel, Hammer }
    public ItemType itemType;

    private void Start()
    {
        if (IsAlreadyCollected())
            gameObject.SetActive(false);
    }

    private bool IsAlreadyCollected()
    {
        switch (itemType)
        {
            case ItemType.Engine: return PlayerInventoryData.HasEngine;
            case ItemType.FireExt: return PlayerInventoryData.HasFireExt;
            case ItemType.Fuel: return PlayerInventoryData.HasFuel;
            case ItemType.Hammer: return PlayerInventoryData.HasHammer;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (itemType)
        {
            case ItemType.Engine: PlayerInventoryData.HasEngine = true; break;
            case ItemType.FireExt: PlayerInventoryData.HasFireExt = true; break;
            case ItemType.Fuel: PlayerInventoryData.HasFuel = true; break;
            case ItemType.Hammer: PlayerInventoryData.HasHammer = true; break;
        }

        gameObject.SetActive(false);
    }
}
