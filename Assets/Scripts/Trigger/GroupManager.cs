using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [SerializeField] private DragManager[] dragManagers;

    [SerializeField] private GameObject objectToActivate;
    [SerializeField] private GameObject objectToDeactivate;
    [SerializeField] private GameObject objectToShowInChest;

    [SerializeField] private GameObject goBackTrigger;

    [Header("How many tasks must be completed?")]
    [SerializeField] private int requiredTasks = 1;

    private bool groupCompleted = false;

    private void Update()
    {
        if (!groupCompleted && AreRequiredManagersCompleted())
        {
            groupCompleted = true;
            OnGroupCompleted();
        }
    }

    private bool AreRequiredManagersCompleted()
    {
        int completed = 0;

        foreach (var manager in dragManagers)
        {
            if (manager.IsCompleted)
                completed++;
        }

        return completed >= requiredTasks;
    }

    private void OnGroupCompleted()
    {
        Debug.Log("Required number of tasks completed!");

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
