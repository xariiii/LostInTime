using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private GameObject takePrompt;     // UI text "Take"
    [SerializeField] private GameObject nextTrigger;    // Trigger that appears after pickup

    private bool pickedUp = false;

    private void Start()
    {
        if (takePrompt != null)
            takePrompt.SetActive(false);

        if (nextTrigger != null)
            nextTrigger.SetActive(false);
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

        gameObject.SetActive(false); // Hide the item

        if (nextTrigger != null)
            nextTrigger.SetActive(true); // Unlock next area
    }
}
