using UnityEngine;

public class TablePlaceZone : MonoBehaviour
{
    [Header("Items to place")]
    [SerializeField] private PickupItem itemA;
    [SerializeField] private PickupItem itemB;

    [Header("Placed versions")]
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    [Header("Combined item")]
    [SerializeField] private GameObject combinedItem;

    [Header("UI Prompt")]
    [SerializeField] private GameObject placePrompt;

    [Header("Quest")]
    [SerializeField] private QuestManager questManager;

    private bool aPlaced = false;
    private bool bPlaced = false;

    private void Start()
    {
        placePrompt.SetActive(false);
        pointA.SetActive(false);
        pointB.SetActive(false);
        combinedItem.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var held = PlayerHoldItem.Instance.heldItem;

        if (held == null)
        {
            placePrompt.SetActive(false);
            return;
        }

        // Pokaż prompt tylko jeśli trzymasz A lub B i nie zostały jeszcze położone
        if (!aPlaced && held == itemA)
            placePrompt.SetActive(true);
        else if (!bPlaced && held == itemB)
            placePrompt.SetActive(true);
        else
            placePrompt.SetActive(false);

        // Położenie przedmiotu
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!aPlaced && held == itemA)
            {
                PlaceItemA();
            }
            else if (!bPlaced && held == itemB)
            {
                PlaceItemB();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        placePrompt.SetActive(false);
    }

    private void PlaceItemA()
    {
        aPlaced = true;

        PlayerHoldItem.Instance.DropItem(Vector3.zero);
        itemA.gameObject.SetActive(false);

        pointA.SetActive(true);

        CheckForCombination();
    }

    private void PlaceItemB()
    {
        bPlaced = true;

        PlayerHoldItem.Instance.DropItem(Vector3.zero);
        itemB.gameObject.SetActive(false);

        pointB.SetActive(true);

        CheckForCombination();
    }

    private void CheckForCombination()
    {
        if (aPlaced && bPlaced)
        {
            // Znikają PointA i PointB
            pointA.SetActive(false);
            pointB.SetActive(false);

            // Pojawia się CombinedItem
            combinedItem.SetActive(true);

            questManager.OnBothPartsPlaced();
        }
    }
}
