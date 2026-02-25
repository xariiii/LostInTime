using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject exitTrigger;

    private void Start()
    {
        exitTrigger.SetActive(false);
    }

    public void OnBothPartsPlaced()
    {
        // Tutaj możesz dodać efekty, dźwięki, animacje
    }

    public void CombinedItemPickedUp()
    {
        exitTrigger.SetActive(true);
    }
}
