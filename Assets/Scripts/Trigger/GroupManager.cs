using UnityEngine;

public class GroupManager : MonoBehaviour
{
    [Header("Drag Managery w tej grupie")]
    [SerializeField] private DragManager[] dragManagers;

    [Header("Co zrobić po ukończeniu grupy")]
    [SerializeField] private GameObject objectToActivate;
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
        Debug.Log("Grupa zadań została ukończona!");

        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        if (objectToDeactivate != null)
            objectToDeactivate.SetActive(false);
    }
}
