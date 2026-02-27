using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject engineIcon;
    [SerializeField] private GameObject fireExtIcon;
    [SerializeField] private GameObject fuelIcon;
    [SerializeField] private GameObject hammerIcon;

    private void Start()
    {
        engineIcon.SetActive(PlayerInventoryData.HasEngine);
        fireExtIcon.SetActive(PlayerInventoryData.HasFireExt);
        fuelIcon.SetActive(PlayerInventoryData.HasFuel);
        hammerIcon.SetActive(PlayerInventoryData.HasHammer);
    }
}
