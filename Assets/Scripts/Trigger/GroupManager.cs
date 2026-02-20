using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [SerializeField] private DragManager[] dragManagers;

    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private GameObject objectToShowInChest;

    [SerializeField] private GameObject goBackTrigger;

    private bool groupCompleted = false;

    private void Update()
    {
        if (!groupCompleted && AreAllManagersCompleted())
        {
            groupCompleted = true;
            OnGroupCompleted();
        }
    }

    private bool AreAllManagersCompleted()
    {
        foreach (var manager in dragManagers)
        {
            if (!manager.IsCompleted)
                return false;
        }
        return true;
    }

    private void OnGroupCompleted()
    {
        Debug.Log("All tasks have been done!");

        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        if (objectToDeactivate != null)
            objectToDeactivate.SetActive(false);

        if (objectToShowInChest != null)
            objectToShowInChest.SetActive(true);

        if (goBackTrigger != null)
            goBackTrigger.SetActive(true);
    }
}
