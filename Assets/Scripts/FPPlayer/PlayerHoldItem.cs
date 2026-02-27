using UnityEngine;

public class PlayerHoldItem : MonoBehaviour
{
    public static PlayerHoldItem Instance;

    public PickupItem heldItem;
    [SerializeField] private Transform holdPoint;

    [Header("Aktywacja tylko dla wybranego itemu")]
    [SerializeField] private PickupItem specificItem;
    [SerializeField] private GameObject enableForSpecific;

    [Header("Przypisz itemy z innych map")]
    [SerializeField] private PickupItem engineItem;
    [SerializeField] private PickupItem fireExtItem;
    [SerializeField] private PickupItem fuelItem;
    [SerializeField] private PickupItem hammerItem;

    private void Awake()
    {
        Instance = this;
        UpdateObjects();
    }

    public void HoldItem(PickupItem item)
    {
        heldItem = item;

        // 🔵 PRZYWRÓCONA TWOJA STARA LOGIKA
        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        // 🔵 NOWE FLAGI – NIC NIE USUWAJĄ, TYLKO DODAJĄ
        if (item == engineItem) PlayerInventoryData.HasEngine = true;
        if (item == fireExtItem) PlayerInventoryData.HasFireExt = true;
        if (item == fuelItem) PlayerInventoryData.HasFuel = true;
        if (item == hammerItem) PlayerInventoryData.HasHammer = true;

        UpdateObjects();
    }

    public void DropItem(Vector3 position)
    {
        if (heldItem == null) return;

        // 🔵 RESET FLAG
        if (heldItem == engineItem) PlayerInventoryData.HasEngine = false;
        if (heldItem == fireExtItem) PlayerInventoryData.HasFireExt = false;
        if (heldItem == fuelItem) PlayerInventoryData.HasFuel = false;
        if (heldItem == hammerItem) PlayerInventoryData.HasHammer = false;

        // 🔵 PRZYWRÓCONA TWOJA STARA LOGIKA
        heldItem.transform.SetParent(null);
        heldItem.transform.position = position;
        heldItem.OnPlaced();
        heldItem = null;

        UpdateObjects();
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }

    private void UpdateObjects()
    {
        if (enableForSpecific != null)
        {
            bool isSpecific = (heldItem == specificItem);
            enableForSpecific.SetActive(isSpecific);
        }
    }
}
