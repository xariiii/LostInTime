using UnityEngine;

public class PlayerHoldItem : MonoBehaviour
{
    public static PlayerHoldItem Instance;

    public PickupItem heldItem;
    [SerializeField] private Transform holdPoint;

    [Header("Aktywacja tylko dla wybranego itemu")]
    [SerializeField] private PickupItem specificItem;      // ten item musi być trzymany
    [SerializeField] private GameObject enableForSpecific; // to się włączy tylko dla niego

    private void Awake()
    {
        Instance = this;
        UpdateObjects();
    }

    public void HoldItem(PickupItem item)
    {
        heldItem = item;

        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        UpdateObjects();
    }

    public void DropItem(Vector3 position)
    {
        if (heldItem == null) return;

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
