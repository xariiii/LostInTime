using UnityEngine;

public class PlayerHoldItem : MonoBehaviour
{
    public static PlayerHoldItem Instance;

    public PickupItem heldItem;
    [SerializeField] private Transform holdPoint; // np. pusta pozycja przed kamerą

    private void Awake()
    {
        Instance = this;
    }

    public void HoldItem(PickupItem item)
    {
        heldItem = item;
        item.transform.SetParent(holdPoint);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public void DropItem(Vector3 position)
    {
        if (heldItem == null) return;

        heldItem.transform.SetParent(null);
        heldItem.transform.position = position;
        heldItem.OnPlaced();
        heldItem = null;
    }

    public bool IsHoldingItem()
    {
        return heldItem != null;
    }
}
