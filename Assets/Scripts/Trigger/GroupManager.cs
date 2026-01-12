using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [SerializeField] private DragManager[] dragManagers; // drag manager in group manager

    [SerializeField] private GameObject objectToActivate; // what happens after task is done
    [SerializeField] private GameObject objectToDeactivate;

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
        Debug.Log("All task have been done!");

        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        if (objectToDeactivate != null)
            objectToDeactivate.SetActive(false);
    }
}
