using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private GameObject takePrompt;

    public bool isCombinedItem = false;
    [SerializeField] private QuestManager questManager;

    // 🔥 DODANE — identyfikator przedmiotu
    public string itemId = "";

    private bool pickedUp = false;

    private void Start()
    {
        if (takePrompt != null)
            takePrompt.SetActive(false);
    }

    public void ShowPrompt(bool show)
    {
        if (!pickedUp && takePrompt != null)
            takePrompt.SetActive(show);
    }

    public void PickUp()
    {
        pickedUp = true;

        if (takePrompt != null)
            takePrompt.SetActive(false);

        PlayerHoldItem.Instance.HoldItem(this);

        if (isCombinedItem)
        {
            questManager.CombinedItemPickedUp();
        }
    }

    public void OnPlaced()
    {
        pickedUp = false;
    }
}
